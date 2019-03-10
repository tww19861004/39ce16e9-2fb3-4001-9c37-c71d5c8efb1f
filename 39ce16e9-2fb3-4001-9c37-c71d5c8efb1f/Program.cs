using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _39ce16e9_2fb3_4001_9c37_c71d5c8efb1f
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"主线程启动,当前线程id:{Thread.CurrentThread.ManagedThreadId}");
            //ThreadPool.QueueUserWorkItem(StartCode,5);
            new Task(StartCode, 5).Start();
            Console.WriteLine($"主线程到此为止,当前线程id:{Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(5000);
            Console.ReadKey();
            return;

            //1000000000这个数字会抛出System.AggregateException
            Task<Int32> t = new Task<Int32>(r => Sum((Int32)r), 1000000000);
            //可以现在开始，也可以以后开始 
            t.Start();
            //Wait显式的等待一个线程完成
            t.Wait();

            Console.ReadKey();
        }

        private static void StartCode(object i)
        {
            Console.WriteLine("开始执行子线程...{0},当前线程id:{1}", i, Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(5000);//模拟代码操作    
        }

        private static Int32 Sum(Int32 i)
        {
            Int32 sum = 0;
            for (; i > 0; i--)
                checked { sum += i; }
            return sum;
        }

        private static Int32 Sum()
        {
            return 1;
        }
    }
}

