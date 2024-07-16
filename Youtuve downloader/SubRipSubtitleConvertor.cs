using System;
using System.Globalization;
using System.Text;
using System.Xml;

namespace Youtube_downloader
{
    internal class SubRipSubtitleConvertor
    {
        public static string XmlToSrt(string xml)
        {
            XmlDocument doc = new XmlDocument();

            doc.LoadXml(xml);

            XmlNodeList textNodes = doc.GetElementsByTagName("text");
            StringBuilder sb = new StringBuilder();

            int index = 1;

            foreach (XmlNode node in textNodes)
            {
                string start = node.Attributes["start"].Value;
                string dur = node.Attributes["dur"].Value;
                string text = node.InnerText;

                sb.AppendLine((index++).ToString());
                sb.AppendLine($"{ConvertSecondsToTimeFormat(double.Parse(start, CultureInfo.InvariantCulture))} --> {ConvertSecondsToTimeFormat(double.Parse(start, CultureInfo.InvariantCulture) + double.Parse(dur, CultureInfo.InvariantCulture))}");
                sb.AppendLine(text);
                sb.AppendLine();
            }

            return sb.ToString();
        }

        public static string ConvertSecondsToTimeFormat(double totalSeconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(totalSeconds);

            string output = string.Format("{0}:{1}:{2},{3}",
                time.Hours.ToString("00"),
                time.Minutes.ToString("00"),
                time.Seconds.ToString("00"),
                time.Milliseconds.ToString("000"));

            return output;
        }
    }
}