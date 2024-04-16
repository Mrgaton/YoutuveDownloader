using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Youtube_downloader
{
    internal class Config
    {
        private static string configPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "YTDownloader.conf");

        public static void Set(string key, string value)
        {
            string[] data = (File.Exists(configPath) ? File.ReadAllText(configPath) : string.Empty).Split('\n');

            StringBuilder sb = new StringBuilder();

            foreach (var k in data)
            {
                if (string.IsNullOrEmpty(k) || k.StartsWith(key + '=')) continue;

                sb.AppendLine(k);
            }

            sb.AppendLine(key + '=' + value);

            File.WriteAllText(configPath, sb.ToString());
        }

        public static string Get(string key)
        {
            if (!File.Exists(configPath)) return null;

            return File.ReadAllText(configPath).Split('\n').FirstOrDefault(s => s.StartsWith(key + '=', StringComparison.InvariantCultureIgnoreCase))?.Split('=').LastOrDefault().Trim('\r');
        }
    }
}