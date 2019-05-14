using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ManualResetEvent1
{
    class Program
    {
        const int count = 10;
        //赋值为false也就是没有信号
        static AutoResetEvent myAutoResetEvent = new AutoResetEvent(false);
        //true-初始状态为发出信号；false-初始状态为未发出信号
        static ManualResetEvent myManualResetEvent = new ManualResetEvent(false);
        static int number;
        static void Main(string[] args)
        {
            //AutoResetEvent和ManualResetEvent分别都有Set()改变为有信号 ,Reset()改变为无信号，
            //WaitOne()将会阻塞当前调用的线程，直到有信号为止，即执行了Set（）方法，WaitOne()方法还可以带指定时间的参数

            //AutoResetEvent与ManualResetEvent的区别是，AutoResetEvent.WaitOne()会自动改变事件对象的状态，即AutoResetEvent.WaitOne()每执行一次，事件的状态就改变一次，也就是从有信号变为无信号。
            //而ManualResetEvent则是调用Set（）方法后其信号量不会自动改变，除非再设置Reset（）方法。            

            //注意：ManualResetEvent可以对所有进行等待的线程进行统一控制
            
            //线程池开启10个线程
            for (int i = 0; i < 10; i++)
            {
                int k = i;

                ThreadPool.QueueUserWorkItem(t =>
                {
                    Console.WriteLine($"这是第{k + 1}个线程，线程ID为{Thread.CurrentThread.ManagedThreadId}");
                    //等待信号，没有信号的话不会执行后面的语句,因为初始状态是false，所以后面的语句暂时不会执行
                    myManualResetEvent.WaitOne();
                    Console.WriteLine($"++++++第{k + 1}个线程获得信号，线程ID为{Thread.CurrentThread.ManagedThreadId}");
                });
            }
            Thread.Sleep(5000);
            Console.WriteLine("\r\n 5秒后发出信号... \r\n");
            //Set()方法：释放信号，所有等待信号的线程都将获得信号，开始执行WaitOne()后面的语句
            myManualResetEvent.Set();
            Console.ReadKey();
        }
    }
}
