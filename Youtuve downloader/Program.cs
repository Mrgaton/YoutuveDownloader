using System;
using System.Windows.Forms;

namespace Youtuve_downloader
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new YoutubeForm());
        }
    }
}