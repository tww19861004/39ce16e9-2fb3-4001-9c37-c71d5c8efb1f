using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Func1
{
    class Program
    {
        static void Main(string[] args)
        {

            Func<int, int, int> getFunc = (p1, p2) =>
            {
                return Fun(p1,p2);
            };

            Console.WriteLine(Test<int, int>(Fun, 100, 200));
            Console.WriteLine(getFunc(1,2));
            Console.ReadKey();
        }
        public static int Test<T1, T2>(Func<T1, T2, int> func, T1 a, T2 b)
        {
            return func(a, b);
        }
        private static int Fun(int a, int b)
        {
            return a + b;
        }
    }
}
