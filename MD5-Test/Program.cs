using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace MD5_Test
{
    
    class Program
    {
        static void Main(string[] args)
        {
            DateTime now = DateTime.Now.AddDays(1);
            DateTime today2 = new DateTime(now.Year, now.Month, now.Day);
            Console.WriteLine(today2);
            Console.ReadKey();
        }

        public static string GetStrMd5(string ConvertString)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)));
            t2 = t2.Replace("-", "");
            return t2;
        }
    }
}
