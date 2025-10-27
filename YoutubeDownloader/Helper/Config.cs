using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Youtuve_downloader
{
    internal static class Config
    {
        private static string ConfigPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "YTDownloader.conf");

        public static void Set(string key, string value)
        {
            string[] data = (File.Exists(ConfigPath) ? File.ReadAllText(ConfigPath) : string.Empty).Split('\n');

            StringBuilder sb = new StringBuilder();

            foreach (var k in data)
            {
                if (string.IsNullOrEmpty(k) || k.StartsWith(key + '=', StringComparison.InvariantCultureIgnoreCase)) continue;

                sb.AppendLine(k);
            }

            sb.AppendLine(key + '=' + value);

            File.WriteAllText(ConfigPath, sb.ToString());
        }

        public static string Get(string key)
        {
            if (!File.Exists(ConfigPath)) return null;

            return File.ReadAllText(ConfigPath).Split('\n').FirstOrDefault(s => s.StartsWith(key + '=', StringComparison.InvariantCultureIgnoreCase))?.Split('=').LastOrDefault().Trim('\r');
        }
    }
}