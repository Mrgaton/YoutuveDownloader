using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Youtube_downloader;
using YoutubeExplode;
using YoutubeExplode.Common;

namespace Youtuve_downloader
{
    internal static class Program
    {
        public static string processPath = Assembly.GetExecutingAssembly().Location;

        [DllImport("kernel32.dll", SetLastError = true)] private static extern bool AttachConsole(int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)] private static extern IntPtr GetStdHandle(uint nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true)] private static extern void SetStdHandle(uint nStdHandle, IntPtr handle);

        [DllImport("kernel32.dll", SetLastError = true)] private static extern bool AllocConsole();

        public static void CreateConsole()
        {
            if (!AttachConsole(-1)) AllocConsole();

            StreamWriter standardOutput = new StreamWriter(Console.OpenStandardOutput());
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);
        }

        [STAThread]
        private static void Main(string[] args)
        {
            //RunChannelCheckup().GetAwaiter().GetResult();


            //SubRipSubtitleConvertor.XmlToSrt(new WebClient().DownloadString("https://www.youtube.com/api/timedtext?v=fhzKLBZJC3w&ei=Ryh_ZqvpAbTOhcIPr4K7wAc&opi=112496729&xoaf=5&hl=en&ip=0.0.0.0&ipbits=0&expire=1719634615&sparams=ip,ipbits,expire,v,ei,opi,xoaf&signature=BA348F99639251D1CB0B77F8A59206966EA27D3C.D9E49052D1D82A058A33BB18559827743FF8F1B8&key=yt8&lang=en"));

            //Console.ReadLine();

            if (args.Length > 0 && args[0].ToLowerInvariant() == "console")
            {
                CreateConsole();

                while (true) Console.WriteLine(Console.ReadLine());
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new YouTubeForm(args));
        }

        /*private static async Task RunChannelCheckup()
        {
            var cookies = "CONSENT=PENDING+802; SOCS=CAESNQgDEitib3FfaWRlbnRpdHlmcm9udGVuZHVpc2VydmVyXzIwMjMxMTI4LjA2X3AyGgJlcyACGgYIgMSkqwY; VISITOR_PRIVACY_METADATA=CgJFUxIIEgQSAgsMIEo%3D; wide=1; NID=515=OQYndni5mbtzQL3wGwYB8koebqOwnYz4zsnqeyNnlUjfeSujLt5cUCpv59j-tcsvupFn1wvIapKX4WTgsiEQU3AuWDDln5_WU6jOyilLNVs7QIr7F7Sg2My251LNy3TMi7ipi8Br5D5lBzqBdign99keRvIaXT_xRzFk1-5BJSZ5YLsnIdYQjtXeJmVRda--cPjuITrlxUdoPnIRFZKep26ZXvhuI7zS6OyprVi8aWvN--vkhjZPEwPN4geTIIhbQJfDqOxOse04BPm-PjYN33s1DTm93qvPVAa4Bi5LqkB0bL-iriSpc9siJRqAj4Hq1tjJ61ShbK2pmQUU9urKx2I-t323EtW1Q3bHrAhGirV8cUUpnjnl; HSID=AdcJjFfugIjMnJTgn; SSID=AXMtrsR1gII_bKWNB; APISID=r1efhvpsIlcZPMVx/AXz26bAPI9qfg69-H; SAPISID=j2jlTRVzP_ySi2bh/A-E4Ibk2P62lVjwGz; __Secure-1PAPISID=j2jlTRVzP_ySi2bh/A-E4Ibk2P62lVjwGz; __Secure-3PAPISID=j2jlTRVzP_ySi2bh/A-E4Ibk2P62lVjwGz; PREF=f6=40000400&volume=100&f7=4100&gl=DE&tz=Europe.Madrid&repeat=NONE&autoplay=true&f5=30000; SID=g.a000lAiWcAPv9pBNAYlugjl7YPZ4V4fI0lrzE1g8dXoKwHpHuiwZfUcS9z7Jho5U-mL_NzQ5vQACgYKAUMSARYSFQHGX2MiQYG7j_ILRB0r9HWSaAEu1BoVAUF8yKpls8MMeuEOYvdSLA-CmjB30076; __Secure-1PSID=g.a000lAiWcAPv9pBNAYlugjl7YPZ4V4fI0lrzE1g8dXoKwHpHuiwZcrP0UJ4WKRKxe8ClhubGogACgYKAfYSARYSFQHGX2MiIDq2D8BKK5NZ_L0FAnRtpRoVAUF8yKr-dyOoc5cX5lzCF_DNGSgN0076; __Secure-3PSID=g.a000lAiWcAPv9pBNAYlugjl7YPZ4V4fI0lrzE1g8dXoKwHpHuiwZ5zQjYxIWzH8iT5796qq7NgACgYKAS0SARYSFQHGX2Mi1FFf3VaNgLOG1aQxtofUVBoVAUF8yKqzBU-LHyyJoT829ftRdqES0076; LOGIN_INFO=AFmmF2swRAIgSmjUNzNI3MwrUUbU5O6Nq-kk04fAE1eoPOJmF7m4wfACIC80IwsgFjgUAtrYOmplg3aPA3sSDV_N78_2s5t8Qmsx:QUQ3MjNmejZQTExVbllFNlo5d0ZZSjFWaHBiaE1lYjlaeUVOR3N2NWlHVkxaMmpFdlh4VGw2QnBGYTQza1J4UkpBYzBPSkthYnlrZkRGOHBXd0hJS1BocWloRzJ1V1Q0YWxiQkZya09zRW4zc0wtVUNVb0luTi1WSkZqR2dtMTBVZnYzVkdRR2JsZEtobm1DbzNXeTMtZ2l1dVNuVk10Rjd0b0EzcjZjd2ZrVWQtVlVmSWdYdVhsZFU1Z1NUVzBEdFU4MEptNW9KSUI0Q0hFV3g4QkZxOUJiNlBHOVE0VHZEUQ==; YSC=LEbY1yjfLCM; VISITOR_PRIVACY_METADATA=CgJFUxIcEhgSFhMLFBUWFwwYGRobHB0eHw4PIBAREiEgZQ%3D%3D; __Secure-1PSIDTS=sidts-CjIB3EgAEkCPZE5NS_Eef8n8uD4USQWaulh5fN0Vn2PjUTvB9H_zXhG7MIGamo5bJumAOBAA; __Secure-3PSIDTS=sidts-CjIB3EgAEkCPZE5NS_Eef8n8uD4USQWaulh5fN0Vn2PjUTvB9H_zXhG7MIGamo5bJumAOBAA; __Secure-YEC=Cgt3VDU1cGhpZWxyZyjzkYe0BjIiCgJFUxIcEhgSFhMLFBUWFwwYGRobHB0eHw4PIBAREiEgZQ%3D%3D; SIDCC=AKEyXzU42N5ceZGuPINvaIg59YMpVgSQ7BPLIlYfSEk-OfGP5_0cBwzBbejSry-Z5Yu1IM2mtxc; __Secure-1PSIDCC=AKEyXzUM3YBcua6bWyN4leFpcxmbup3swoZn-p3DZvEHzhGGqQFlNf118-hB5F_Gq7ww1FQBNcM; __Secure-3PSIDCC=AKEyXzWtbvASbJWKrQTMYEau9p6OY0-K6Qf46DjPA0YeXW8Ye29V5jgwc9VUh0Ixcshs85OOUDbk\r\n".Split(';').Select(c => {
                var splited = c.Trim().Split(new[] { '=' }, 2);

                return new Cookie(splited[0], splited[1]) { Domain = "www.youtube.com" };
            });

            var youtube = new YoutubeClient(cookies.ToArray());

            var channelUrl = "www.youtube.com/channel/UCAYsy_6_VBLiXkTGYIO2pdg";
            var channel = await youtube.Channels.GetAsync(channelUrl);

            var videos = await youtube.Channels.GetUploadsAsync(channelUrl);

            StreamWriter writer = new StreamWriter(File.Open("videos.txt",FileMode.OpenOrCreate,FileAccess.ReadWrite,FileShare.ReadWrite));

            foreach (var video in videos)
            {
                writer.WriteLine(video.Id + ":" + video.Url + ":" + video.Title);
                Console.WriteLine(video.Id + ":" + video.Url + ":" + video.Title);
            }

            writer.Flush();
        }*/
    }
}