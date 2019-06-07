using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace async3
{
    class Program
    {
        static void Main(string[] args)
        {
            //控制台程序没有主线程，程序跟着线程跑，启动线程3，就在一直在线程3里跑，除非再创建新的线程
            // 所以在控制台程序中同步方法中调用异步方法不会发生死锁
            ShowThreadId("Main Before");
            Test();
            ShowThreadId("Main End");
            Console.ReadKey();
        }

        private static async void Test()
        {
            ShowThreadId("Test.Before");
            await Task.Run(() => { Thread.Sleep(2000); ShowThreadId("Test.Task"); });            
            //最后再补充说一点，本文提到的await and async死锁问题，在.Net控制台程序中并不存在。
            //因为经过实验发现在.Net控制台程序中，
            //await关键字这一行后面的代码默认就是在一个新的线程上执行的，
            //也就是说在控制台程序中就算不调用Task.ConfigureAwait(false)，
            //await关键字这一行后面的代码也会在一个新启动的线程上执行，不会和主线程发生死锁。
            //但是在Winform和Asp.net中就会发生死锁。
            ShowThreadId("Test.After");
        }

        private static void ShowThreadId(string str)
        {
            Console.WriteLine($"{str} {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
