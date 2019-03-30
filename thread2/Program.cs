using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace thread2
{
    class Program
    {
        static void Main(string[] args)
        {
            //3.5 d、线程池
            //该线程池可用于发送工作项，处理异步I/O,代表其他线程等待及处理计时器，线性池允许在后台运行多个工作，
            //而不需要为每个任务频繁地创建和销毁单独的线程，从而减少了开销
            //ThreadPool.QueueUserWorkItem(WaitCallback callBack)
            //(1)、线程池中的线程都是后台线程
            //(2)、不能手动设置每个线程的属性
            //(3)、当执行一些比较短的任务时可以考虑使用线程池，长时间执行的任务不要使用线程池来创建，而要手动创建一个线程
            //通过BeginXXX方法运行的线程都是后台线程


            //当前主线程是个前台线程,且不能修改为后台线程
            Console.WriteLine("当前主线程,IsBackground="+Thread.CurrentThread.IsBackground);

            //Thread创建的线程是前台线程

            //Task使用程序池创建线程,默认为后台线程            

            //.Net的公用语言运行时（Common Language Runtime，CLR）
            //能区分两种不同类型的线程：前台线程和后台线程。这两者的区别就是：应用程序必须运行完所有的前台线程才可以退出；
            //而对于后台线程，应用程序则可以不考虑其是否已经运行完毕而直接退出，
            //所有的后台线程在应用程序退出时都会自动结束。
            //.net环境使用Thread建立的线程默认情况下是前台线程，即线程属性IsBackground=false
            //在进程中，只要有一个前台线程未退出，进程就不会终止。主线程就是一个前台线程。
            //一般后台线程用于处理时间较短的任务            
            //System.Threading.Thread.Sleep(100);  

            //前台线程阻止了主线程的关闭
            Thread th = new Thread(delegate ()
            {
                Thread.Sleep(6000);
                Console.WriteLine("start a new thread,IsBackground=" + Thread.CurrentThread.IsBackground);
                //File.AppendAllText(@"D:\1.txt", "Thread:"+System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + Environment.NewLine);
            });
            th.IsBackground = false;
            th.Start();

            Action<string> ac = (a) =>
              {
                  Console.WriteLine("start a new action beginxxx,IsBackground=" + Thread.CurrentThread.IsBackground);
                  //File.AppendAllText(@"D:\1.txt", "Action:"+System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + Environment.NewLine);
              };
            ac.BeginInvoke(null, null, null);


            Console.WriteLine("main thread end,IsBackground=" + Thread.CurrentThread.IsBackground);
            Console.ReadKey();

            //.NET线程池默认包含25个线程

        }
    }
}
