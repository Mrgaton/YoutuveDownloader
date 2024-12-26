using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Youtube_downloader.Updater
{
    internal class GithubUpdater
    {
        [DllImport("wininet.dll")] private static extern bool InternetGetConnectedState(out int Description, int ReservedValue);

        public static bool CheckNet() => InternetGetConnectedState(out int s, 0);

        private static string Username = "Mrgaton";
        private static string Repo = "YoutuveDownloader";

        private static Assembly CurrentAssembly = Assembly.GetExecutingAssembly();

        public static void CheckAndUpdate(string[] args)
        {
            var currentVersion = CurrentAssembly.GetName().Version.ToString();

            if (args.Length > 0)
            {
                if (args[0] == "update")
                {
                    string targetPath = args[1];

                    using (Stream stream = client.GetStreamAsync(args[2]).Result)
                    {
                        try
                        {
                            Process.GetProcessById(int.Parse(args[3])).Kill();
                        }
                        catch { }

                        using (FileStream fs = File.OpenWrite(targetPath))
                        {
                            fs.SetLength(0);

                            stream.CopyTo(fs);
                        }
                    }

                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = targetPath,
                        Arguments = "updated",
                        UseShellExecute = false,
                    });

                    Environment.Exit(0);
                }
                else if (args[0] == "updated")
                {
                    MessageBox.Show("Updated to " + GetRepoReleases().Result[0], Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            if (!CheckNet())
            {
                MessageBox.Show("Please connect the device to the internet.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Environment.Exit(0);
            }

            var version = GetRepoReleases().Result[0];

            if (!currentVersion.EndsWith(version))
            {
                MessageBox.Show("An update has been found updating to " + version, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                string attachement = GetReleaseAssets(version).Result.FirstOrDefault(l => l.EndsWith(".exe", StringComparison.InvariantCultureIgnoreCase));

                string tempPath = Path.GetTempFileName();

                File.Copy(CurrentAssembly.Location, tempPath, true);

                Process.Start(new ProcessStartInfo()
                {
                    FileName = tempPath,
                    Arguments = "update \"" + CurrentAssembly.Location + "\" \"" + attachement + "\" " + Process.GetCurrentProcess().Id,
                    UseShellExecute = false,
                });

                Environment.Exit(0);
            }
            else
            {
                //Console.WriteLine("Updated");
                //Console.WriteLine();
            }
        }

        private static HttpClient client = new HttpClient();

        private static async Task<string[]> GetRepoReleases()
        {
            var res = await client.GetStringAsync($"https://github.com/{Username}/{Repo}/tags");

            string data = $"<a href=\"/{Username}/{Repo}/releases/tag/";

            List<string> versions = [];

            foreach (var release in res.Split('>').Where(l => l.StartsWith(data, StringComparison.InvariantCultureIgnoreCase)))
            {
                versions.Add(release.Split('/')[5].Split('\"')[0]);
            }

            return versions.ToArray();
        }

        private static async Task<string[]> GetReleaseAssets(string tag)
        {
            var res = await client.GetStringAsync($"https://github.com/{Username}/{Repo}/releases/expanded_assets/{tag}");

            string data = $"<a href=\"/{Username}/{Repo}/releases/download/";

            List<string> assets = [];

            foreach (var asset in res.Split('>').Where(l => l.Trim().StartsWith(data, StringComparison.InvariantCultureIgnoreCase)))
            {
                assets.Add("https://github.com" + asset.Split('\"')[1]);
            }

            return assets.ToArray();
        }
    }
}