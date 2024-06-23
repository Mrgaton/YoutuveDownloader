namespace Youtube_downloader
{
    internal class YoutubeFormOld
    {
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

        private void YouTubeForm_Load(object sender, EventArgs e)
        {
        }

        private void BitrateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }*/
    }
}