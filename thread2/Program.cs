using System;
using System.Collections.Generic;
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


            //当前主线程是个前台线程,且不能修改为后台线程
            Console.WriteLine(Thread.CurrentThread.IsBackground);            

            //Thread创建的线程是前台线程
            Thread th = new Thread(delegate () 
            {
                Thread.Sleep(6000);
                Console.WriteLine("start a new thread");
            });
            Console.WriteLine(th.IsBackground);

            //Task使用程序池创建线程,默认为后台线程
            Task task = new Task(() => Console.WriteLine("start a new task="+Thread.CurrentThread.IsBackground));
            task.Start();


            Console.ReadKey();
        }
    }
}
