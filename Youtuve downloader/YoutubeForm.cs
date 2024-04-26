using AngleSharp.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Youtube_downloader;
using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

namespace Youtuve_downloader
{
    public partial class YoutubeForm : Form
    {
        private static WebClient wc = new WebClient();

        public static YoutubeClient youtube = new YoutubeClient();
        public static Video currentVideo { get; set; }
        public static StreamManifest currentStreamManifest { get; set; }

        public static IEnumerable<AudioOnlyStreamInfo> audioStreamsInfo { get; set; }
        public static IEnumerable<VideoOnlyStreamInfo> videoStreamsInfo { get; set; }
        public static IEnumerable<MuxedStreamInfo> muxedStreamsInfo { get; set; }

        public static string AsemblyName = Assembly.GetEntryAssembly().GetName().Name.ToString();

        public static Dictionary<string, string> arguments = new Dictionary<string, string>();

        public YoutubeForm(string[] args)
        {
            foreach (string arg in args)
            {
                string[] splited = arg.Split('=');

                if (splited.Length > 0) arguments.Add(splited[0], string.Join("=", splited.Skip(1)));
            }

            //MessageBox.Show(string.Join("\n",arguments.Select(c => c.Key + " = " + c.Value)));

            InitializeComponent();

            if (arguments.TryGetValue("video", out string video))
            {
                YoutubeLinkTextBox.Text = video;
                FormatComboBox.SelectedIndex = 0;
                AudioCodecsComboBox.Enabled = ReEncodeAudioCheckBox.Checked = true;
            }
            else
            {
                YoutubeLinkTextBox.Text = Config.Get("lastVideo");

                if (int.TryParse(Config.Get("formatIndex"), out int index)) FormatComboBox.SelectedIndex = index;
                
                if (bool.TryParse(Config.Get("ReEncodeAudio"), out bool audioReEncode)) AudioCodecsComboBox.Enabled = ReEncodeAudioCheckBox.Checked = audioReEncode;
                if (bool.TryParse(Config.Get("ReEncodeVideo"), out bool videoReEncode)) VideoCodecsComboBox.Enabled = ReEncodeVideoCheckBox.Checked = videoReEncode;
            }

            wc.DownloadProgressChanged += (s, e) => DownloadProgressBar.Value = e.ProgressPercentage * 10;
            wc.DownloadFileCompleted += (s, e) => DownloadProgressBar.Value = 0;

            foreach (var codec in audioCodecs)
            {
                AudioCodecsComboBox.Items.Add(codec.Key);
            }
            this.AudioCodecsComboBox.SelectedIndex = 2;

            foreach (var codec in videoCodecs)
            {
                VideoCodecsComboBox.Items.Add(codec.Key);
            }
            this.VideoCodecsComboBox.SelectedIndex = 5;
        }

        private void YoutubeLinkTextBox_DoubleClick(object sender, EventArgs e) => YoutubeLinkTextBox.SelectAll();
        public static Regex AlphanumericRegex = new Regex("[^a-zA-Z0-9 -]");
        private async void DownloadButton_Click(object sender, EventArgs e)
        {
            if (currentVideo == null) return;

            dynamic streamInfo = null;

            string fileExtension = string.Empty;

            switch (FormatComboBox.SelectedItem.ToString())
            {
                case "mp3":
                    streamInfo = audioStreamsInfo.ElementAt(AudioStreamsComboBox.SelectedIndex);
                    fileExtension = "mp3";
                    break;

                case "com":
                case "mp4":
                    streamInfo = videoStreamsInfo.ElementAt(VideoStreamsComboBox.SelectedIndex);
                    fileExtension = "mp4";
                    break;

                case "mux":
                    streamInfo = muxedStreamsInfo.ElementAt(VideoStreamsComboBox.SelectedIndex);
                    fileExtension = "mp4";
                    break;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();

            if (FormatComboBox.Text == "mp3")
            {
                string extension = fileExtension;

                if (ReEncodeAudioCheckBox.Checked) extension = fileExtension = AlphanumericRegex.Replace(AudioCodecsComboBox.Items[AudioCodecsComboBox.SelectedIndex].ToString().ToLower(),"").Trim();
                
                saveFileDialog.Filter = extension + " Audio | *." + fileExtension;
            }
            else if (FormatComboBox.Text == "mp4" || FormatComboBox.Text == "mux" || FormatComboBox.Text == "com")
            {
                saveFileDialog.Filter = "MP4 Video|*.mp4|AVI Video|*.avi|MOV Video|*.mov|MKV Video|*.mkv|FLV Video|*.flv;";
            }

            saveFileDialog.Title = "Interesting question";
            saveFileDialog.FileName = SanitizedFileName(currentVideo.Title + "_" + (int)((streamInfo).Bitrate.KiloBitsPerSecond) + "." + fileExtension);

            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            /*if (saveFileDialog.FileName != "")
            {
                DownloadButton.Text = "Conectando";
                DownloadButton.Enabled = false;

                await Task.Run(() =>
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        DownloadFileWithProgress(YoutubeApi.GetDownloadUrlYT(YoutubeApi.GetVideoCode(YoutubeLinkTextBox.Text), FormatComboBox.Text).ToString(), saveFileDialog.FileName, true, DownloadProgressBar);
                    }));
                DownloadProgressBar.Value = 0;
            }*/

            bool result = false;

            if (FormatComboBox.SelectedItem.ToString() == "mp3" && ReEncodeAudioCheckBox.Checked)
            {
                string tempAudioFile = Path.GetTempFileName();

                await youtube.Videos.Streams.DownloadAsync(streamInfo, tempAudioFile, new Progress<double>(p => DownloadProgressBar.Value = (int)(p * 1000)));

               result = await ChangeMediaExtension(tempAudioFile, saveFileDialog.FileName);
            }
            else if (FormatComboBox.SelectedItem.ToString() != "com")
            {
                await youtube.Videos.Streams.DownloadAsync(streamInfo, saveFileDialog.FileName, new Progress<double>(p => DownloadProgressBar.Value = (int)(p * 1000)));
            }
            else
            {
                if (!File.Exists(ffmpegTempPath))
                {
                    MessageBox.Show("ffmpeg does not exist and cant download combined videos", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string tempVideoFile = Path.GetTempFileName();
                string tempAudioFile = Path.GetTempFileName();

                AudioOnlyStreamInfo audioStreamInfo = audioStreamsInfo.ElementAt(AudioStreamsComboBox.SelectedIndex);

                long videoBytesSize = streamInfo.Size.Bytes;
                long audioBytesSize = audioStreamInfo.Size.Bytes;
                double totalDataSize = videoBytesSize + audioBytesSize;
                double videoPercentage = (videoBytesSize / totalDataSize) * 1000;
                double audioPercentage = (audioBytesSize / totalDataSize) * 1000;

                await youtube.Videos.Streams.DownloadAsync(streamInfo, tempVideoFile, new Progress<double>(p => DownloadProgressBar.Value = (int)(p * videoPercentage)));

                await youtube.Videos.Streams.DownloadAsync(audioStreamInfo, tempAudioFile, new Progress<double>(p => DownloadProgressBar.Value = (int)(videoPercentage + (p * audioPercentage))));

                result = await CombineAudioAndVideo(tempVideoFile, tempAudioFile, saveFileDialog.FileName);

                try
                {
                    File.Delete(tempAudioFile);
                    File.Delete(tempVideoFile);
                }
                catch { }

                if (!result)
                {
                    MessageBox.Show("An error has occured converting the video", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            MessageBox.Show("Media downloaded", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static readonly Regex removeInvalidChars = new Regex($"[{Regex.Escape(new string(Path.GetInvalidFileNameChars()))}]", RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public static string SanitizedFileName(string fileName, string replacement = "_")
        {
            return removeInvalidChars.Replace(fileName, replacement);
        }

        private void FormatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Config.Set("formatIndex", FormatComboBox.SelectedIndex.ToString());

            UpdateStreams();
        }

        private static string GetVideoID(string url) => url.Contains("v=") ? url.Split('?').Last().Split('&').First(s => s.StartsWith("v=", StringComparison.InvariantCultureIgnoreCase)).Split('=')[1] : url.Split('?')[0].Split('/').Last();

        private async void YoutubeLinkTextBox_TextChanged(object sender, EventArgs e)
        {
            DownloadButton.Enabled = false;

            try
            {
                YoutubeLinkTextBox.Text = GetVideoID(YoutubeLinkTextBox.Text);

                currentVideo = await youtube.Videos.GetAsync(YoutubeLinkTextBox.Text);

                AudioStreamsComboBox.Items.Clear();
                VideoStreamsComboBox.Items.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cannot find the video by the id\n\n" + ex.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (currentVideo != null)
            {
                Config.Set("lastVideo", YoutubeLinkTextBox.Text);

                currentStreamManifest = null;

                VideoNameLabel.Text = currentVideo.Title;

                foreach (var t in currentVideo.Thumbnails) Console.WriteLine(t.Url);

                var thumbnailUrl = currentVideo.Thumbnails.Where(c => !c.Url.Split('?')[0].EndsWith(".webp", StringComparison.InvariantCultureIgnoreCase)).OrderBy(c => c.Resolution.Width).Last().Url;

                currentStreamManifest = await youtube.Videos.Streams.GetManifestAsync(currentVideo.Id);

                audioStreamsInfo = currentStreamManifest.GetAudioOnlyStreams().OrderBy(c => c.Bitrate.BitsPerSecond);
                videoStreamsInfo = currentStreamManifest.GetVideoOnlyStreams().OrderBy(c => c.Bitrate.BitsPerSecond);
                muxedStreamsInfo = currentStreamManifest.GetMuxedStreams().OrderBy(c => c.Bitrate.BitsPerSecond);

                VideoFotoPictureBox.Image = Image.FromStream(await (new WebClient()).OpenReadTaskAsync(new Uri(thumbnailUrl)));

                await UpdateStreams();

                DownloadButton.Enabled = true;
            }
        }

        private static int oldVideoIndex = -1;
        private static int oldAudioIndex = -1;

        private async Task UpdateStreams()
        {
            if (currentVideo == null) return;

            if (FormatComboBox.SelectedItem == null || muxedStreamsInfo == null) return;

            VideoStreamsComboBox.Items.Clear();

            AudioStreamsComboBox.Items.Clear();

            foreach (var stream in audioStreamsInfo) AudioStreamsComboBox.Items.Add((int)stream.Bitrate.KiloBitsPerSecond + "k|" + stream.Size + "|" + stream.AudioCodec.ToUpper().Split('.')[0]);

            if (FormatComboBox.SelectedItem.ToString() == "mux")
            {
                foreach (var stream in muxedStreamsInfo) VideoStreamsComboBox.Items.Add(stream.VideoResolution + "|" + stream.Size + "|" + stream.VideoCodec.Split('.')[0]);
            }
            else
            {
                foreach (var stream in videoStreamsInfo) VideoStreamsComboBox.Items.Add(stream.VideoResolution + "|" + stream.Size + "|" + stream.VideoCodec.Split('.')[0]);
            }

            switch (FormatComboBox.SelectedItem.ToString())
            {
                case "mp3":
                    VideoStreamsComboBox.Enabled = false;
                    AudioStreamsComboBox.Enabled = true;

                    ReEncodeAudioCheckBox.Enabled  = true;
                    if (ReEncodeAudioCheckBox.Checked) AudioCodecsComboBox.Enabled = true;

                    ReEncodeVideoCheckBox.Enabled = VideoCodecsComboBox.Enabled = false;
                    break;

                case "mux":
                case "mp4":
                    VideoStreamsComboBox.Enabled = true;
                    AudioStreamsComboBox.Enabled = false;

                    ReEncodeAudioCheckBox.Enabled = AudioCodecsComboBox.Enabled = false;
                    ReEncodeVideoCheckBox.Enabled = VideoCodecsComboBox.Enabled = false;

                    break;

                case "com":
                    VideoStreamsComboBox.Enabled = AudioStreamsComboBox.Enabled = true;

                    ReEncodeAudioCheckBox.Enabled  = true;
                    if (ReEncodeAudioCheckBox.Checked) AudioCodecsComboBox.Enabled = true;

                    ReEncodeVideoCheckBox.Enabled = true;
                    if (ReEncodeVideoCheckBox.Checked) VideoCodecsComboBox.Enabled = true;
                    break;
            }

            if (oldAudioIndex >= 0 && oldAudioIndex <= AudioStreamsComboBox.Items.Count)
            {
                AudioStreamsComboBox.SelectedIndex = oldAudioIndex;
            }
            else
            {
                AudioStreamsComboBox.SelectedIndex = AudioStreamsComboBox.Items.Count - 1;
            }

            if (oldVideoIndex >= 0 && oldVideoIndex <= VideoStreamsComboBox.Items.Count)
            {
                VideoStreamsComboBox.SelectedIndex = oldVideoIndex;
            }
            else
            {
                VideoStreamsComboBox.SelectedIndex = VideoStreamsComboBox.Items.Count - 1;
            }
        }

        private void VideoFotoPictureBox_Click(object sender, EventArgs e)
        {
            if (VideoFotoPictureBox.Image != null)
            {
                Clipboard.SetImage(VideoFotoPictureBox.Image);
            }
        }

        private static string ffmpegTempPath = Path.Combine(Path.GetTempPath(), "ffmpeg.exe");

        private async Task CheckAndDownloadffmpeg()
        {
            if (!File.Exists(ffmpegTempPath))
            {
                if (MessageBox.Show("FFMPEG is not downloaded you want to download it to temp files?", Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                {
                    FormatComboBox.SelectedIndex++;
                    Environment.Exit(0);
                }

                Dictionary<Control, bool> originalValues = new Dictionary<Control, bool>();

                foreach (Control c in this.Controls)
                {
                    if (c.Name == DownloadProgressBar.Name) continue;

                    originalValues.Add(c, c.Enabled);
                    c.Enabled = false;
                }

                await wc.DownloadFileTaskAsync("https://raw.githubusercontent.com/Mrgaton/ffmpegDownload/main/ffmpeg-7.0-full_build/ffmpeg.exe", ffmpegTempPath);

                foreach (var dic in originalValues) dic.Key.Enabled = dic.Value;
            }
        }
        private void YoutubeForm_Shown(object sender, EventArgs e) => CheckAndDownloadffmpeg();

        private static string defaultffmpegArgs = "-map_metadata -1";

        public static Dictionary<string, string> videoCodecs = new Dictionary<string, string>()
        {
            { "AVC", "libx264" },
            { "MPEG-4", "mpeg4" },
            { "FLV1", "flv" },
            { "VP9 ⭐", "libvpx-vp9" },
            { "VP8", "libvpx" },
            { "HEVC ⭐", "libx265 -preset slow" },
            { "AV1 ⭐", "libsvtav1 -preset 6" },
            { "XVID", "libxvid -q:v 5 -q:a 4" },
        };

        public static Dictionary<string, string> audioCodecs = new Dictionary<string, string>()
        {
            { "WAV","pcm_s16le" },
            { "FLAC","flac -sample_fmt s16 -compression_level 8" },
            { "AAC ⭐","aac" },
            { "OPUS ⭐","opus -strict -2 -vbr on -compression_level 10" },
            { "OGG","libvorbis" },
            { "MP3","libmp3lame" }
        };

        private static Process StartConsole()
        {
            Process proc = Process.Start(new ProcessStartInfo()
            {
                FileName = Program.processPath,
                Arguments = "console",
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = false,
                CreateNoWindow = false
            });

            return proc;
        }

        private async Task<bool> CombineAudioAndVideo(string videoPath, string audioPath, string outputFile)
        {
            string videoCodec = (ReEncodeVideoCheckBox.Checked ? videoCodecs[VideoCodecsComboBox.Items[VideoCodecsComboBox.SelectedIndex].ToString()] : "copy");
            string audioCodec = (ReEncodeAudioCheckBox.Checked ? audioCodecs[AudioCodecsComboBox.Items[AudioCodecsComboBox.SelectedIndex].ToString()] : "copy");
            //string reference = "ffmpeg.exe -i \"video.mp4\" -i \"audio.mp3\" -c:a copy -c:v copy -n oeut.mp4";

            ProcessStartInfo processStartInfo = new ProcessStartInfo()
            {
                FileName = ffmpegTempPath,

                Arguments = $"-hwaccel auto -hide_banner -nostdin -i \"{videoPath}\" -i \"{audioPath}\" " 
                + defaultffmpegArgs 
                + $" -strict -2 -c:a {audioCodec} -c:v {videoCodec} -y \"{outputFile}\"",

                CreateNoWindow = true,
                //RedirectStandardInput = true,
                RedirectStandardError = true,
                RedirectStandardOutput = false,
                UseShellExecute = false
            };

            var console = StartConsole();
            console.StandardInput.WriteLine("Converting media please wait");
            StringBuilder output = new StringBuilder();
            var proc = Process.Start(processStartInfo);
            while (!proc.StandardError.EndOfStream)
            {
                string line = proc.StandardError.ReadLine();
                output.AppendLine(line);
                if (console.HasExited) proc.Kill();
                console.StandardInput.WriteLine(line);
            }
            await proc.WaitForExitAsync();
            console.Kill();

            if (proc.ExitCode != 0) ShowConversionError(output.ToString());



            return proc.ExitCode == 0;
        }

        private async Task<bool> ChangeMediaExtension(string mediaPath, string outputPath)
        {
            //string reference = "ffmpeg.exe -i \"video.mp4\" -i \"audio.mp3\" -c:a copy -c:v copy -n oeut.mp4";

            string audioCodec = (ReEncodeAudioCheckBox.Checked ? audioCodecs[AudioCodecsComboBox.Items[AudioCodecsComboBox.SelectedIndex].ToString()] : "copy");

            ProcessStartInfo processStartInfo = new ProcessStartInfo()
            {
                FileName = ffmpegTempPath,

                Arguments = $"-hide_banner -nostdin -i \"{mediaPath}\" {defaultffmpegArgs} -c:a {audioCodec} -y \"{outputPath}\"",

                CreateNoWindow = true,
                //RedirectStandardInput = true,
                RedirectStandardError = true,
                RedirectStandardOutput = false,
                UseShellExecute = false
            };

            Clipboard.SetText(processStartInfo.Arguments);

            var console = StartConsole();
            console.StandardInput.WriteLine("Converting media please wait");
            StringBuilder output = new StringBuilder();
            var proc = Process.Start(processStartInfo);
            while (!proc.StandardError.EndOfStream)
            {
                string line = proc.StandardError.ReadLine();
                output.AppendLine(line);
                if (console.HasExited) proc.Kill();
                console.StandardInput.WriteLine(line);
            }
            await proc.WaitForExitAsync();
            console.Kill();

            if (proc.ExitCode != 0) ShowConversionError(output.ToString());

            return proc.ExitCode == 0;
        }
        private static void ShowConversionError(string data)
        {
            Clipboard.SetText(data);

            MessageBox.Show("An error has ocured converting video\n\n" + data, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void VideoStreamsComboBox_SelectedIndexChanged(object sender, EventArgs e) => oldVideoIndex = VideoStreamsComboBox.SelectedIndex;

        private void AudioStreamsComboBox_SelectedIndexChanged(object sender, EventArgs e) => oldAudioIndex = AudioStreamsComboBox.SelectedIndex;

        private void ReEncodeAudioCheckBox_CheckedChanged(object sender, EventArgs e) => Config.Set("ReEncodeAudio", (AudioCodecsComboBox.Enabled = ReEncodeAudioCheckBox.Checked).ToString());
        private void ReEncodeVideoCheckBox_CheckedChanged(object sender, EventArgs e) => Config.Set("ReEncodeVideo", (VideoCodecsComboBox.Enabled = ReEncodeVideoCheckBox.Checked).ToString());

        // old crapy code
        /*private async void DownloadFileWithProgress(string DownloadLink, string PathDe, bool WithLabel, ProgressBar LAbelsita)
        {
            DownloadButton.Text = "Descargando";
            WriteLine("Descargando Archivo", false);

            int bytesProcessed = 0;
            Stream remoteStream = null;
            Stream localStream = null;
            WebResponse response = null;

            var sw = Stopwatch.StartNew();
            long AntiguoTiempo = 0;
            int LastPercent = -1;

            string TargetPath = GetFreePath(Path.GetTempPath(), GenerateRandomChars(30) + ".exe", 50);
            try
            {
                WebRequest request = WebRequest.Create(DownloadLink);
                if (request != null)
                {
                    double TotalBytesToReceive = 0;
                    var SizewebRequest = HttpWebRequest.Create(new Uri(DownloadLink));
                    SizewebRequest.Method = "HEAD";

                    using (var webResponse = SizewebRequest.GetResponse())
                    {
                        var fileSize = webResponse.Headers.Get("Content-Length");

                        WriteLine("Descargando desde los servidores de " + webResponse.Headers.Get("Server"), false);

                        WriteLine("Archivo Suvido a los servidores a las " + webResponse.Headers.Get("Last-Modified"), false);

                        if (fileSize != null)
                        {
                            WriteLine("Peso del archivo " + fileSize.ToString(), false);
                            TotalBytesToReceive = Convert.ToDouble(fileSize);
                        }
                        else
                        {
                            WriteLine("Peso del archivo desconocido", false);
                            TotalBytesToReceive = Convert.ToDouble(-1);
                        }
                    }

                    response = request.GetResponse();
                    if (response != null)
                    {
                        remoteStream = response.GetResponseStream();
                        localStream = File.Create(TargetPath);
                        byte[] buffer = new byte[1024];
                        int bytesRead = 0;
                        do
                        {
                            bytesRead = remoteStream.Read(buffer, 0, buffer.Length);
                            localStream.Write(buffer, 0, bytesRead);
                            bytesProcessed += bytesRead;
                            double bytesIn = double.Parse(bytesProcessed.ToString());
                            double percentage = Math.Round(bytesIn / TotalBytesToReceive * 100, 0);

                            int Porcentaje = int.Parse(Math.Truncate(percentage).ToString());

                            if (LastPercent != Porcentaje)
                            {
                                sw.Stop();

                                LastPercent = Porcentaje;
                                long MilisegundosRestantes = (((100 - Porcentaje) * sw.ElapsedMilliseconds) + AntiguoTiempo) / 2;

                                AntiguoTiempo = MilisegundosRestantes;
                                Log("Descargando datos " + MilisegundosToDate(MilisegundosRestantes) + " " + Porcentaje.ToString() + "% Completado");
                                if (WithLabel & Porcentaje >= 0)
                                {
                                    LAbelsita.Value = Porcentaje;
                                    LAbelsita.Maximum = 100;
                                    this.Refresh();
                                }
                                sw.Restart();
                            }
                        } while (bytesRead > 0);
                    }
                }
            }
            catch (Exception ex)
            {
                Error(ex.ToString());
            }
            finally
            {
                if (response != null) response.Close();
                if (remoteStream != null) remoteStream.Close();
                if (localStream != null) localStream.Close();

                if (File.Exists(PathDe))
                {
                    File.Delete(PathDe);
                }

                File.Move(TargetPath, PathDe);

                if (File.Exists(TargetPath))
                {
                    File.Delete(TargetPath);
                }
                DownloadButton.Text = "Descargar";
                DownloadButton.Enabled = true;
            }
        }
        public static string WriteLine(string message, bool type)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("]  ");

            if (!type)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(AsemblyName);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("] " + message + "\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(AsemblyName);
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("] " + message + "\n");
            }
            return null;
        }
        public static string GetFreePath(string Directory, string FileName, int pum)
        {
            ApiWriteLine("Consiguiendo path libre", true);
            if (!Directory.EndsWith("\\"))
            {
                Directory = Directory + "\\";
            }
            bool exist = File.Exists(Path.Combine(Directory + FileName));
            var Filee = Path.Combine(Directory + FileName);
            while (exist)
            {
                if (exist == true)
                {
                    Filee = Path.Combine(Directory + GenerateRandomChars(pum) + ".exe");
                    exist = File.Exists(Filee);
                    Thread.Sleep(50);
                }
            }
            return Filee;
        }
        public static string ApiWriteLine(string message, bool type)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("]  ");

            if (!type)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(AsemblyName + " SClass");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("] " + message + "\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(AsemblyName + " DClass");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("] " + message + "\n");
            }
            return null;
        }
        public static string Log(string message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("+");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("] " + message);

            if (!message.EndsWith("\n"))
            {
                Console.Write("\n");
            }
            return null;
        }
        public static string Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("+");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("] " + message);

            if (!message.EndsWith("\n"))
            {
                Console.Write("\n");
            }
            return null;
        }
        public static string MilisegundosToDate(long Time)
        {
            string Date = "Milisegundos";
            if (Time >= 1000)
            {
                Time = Time / 1000;

                if (Time > 1)
                {
                    Date = "Segundos";
                }
                else
                {
                    Date = "Segundo";
                }

                if (Time > 60)
                {
                    Time = Time / 60;

                    if (Time > 1)
                    {
                        Date = "Minutos";
                    }
                    else
                    {
                        Date = "Minuto";
                    }

                    if (Time >= 60)
                    {
                        Time = Time / 60;

                        if (Time > 1)
                        {
                            Date = "Horas";
                        }
                        else
                        {
                            Date = "Hora";
                        }

                        if (Time >= 24)
                        {
                            Time = Time / 24;
                            if (Time > 1)
                            {
                                Date = "Dias";
                            }
                            else
                            {
                                Date = "Dia";
                            }
                        }
                    }
                }
            }

            return (Time.ToString() + " " + Date + " Restantes");
        }

        public static string GenerateRandomChars(int Chars)
        {
            ApiWriteLine("Generando caracteres aleatorios", true);
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[Chars];
            var random = new Random();
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            return new string(stringChars);
        }

        private void YoutubeForm_Load(object sender, EventArgs e)
        {
        }

        private void BitrateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }*/
    }
}