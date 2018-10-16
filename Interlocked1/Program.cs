using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Interlocked1
{
    class Program
    {

        private static string _flag;

        public static void ExactlyOnceMethod(string key)
        {
            var original = Interlocked.Exchange(ref key, "1");
            if (original == _flag)
            {
                // 1.重复进入
                Console.WriteLine($"key:{key},重复进入");
            }
            else
            {
                // 2.第一次进入
                Console.WriteLine($"key:{key},第一次进入");
            }
        }

        public static void ByKey(string key)
        {
            //Interlocked.Increment(ref )
        }

        static void Main(string[] args)
        {
            Parallel.For(0, 10, (n) =>
              {
                  ExactlyOnceMethod(n.ToString());
              });
            Console.ReadKey();
        }
    }
}
