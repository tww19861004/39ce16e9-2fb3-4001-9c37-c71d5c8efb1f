using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 析构函数1
{
    class Program
    {
        class First                     // 基类First
        {
            ~First()                    // 析构函数
            {
                Console.WriteLine("~First()析构函数");
            }
        }
        class Second : First            // Second类从First类派生
        {
            ~Second()                   // 析构函数
            {
                Console.WriteLine("~Second()析构函数");
            }
        }
        class Third : Second            // Third类从Second类派生
        {
            ~Third()                    // 析构函数
            {
                Console.WriteLine("~Third()析构函数");
            }
        }
        static void Main(string[] args)
        {
            Third Third1 = new Third(); // 创建类的实例
            //Console.ReadKey();
        }
    }
}
