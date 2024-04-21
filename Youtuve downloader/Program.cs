using System;
using System.Windows.Forms;

namespace Youtuve_downloader
{
    internal static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                Console.WriteLine(args[1]);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new YoutubeForm());
        }
    }
}