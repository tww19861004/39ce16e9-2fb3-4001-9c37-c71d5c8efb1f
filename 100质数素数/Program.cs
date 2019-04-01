using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _100质数素数
{
    //质数又称素数，有无限个。质数定义为在大于1的自然数中，除了1和它本身以外不再有其他因数的数称为质数,质数不包括负数
    class Program
    {
        static void Main(string[] args)
        {
            //100以内的质数
            for(int i=0;i<=100;i++)
            {
                if(IsZhiShu(i))
                {
                    Console.WriteLine(i+"是质数");
                }
            }
            Console.ReadKey();
        }

        //判断一个数是否为质数
        private static bool IsZhiShu(int n)
        {
            if (n < 2)
                return false;
            for(int i = 2;i<n;i++)
            {
                //能被其他整数相除，不是质数
                if (n%i == 0)
                {                    
                    return false;
                }
            }
            return true;
        }

        public static int Foo(int i)
        {
            if (i <= 0)
                return 0;
            else if (i > 0 && i <= 2)
                return 1;
            else return Foo(i - 1) + Foo(i - 2);
        }
    }
}
