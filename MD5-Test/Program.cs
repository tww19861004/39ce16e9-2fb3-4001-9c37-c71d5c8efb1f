using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Linq;

namespace MD5_Test
{

    using System;
    class A
    {
        public A()
        {
            Console.WriteLine("执行A构造函数");
            PrintFields();            
        }
        public virtual void PrintFields()
        {
            Console.WriteLine("执行A-PrintFields");
        }
    }
    class B : A
    {
        int x = 1;
        int y;
        public B()
        {
            y = -1;
            Console.WriteLine($"执行B构造函数,x={x},y={y}");
        }
        public override void PrintFields()
        {
            Console.WriteLine("执行B-PrintFields,x={0},y={1}", x, y);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string ordernos = "3000054222,3000053719";
            string str = $"WHERE orderno in ({string.Join(",", ordernos.Split(new char[] { ',', '，' }, StringSplitOptions.RemoveEmptyEntries).Select(r=>"'"+r+"'"))}) ";;

            DateTime now = DateTime.Now.AddDays(1);
            DateTime today2 = new DateTime(now.Year, now.Month, now.Day);

            //B b1 = new B();
            Print(1);

            Console.ReadKey();
        }

        public static string GetStrMd5(string ConvertString)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)));
            t2 = t2.Replace("-", "");
            return t2;
        }

        private static bool Print(int number)
        {
            Console.WriteLine(number);
            return number >= 200 || Print(number + 1);
        }

    }
}
