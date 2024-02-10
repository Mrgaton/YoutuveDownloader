namespace Youtuve_downloader
{
    /*internal class YoutubeApi
    {
        public static WebClient Wc = new WebClient();

        public static bool ValidVideo = false;
        public static string VideoFotoUrl = null;
        public static string VideoTitulo = null;
        public static string VideoAutor = null;

        public static Uri GetDownloadUrlYT(string vidid, string Type)
        {
            string url = "https://www.yt-download.org/file/" + Type + "/" + vidid;
            string downloaded = Wc.DownloadString(url);
            foreach (string line in downloaded.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (line.Contains("https://www.yt-download.org/download/"))
                {
                    return new Uri(GetURl(line));
                }
            }
            return null;
        }

        private static string GetURl(string stringwithurl)
        {
            foreach (Match m in new Regex(@"\b(?:https?://|www\.)\S+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase).Matches(stringwithurl))
            {
                return m.Value;
            }
            return "";
        }

        public static bool GetYoutubeVideoInfo(string Video)
        {
            try
            {
                var Json = Wc.DownloadString("https://www.youtube.com/oembed?url=https://youtu.be/" + Video);
                VideoFotoUrl = DesirealitzeJsonString(Json, "thumbnail_url");
                VideoTitulo = DesirealitzeJsonString(Json, "title");
                VideoAutor = DesirealitzeJsonString(Json, "author_name");
                ValidVideo = true;
                return true;
            }
            catch
            {
                ValidVideo = false;
                return false;
            }
        }

        public static string GetVideoCode(string URL)
        {
            if (URL.Length >= 11)
            {
                return URL.Substring(URL.Length - 11);
            }
            else
            {
                return null;
            }
        }

        public static string Removeillegal(string var)
        {
            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            return r.Replace(var, "");
        }

        public static string DesirealitzeJsonString(string Json, string Path)
        {
            return XElement.Load(JsonReaderWriterFactory.CreateJsonReader(Encoding.UTF8.GetBytes(Json), new System.Xml.XmlDictionaryReaderQuotas())).XPathSelectElement("//" + Path).Value;
        }
    }*/
}