using System;
using System.Security.Cryptography;
using System.Text;

namespace YoutuveDownloader.Helper
{
    public static class StringExtensions
    {
        private static MD5 hashAlg = MD5.Create();

        public static string Hash(this string s)
        {
            return Base64Url.ToBase64Url(hashAlg.ComputeHash(Encoding.UTF8.GetBytes(s)));
        }
    }

    public static class Base64Url
    {
        public static string ToBase64Url(byte[] data) => Convert.ToBase64String(data).Trim('=').Replace('+', '-').Replace('/', '_');

        public static byte[] FromBase64Url(string data) => Convert.FromBase64String(data.Replace('_', '/').Replace('-', '+').PadRight(data.Length + (4 - data.Length % 4) % 4, '='));
    }
}