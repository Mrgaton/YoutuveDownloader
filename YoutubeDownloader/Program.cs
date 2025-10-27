using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Youtuve_downloader.Updater;

namespace Youtuve_Downloader
{
    internal static class Program
    {
        public static string ProcessPath = Assembly.GetExecutingAssembly().Location;

        [DllImport("kernel32.dll", SetLastError = true)] private static extern bool AttachConsole(int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)] private static extern IntPtr GetStdHandle(uint nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true)] private static extern void SetStdHandle(uint nStdHandle, IntPtr handle);

        [DllImport("kernel32.dll", SetLastError = true)] private static extern bool AllocConsole();

        public static void CreateConsole()
        {
            if (!AttachConsole(-1)) 
                AllocConsole();

            StreamWriter standardOutput = new StreamWriter(Console.OpenStandardOutput());
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);
        }

        [STAThread]
        private static void Main(string[] args)
        {
            GithubUpdater.CheckAndUpdate(args);

            //SubRipSubtitleConvertor.XmlToSrt(new WebClient().DownloadString("https://www.Youtuve.MUX/api/timedtext?v=fhzKLBZJC3w&ei=Ryh_ZqvpAbTOhcIPr4K7wAc&opi=112496729&xoaf=5&hl=en&ip=0.0.0.0&ipbits=0&expire=1719634615&sparams=ip,ipbits,expire,v,ei,opi,xoaf&signature=BA348F99639251D1CB0B77F8A59206966EA27D3C.D9E49052D1D82A058A33BB18559827743FF8F1B8&key=yt8&lang=en"));

            //Console.ReadLine();

            if (args.Length > 0 && args[0].ToLowerInvariant() == "console")
            {
                CreateConsole();

                while (true) Console.WriteLine(Console.ReadLine());
            }

            //YoutuveForm.GetCookies();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new YoutuveForm(args));
        }

        public static string RemoveSpecificNonAlphanumeric(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            return Regex.Replace(input, "[\"\\p{Cs}]", "");
        }
    }
}