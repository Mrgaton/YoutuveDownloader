using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

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
            if (args.Length > 0 && args[0].ToLowerInvariant() == "console")
            {
                CreateConsole();

                while (true) Console.WriteLine(Console.ReadLine());
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new YoutubeForm(args));
        }
    }
}