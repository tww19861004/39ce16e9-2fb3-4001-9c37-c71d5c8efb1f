using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

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
            DateTime now = DateTime.Now.AddDays(1);
            DateTime today2 = new DateTime(now.Year, now.Month, now.Day);

            //B b1 = new B();


            Console.ReadKey();
        }

        public static string GetStrMd5(string ConvertString)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)));
            t2 = t2.Replace("-", "");
            return t2;
        }

        /// <summary>
        /// 获得无重复随机数组
        /// </summary>
        /// <param name="n">上限n</param>
        /// <returns>返回随机数组</returns>
        static int[] GetRandom(int n)
        {
            //容器A和B
            int[] arryA = new int[n];
            int[] arryB = new int[n];
            //填充容器a
            for (int i = 0; i < arryA.Length; i++)
            {
                arryA[i] = i + 1;
            }
            //随机对象
            Random r = new Random();
            //最后一个元素的索引 如n=100，end=99
            int end = n - 1;
            for (int i = 0; i < n; i++)
            {
                //生成随机数 因为随机的是索引 所以从0到100取，end=100
                //一个大于等于 minValue 且小于 maxValue 的 32 位带符号整数，即：返回的值范围包括 minValue 但不包括 maxValue。 
                //如果 minValue 等于 maxValue，则返回 minValue
                //
                int minValue = 0;
                int maxValue = end + 1;
                int ranIndex = r.Next(minValue, maxValue);
                //把随机数放在容器B中
                arryB[i] = arryA[ranIndex];
                //用最后一个元素覆盖取出的元素
                arryA[ranIndex] = arryA[end];
                //缩减随机数生成的范围
                end--;
            }
            //返回生成的随机数组
            return arryB;
        }
    }
}
