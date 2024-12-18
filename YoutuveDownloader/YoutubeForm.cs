﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Youtube_downloader;
using YoutubeDownloader.Helper;
using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.ClosedCaptions;
using YoutubeExplode.Videos.Streams;

namespace Youtuve_Downloader
{
    public partial class YouTubeForm : Form
    {
        private static WebClient wc = new WebClient();

        public static YoutubeClient youtube = new YoutubeClient(new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip}), GetCookies());
        private static IReadOnlyList<Cookie>? GetCookies()
        {
            string cookiesFile = "cookies.txt";

            if (File.Exists(cookiesFile))
            {
                return File.ReadAllText(cookiesFile).Trim(['\'','"']).Split(';').Select(el => el.Split('=')).Select(c => new Cookie(c[0], string.Join("=",c.Skip(1)))) as IReadOnlyList<Cookie>;
            }

            return null;
        }
        public static Video CurrentVideo { get; set; }
        public static StreamManifest CurrentStreamManifest { get; set; }

        public static IEnumerable<AudioOnlyStreamInfo> AdioStreamsInfo { get; set; }
        public static IEnumerable<VideoOnlyStreamInfo> VideoStreamsInfo { get; set; }
        public static IEnumerable<ClosedCaptionTrackInfo> TrackInfos { get; set; }

        public static readonly string asemblyName = Assembly.GetEntryAssembly().GetName().Name.ToString();

        public static readonly Dictionary<string, string> arguments = [];

        private IntPtr nextClipboardViewer;

        public MediaType FormatComboBoxMediaType { get => (MediaType)FormatComboBox.SelectedIndex; }

        public enum MediaType
        {
            COM = 0,
            MP3 = 1,
            MP4 = 2,
            //MUX = 3
        }

        public YouTubeForm(string[] args)
        {
            nextClipboardViewer = (IntPtr)SetClipboardViewer((int)this.Handle);

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
                if (bool.TryParse(Config.Get("ReEncodeVideo"), out bool videoReEncode)) VideoCodecsComboBox.Enabled = FpsUpDown.Enabled = ReEncodeVideoCheckBox.Checked = videoReEncode;
                if (bool.TryParse(Config.Get("Minterpolate"), out bool Minterpolate)) MinterpolateCheckBox.Checked = Minterpolate;
                if (bool.TryParse(Config.Get("Metadata"), out bool Metadata)) MetadataCheckBox.Checked = Metadata;
            }

            wc.DownloadProgressChanged += (s, e) => DownloadProgressBar.Value = e.ProgressPercentage * 10;
            wc.DownloadFileCompleted += (s, e) => DownloadProgressBar.Value = 0;

            foreach (var codec in audioCodecs) AudioCodecsComboBox.Items.Add(codec.Key);
            foreach (var codec in videoCodecs) VideoCodecsComboBox.Items.Add(codec.Key);

            if (int.TryParse(Config.Get("videoCodec"), out int videoCodecIndex))
            {
                VideoCodecsComboBox.SelectedIndex = videoCodecIndex;
            }
            else
            {
                this.VideoCodecsComboBox.SelectedIndex = 5;
            }

            if (int.TryParse(Config.Get("audioCodec"), out int audioCodecIndex))
            {
                AudioCodecsComboBox.SelectedIndex = audioCodecIndex;
            }
            else
            {
                this.AudioCodecsComboBox.SelectedIndex = 2;
            }

            if (args.Length == 0)
            {
                string clipboard = Clipboard.GetText();

                if (IsYoutubeID(clipboard))
                {
                    YoutubeLinkTextBox.Text = clipboard;
                }
            }

            ClipboardChanged += (object e, ClipboardChangedEventArgs i) =>
            {
                if (i.DataObject.GetDataPresent(DataFormats.Text))
                {
                    string clipboardText = ((string)i.DataObject.GetData(DataFormats.Text)).Trim();

                    if (IsYoutubeID(clipboardText))
                    {
                        YoutubeLinkTextBox.Text = clipboardText;
                    }
                }
            };

            if (args.Length == 0)
            {
                string clipboard = Clipboard.GetText().Trim();

                if (IsYoutubeID(clipboard))
                {
                    YoutubeLinkTextBox.Text = clipboard;
                }
            }
        }

        private void YoutubeLinkTextBox_DoubleClick(object sender, EventArgs e) => YoutubeLinkTextBox.SelectAll();

        public static readonly Regex AlphanumericRegex = new Regex("[^a-zA-Z0-9 -]");

        private async void DownloadButton_Click(object sender, EventArgs e)
        {
            if (CurrentVideo == null) return;

            dynamic streamInfo = null;

            string fileExtension = string.Empty;

            switch (FormatComboBoxMediaType)
            {
                case MediaType.MP3:
                    streamInfo = AdioStreamsInfo.ElementAt(AudioStreamsComboBox.SelectedIndex);
                    fileExtension = "mp3";
                    break;

                case MediaType.COM:
                case MediaType.MP4:
                    streamInfo = VideoStreamsInfo.ElementAt(VideoStreamsComboBox.SelectedIndex);
                    fileExtension = "mp4";
                    break;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();

            if (FormatComboBoxMediaType == MediaType.MP3)
            {
                string extension = fileExtension;

                if (ReEncodeAudioCheckBox.Checked) extension = fileExtension = AlphanumericRegex.Replace(AudioCodecsComboBox.Items[AudioCodecsComboBox.SelectedIndex].ToString().ToLower(), "").Trim();

                saveFileDialog.Filter = extension + " Audio | *." + fileExtension;
            }
            else if (FormatComboBoxMediaType == MediaType.MP4 || FormatComboBoxMediaType == MediaType.COM)
            {
                saveFileDialog.Filter = "MP4 Video|*.mp4|MKV Video|*.mkv|AVI Video|*.avi|MOV Video|*.mov";
            }

            saveFileDialog.Title = "Interesting question";
            saveFileDialog.FileName = SanitizedFileName(CurrentVideo.Title + "_" + (int)((streamInfo).Bitrate.KiloBitsPerSecond) + "." + fileExtension);

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

            if (FormatComboBoxMediaType == MediaType.MP3 && ReEncodeAudioCheckBox.Checked)
            {
                string tempAudioFile = Path.GetTempFileName();

                RemoveOnBoot(tempAudioFile);

                await youtube.Videos.Streams.DownloadAsync(streamInfo, tempAudioFile, new Progress<double>(p => DownloadProgressBar.Value = (int)(p * 1000)));

                result = await ChangeMediaExtension(tempAudioFile, saveFileDialog.FileName);

                try
                {
                    File.Delete(tempAudioFile);
                }
                catch { }
            }
            else if (FormatComboBoxMediaType != MediaType.COM)
            {
                await youtube.Videos.Streams.DownloadAsync(streamInfo, saveFileDialog.FileName, new Progress<double>(p => DownloadProgressBar.Value = (int)(p * 1000)));
            }
            else
            {
                if (!File.Exists(ffmpegTempPath))
                {
                    MessageBox.Show("FFMPEG does not exist and cant download combined videos", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                AudioOnlyStreamInfo audioStreamInfo = AdioStreamsInfo.ElementAt(AudioStreamsComboBox.SelectedIndex);

                string tempPath = Path.GetTempPath();
                string tempVideoFile = Path.Combine(tempPath, ((VideoOnlyStreamInfo)streamInfo).Url.Hash() + ".mp4");
                string tempAudioFile = Path.Combine(tempPath, audioStreamInfo.Url.Hash() + ".mp3");

                RemoveOnBoot(tempVideoFile);
                RemoveOnBoot(tempAudioFile);

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
                    MessageBox.Show("An error has occurred converting the video", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private static bool IsYoutubeID(string url)
        {
            return url.StartsWith("http", StringComparison.InvariantCultureIgnoreCase) && url.Contains("://") && url.ToLower().Contains("youtu");
        }

        private static string GetVideoID(string url) => url.Contains("v=") ? url.Split('?').Last().Split('&').First(s => s.StartsWith("v=", StringComparison.InvariantCultureIgnoreCase)).Split('=')[1] : url.Split('?')[0].Split('/').Last();

        private static string LastVideo { get; set; } = string.Empty;

        private static Dictionary<string, int> CodecOrder = new Dictionary<string, int>()
        {
            {"av01", 1 },
            {"vp9", 2 },
            {"avc1", 3},
        };

        private async void YoutubeLinkTextBox_TextChanged(object sender, EventArgs e)
        {
            DownloadButton.Enabled = false;

            try
            {
                YoutubeLinkTextBox.Text = YoutubeLinkTextBox.Text.Trim();

                if (YoutubeLinkTextBox.Text == LastVideo) return;

                LastVideo = YoutubeLinkTextBox.Text;

                YoutubeLinkTextBox.Text = GetVideoID(YoutubeLinkTextBox.Text);

                CurrentVideo = await youtube.Videos.GetAsync(YoutubeLinkTextBox.Text);

                StartTextBox.Text = "00:00";
                videoDuration = (TimeSpan)CurrentVideo.Duration;
                EndTextBox.Text = videoDuration.Hours == 0 ? videoDuration.ToString(@"mm\:ss") : videoDuration.ToString(@"hh\:mm\:ss");

                AudioStreamsComboBox.Items.Clear();
                VideoStreamsComboBox.Items.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cannot find the video by the id\n\n" + ex.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (CurrentVideo != null)
            {
                Config.Set("lastVideo", YoutubeLinkTextBox.Text);

                CurrentStreamManifest = null;

                Task.Factory.StartNew(() =>
                {
                    Invoke((MethodInvoker)(async () => await UpdateVideoInfoAsync()));
                });

                CurrentStreamManifest = await youtube.Videos.Streams.GetManifestAsync(CurrentVideo.Id);

                AdioStreamsInfo = CurrentStreamManifest.GetAudioOnlyStreams().OrderByDescending(c => c.Bitrate.BitsPerSecond);

                VideoStreamsInfo = CurrentStreamManifest.GetVideoOnlyStreams().OrderByDescending(c => c.VideoResolution.Height).ThenBy(c =>
                {
                    string codec = c.VideoCodec.Split('.')[0].ToLower();

                    if (CodecOrder.TryGetValue(codec, out int order))
                        return order;

                    return 0;
                });

                UpdateStreams();

                var captionsManifest = await youtube.Videos.ClosedCaptions.GetManifestAsync(CurrentVideo.Id);

                TrackInfos = captionsManifest.Tracks;

                foreach (var t in TrackInfos)
                {
                    //Clipboard.SetText(t.Url);
                    Console.Write(t.Language.ToString() + ':');
                    Console.WriteLine(t.Url);
                    Console.WriteLine();
                }

                DownloadButton.Enabled = true;
            }
        }

        private async Task UpdateVideoInfoAsync()
        {
            VideoNameLabel.Text = CurrentVideo.Title;

            using (WebClient client = new WebClient())
            {
                try
                {
                    using (Stream stream = await client.OpenReadTaskAsync(new Uri($"https://i.ytimg.com/vi/{CurrentVideo.Id}/maxresdefault.jpg")))
                    {
                        VideoFotoPictureBox.Image = Image.FromStream(stream);
                    }
                }
                catch
                {
                    using (Stream stream = await client.OpenReadTaskAsync(new Uri(CurrentVideo.Thumbnails.First(url => url.Url.EndsWith(".jpg", StringComparison.InvariantCultureIgnoreCase)).Url)))
                    {
                        VideoFotoPictureBox.Image = Image.FromStream(stream);
                    }
                }
            }
        }

        private int oldVideoIndex = 0;
        private int oldAudioIndex = 0;

        public static string PrettifyCodecs(string codec)
        {
            return codec.Replace("av01", "AV1").Replace("avc1", "AVC").Replace("vp09", "VP9").ToUpper();
        }

        private async void UpdateStreams()
        {
            if (CurrentVideo == null) return;
            if (FormatComboBox.SelectedItem == null) return;

            VideoStreamsComboBox.Items.Clear();
            AudioStreamsComboBox.Items.Clear();

            foreach (var stream in AdioStreamsInfo) AudioStreamsComboBox.Items.Add((int)stream.Bitrate.KiloBitsPerSecond + "k|" + stream.Size + "|" + stream.AudioCodec.ToUpper().Split('.')[0]);

            foreach (var stream in VideoStreamsInfo) VideoStreamsComboBox.Items.Add(stream.VideoResolution + "|" + stream.Size + "|" + PrettifyCodecs(stream.VideoCodec.Split('.')[0]));

            switch (FormatComboBoxMediaType)
            {
                case MediaType.MP3:
                    StartTextBox.Enabled = EndTextBox.Enabled = ReEncodeAudioCheckBox.Checked;

                    MinterpolateCheckBox.Enabled = FpsUpDown.Enabled = false;

                    VideoStreamsComboBox.Enabled = false;
                    AudioStreamsComboBox.Enabled = true;

                    ReEncodeAudioCheckBox.Enabled = true;

                    if (ReEncodeAudioCheckBox.Checked) AudioCodecsComboBox.Enabled = true;

                    ReEncodeVideoCheckBox.Enabled = VideoCodecsComboBox.Enabled = false;

                    MetadataCheckBox.Enabled = ReEncodeAudioCheckBox.Checked;
                    break;

                case MediaType.MP4:
                    MinterpolateCheckBox.Enabled = FpsUpDown.Enabled = StartTextBox.Enabled = EndTextBox.Enabled = false;

                    VideoStreamsComboBox.Enabled = true;
                    AudioStreamsComboBox.Enabled = false;

                    ReEncodeAudioCheckBox.Enabled = AudioCodecsComboBox.Enabled = false;
                    ReEncodeVideoCheckBox.Enabled = VideoCodecsComboBox.Enabled = false;
                    break;

                case MediaType.COM:
                    StartTextBox.Enabled = EndTextBox.Enabled = true;
                    MinterpolateCheckBox.Enabled = FpsUpDown.Enabled = ReEncodeVideoCheckBox.Checked;

                    VideoStreamsComboBox.Enabled = AudioStreamsComboBox.Enabled = true;

                    ReEncodeAudioCheckBox.Enabled = true;
                    if (ReEncodeAudioCheckBox.Checked) AudioCodecsComboBox.Enabled = true;

                    ReEncodeVideoCheckBox.Enabled = true;
                    if (ReEncodeVideoCheckBox.Checked) VideoCodecsComboBox.Enabled = true;

                    MetadataCheckBox.Enabled = true;
                    break;
            }

            if (oldAudioIndex >= 0 && oldAudioIndex < AudioStreamsComboBox.Items.Count)
            {
                AudioStreamsComboBox.SelectedIndex = oldAudioIndex;
            }
            else
            {
                AudioStreamsComboBox.SelectedIndex = 0;
            }

            if (oldVideoIndex >= 0 && oldVideoIndex < VideoStreamsComboBox.Items.Count)
            {
                VideoStreamsComboBox.SelectedIndex = oldVideoIndex;
            }
            else
            {
                VideoStreamsComboBox.SelectedIndex = 0;
            }
        }

        private void VideoFotoPictureBox_Click(object sender, EventArgs e)
        {
            if (VideoFotoPictureBox.Image != null)
            {
                Clipboard.SetImage(VideoFotoPictureBox.Image);
            }
        }

        private static string ffmpegTempPath = Path.Combine(Path.GetTempPath(), "ffmpeg-7.0.2.exe");

        private async Task CheckAndDownloadffmpeg()
        {
            this.Focus();

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

                try
                {
                    await wc.DownloadFileTaskAsync($"https://raw.githubusercontent.com/Mrgaton/ffmpegDownload/main/{Path.GetFileNameWithoutExtension(ffmpegTempPath)}-full_build/ffmpeg.exe", ffmpegTempPath);

                    foreach (var dic in originalValues) dic.Key.Enabled = dic.Value;

                    DownloadButton.Enabled = true;
                }
                catch (Exception ex)
                {
                    if (File.Exists(ffmpegTempPath))
                    {
                        File.Delete(ffmpegTempPath);
                    }

                    MessageBox.Show("There was an error downloading FFMPEG.\n\n" + ex.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                }
            }
        }

        private void YouTubeForm_Shown(object sender, EventArgs e) => CheckAndDownloadffmpeg();

        public static readonly Dictionary<string, string> videoCodecs = new Dictionary<string, string>()
        {
            { "AV1 ⭐", "libsvtav1 -preset 1 -crf 32 -g 240 -pix_fmt yuv420p10le -svtav1-params tune=0:film-grain=8" },
            //{ "VVC", "libvvenc -qp 27 -preset slow" },
            //{ "FLV1", "flv" },
            { "VP9 ⭐", "libvpx-vp9 -crf 30" },
            { "HEVCN ⭐", "hevc_nvenc -preset p7 -rc:v vbr" },
            { "HEVC ⭐", "libx265 -crf 28 -preset slow -threads " + Environment.ProcessorCount},

            //{ "VP8", "libvpx" },
            { "AVC", "libx264" },
            { "MPEG-4", "mpeg4" },
            { "XVID", "libxvid -q:v 5 -q:a 4" },
        };

        public static readonly Dictionary<string, string> audioCodecs = new Dictionary<string, string>()
        {
            { "WAV","pcm_s16le" },
            { "FLAC","flac -sample_fmt s16 -compression_level 8" },
            { "AAC ⭐","aac" },
            { "OPUS ⭐","opus -strict -2 -vbr on -compression_level 10 -frame_duration 60" },
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

        public static async Task<string> CreateSubtitlesFFMPEGCommand()
        {
            Dictionary<string, string> srts = new Dictionary<string, string>();

            foreach (var track in TrackInfos)
            {
                try
                {
                    string filePath = Path.Combine(Path.GetTempPath(), ((uint)track.Url.GetHashCode()) + ".srt");

                    string subtitlesData = SubRipSubtitleConvertor.XmlToSrt(Encoding.UTF8.GetString(await wc.DownloadDataTaskAsync(track.Url)));

                    //Console.WriteLine(subtitlesData);

                    File.WriteAllText(filePath, subtitlesData, Encoding.UTF8);
                    RemoveOnBoot(filePath);
                    srts.Add(filePath, track.Language.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            StringBuilder sb = new StringBuilder();

            int index = 0;

            foreach (var srt in srts)
            {
                sb.Append($" -i \"{srt.Key}\"");
            }

            sb.Append($" -map 0 -map 1" + string.Join(" ", Enumerable.Range(2, srts.Count).Select(i => $" -map {i}")));

            foreach (var srt in srts)
            {
                sb.Append($" -c:s:{index} srt -metadata:s:s:{index++} language=\"{srt.Value}\"");
            }

            return sb.ToString();
        }

        private async Task<bool> CombineAudioAndVideo(string videoPath, string audioPath, string outputFile)
        {
            string videoCodec = (ReEncodeVideoCheckBox.Checked ? videoCodecs[VideoCodecsComboBox.Items[VideoCodecsComboBox.SelectedIndex].ToString()] : "copy");
            string audioCodec = (ReEncodeAudioCheckBox.Checked ? audioCodecs[AudioCodecsComboBox.Items[AudioCodecsComboBox.SelectedIndex].ToString()] : "copy");
            //string reference = "ffmpeg.exe -i \"video.mp4\" -i \"audio.mp3\" -c:a copy -c:v copy -n oeut.mp4";

            ProcessStartInfo processStartInfo = new ProcessStartInfo()
            {
                FileName = ffmpegTempPath,

                Arguments = $"-hwaccel auto -hide_banner -nostdin -i \"{audioPath}\" -i \"{videoPath}\""
                + (SubtitlesCheckbox.Checked ? await CreateSubtitlesFFMPEGCommand() : null)

                + (MetadataCheckBox.Checked ? $" -metadata title=\"{Program.RemoveSpecificNonAlphanumeric(CurrentVideo.Title)}\" -metadata author=\"{CurrentVideo.Author.ChannelTitle}\"" : " -map_metadata -1")

                + (tarjetStartTimeSpan.TotalSeconds > 0 ? " -ss " + StartTextBox.Text : null)
                + (tarjetEndTimeSpan.TotalSeconds < videoDuration.TotalSeconds ? " -to " + EndTextBox.Text : null)
                + (MinterpolateCheckBox.Enabled && MinterpolateCheckBox.Checked ? " -vf \"minterpolate=mi_mode=mci:mc_mode=aobmc:me_mode=bidir\"" : null)
                + (FpsUpDown.Enabled && FpsUpDown.Value != videoQuality.Value.Framerate ? " -r " + FpsUpDown.Value : null)
                + (outputFile.EndsWith(".mp4", StringComparison.InvariantCultureIgnoreCase) ? " -c:s " + (outputFile.EndsWith(".mp4") ? "mov_text" : "copy") : null)
                + $" -strict -2 -c:a {audioCodec} -c:v {videoCodec} -y \"{outputFile}\"",

                CreateNoWindow = true,
                //RedirectStandardInput = true,
                RedirectStandardError = true,
                RedirectStandardOutput = false,
                UseShellExecute = false
            };

            Console.WriteLine("Running ffmpeg with commands: " + processStartInfo.Arguments);

            Process console = StartConsole();

            console.StandardInput.WriteLineAsync("Converting media please wait");

            StringBuilder output = new StringBuilder();
            Process proc = Process.Start(processStartInfo);
            proc.PriorityClass = ProcessPriorityClass.Idle;

            while (!proc.StandardError.EndOfStream)
            {
                string line = await proc.StandardError.ReadLineAsync();
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

                Arguments = $"-hide_banner -nostdin -i \"{mediaPath}\" -map_metadata -1"

                + (MetadataCheckBox.Checked ? $" -metadata title=\"{Program.RemoveSpecificNonAlphanumeric(CurrentVideo.Title)}\" -metadata author=\"{CurrentVideo.Author.ChannelTitle}\"" : null)
                + (tarjetStartTimeSpan.TotalSeconds > 0 ? " -ss " + StartTextBox.Text : null)
                + (tarjetEndTimeSpan.TotalSeconds < videoDuration.TotalSeconds ? " -to " + EndTextBox.Text : null)

                + $" -c:a {audioCodec} -y \"{outputPath}\"",

                CreateNoWindow = true,
                //RedirectStandardInput = true,
                RedirectStandardError = true,
                RedirectStandardOutput = false,
                UseShellExecute = false
            };

            Clipboard.SetText(processStartInfo.Arguments);

            var console = StartConsole();
            await console.StandardInput.WriteLineAsync("Converting media please wait");
            StringBuilder output = new StringBuilder();
            var proc = Process.Start(processStartInfo);
            proc.PriorityClass = ProcessPriorityClass.Idle;

            while (!proc.StandardError.EndOfStream)
            {
                string line = await proc.StandardError.ReadLineAsync();
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

            MessageBox.Show("An error has occurred converting video\n\n" + data, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void AudioStreamsComboBox_SelectedIndexChanged(object sender, EventArgs e) => oldAudioIndex = AudioStreamsComboBox.SelectedIndex;

        private void VideoStreamsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            oldVideoIndex = VideoStreamsComboBox.SelectedIndex;

            videoQuality = VideoStreamsInfo.ElementAt(VideoStreamsComboBox.SelectedIndex).VideoQuality;
            FpsUpDown.Value = FpsUpDown.Maximum = MinterpolateCheckBox.Checked ? videoQuality.Value.Framerate * 2 : videoQuality.Value.Framerate;
        }

        private void ReEncodeAudioCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Config.Set("ReEncodeAudio", (AudioCodecsComboBox.Enabled = ReEncodeAudioCheckBox.Checked).ToString());

            if (FormatComboBoxMediaType == MediaType.MP3)
            {
                StartTextBox.Enabled = EndTextBox.Enabled = MetadataCheckBox.Enabled = ReEncodeAudioCheckBox.Checked;
            }
        }

        private void ReEncodeVideoCheckBox_CheckedChanged(object sender, EventArgs e) => Config.Set("ReEncodeVideo", (MinterpolateCheckBox.Enabled = VideoCodecsComboBox.Enabled = FpsUpDown.Enabled = ReEncodeVideoCheckBox.Checked).ToString());

        public event EventHandler<ClipboardChangedEventArgs> ClipboardChanged;

        [DllImport("User32.dll")] protected static extern int SetClipboardViewer(int hWndNewViewer);

        [DllImport("User32.dll", CharSet = CharSet.Auto)] public static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

        [DllImport("user32.dll", CharSet = CharSet.Auto)] public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        protected override void WndProc(ref Message m)
        {
            const int WM_DRAWCLIPBOARD = 0x308;
            const int WM_CHANGECBCHAIN = 0x030D;

            switch (m.Msg)
            {
                case WM_DRAWCLIPBOARD:
                    OnClipboardChanged();
                    SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                    break;

                case WM_CHANGECBCHAIN:
                    if (m.WParam == nextClipboardViewer)
                        nextClipboardViewer = m.LParam;
                    else
                        SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                    break;

                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        private void OnClipboardChanged()
        {
            try
            {
                IDataObject iData = Clipboard.GetDataObject();

                if (ClipboardChanged != null)
                {
                    ClipboardChanged(this, new ClipboardChangedEventArgs(iData));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto)] private static extern bool MoveFileEx(string lpExistingFileName, string lpNewFileName, int dwFlags);

        public static void RemoveOnBoot(string filePath) => MoveFileEx(filePath, null, 0x4);

        private void VideoCodecsComboBox_SelectedIndexChanged(object sender, EventArgs e) => Config.Set("videoCodec", VideoCodecsComboBox.SelectedIndex.ToString());

        private void AudioCodecsComboBox_SelectedIndexChanged(object sender, EventArgs e) => Config.Set("audioCodec", AudioCodecsComboBox.SelectedIndex.ToString());

        private VideoQuality? videoQuality { get; set; } = null;
        private TimeSpan videoDuration { get; set; } = TimeSpan.Zero;

        private TimeSpan tarjetStartTimeSpan = TimeSpan.Zero;
        private TimeSpan tarjetEndTimeSpan = TimeSpan.Zero;

        private string LastValidStartDate = string.Empty;

        private void StartTextBox_TextChanged(object sender, EventArgs e)
        {
            TimeCheckTimer.Enabled = false;
            TimeCheckTimer.Enabled = true;
        }

        private string LastValidEndDate = string.Empty;

        private void EndTextBox_TextChanged(object sender, EventArgs e)
        {
            TimeCheckTimer.Enabled = false;
            TimeCheckTimer.Enabled = true;
        }

        private bool TimeValid(string txt) => txt.Contains(':') && txt.Replace(":", "").Any(c => char.IsDigit(c)) && txt.Split(':').All(p => p.Length == 2);

        private void TimeCheckTimer_Tick(object sender, EventArgs e)
        {
            TimeCheckTimer.Enabled = false;

            if (TimeSpan.TryParseExact(StartTextBox.Text, new[] { @"hh\:mm\:ss", @"mm\:ss", @"mm\:ss\.fff" }, null, out tarjetStartTimeSpan)
                && tarjetStartTimeSpan.TotalSeconds >= 0
                && tarjetStartTimeSpan.TotalSeconds < videoDuration.TotalSeconds)
            {
                LastValidStartDate = StartTextBox.Text;
            }
            else
            {
                StartTextBox.Text = LastValidStartDate;
            }

            if (TimeSpan.TryParseExact(EndTextBox.Text, new[] { @"hh\:mm\:ss", @"mm\:ss", @"mm\:ss\.fff" }, null, out tarjetEndTimeSpan)
                && tarjetEndTimeSpan.TotalSeconds >= 0
                && tarjetEndTimeSpan.TotalSeconds <= videoDuration.TotalSeconds
                && tarjetStartTimeSpan.TotalSeconds < tarjetEndTimeSpan.TotalSeconds)
            {
                LastValidEndDate = EndTextBox.Text;
            }
            else
            {
                EndTextBox.Text = LastValidEndDate;
            }
        }

        private void MinterpolateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Config.Set("Minterpolate", MinterpolateCheckBox.Checked.ToString());

            if (MinterpolateCheckBox.Checked && videoQuality.HasValue)
            {
                MessageBox.Show("Activating minterpolate can significantly increase video conversion time, potentially taking hours or even days. Use it with caution, especially on high-resolution or long videos", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                FpsUpDown.Value = FpsUpDown.Maximum = videoQuality.Value.Framerate * 2;
            }
            else if (videoQuality.HasValue)
            {
                FpsUpDown.Maximum = videoQuality.Value.Framerate;
            }
        }

        private void MetadataCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Config.Set("Metadata", (MetadataCheckBox.Checked).ToString());
        }
    }

    public class ClipboardChangedEventArgs : EventArgs
    {
        public readonly IDataObject DataObject;

        public ClipboardChangedEventArgs(IDataObject dataObject)
        {
            DataObject = dataObject;
        }
    }
}