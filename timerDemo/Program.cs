using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //https://www.cnblogs.com/GreenLeaves/p/10117374.html
            //回唤起更多的线程参与运算,综上所述每个回调方法线程池会给它分配一个线程,到底会分配多少个线程取决于你定的间隔时间.
            var timer = new System.Threading.Timer(state =>
            {
                Console.WriteLine($"{state?.ToString()}每秒执行一次的定时任务,当前线程Id={System.Threading.Thread.CurrentThread.ManagedThreadId},IsThreadPoolThread={System.Threading.Thread.CurrentThread.IsThreadPoolThread},IsBackground={System.Threading.Thread.CurrentThread.IsBackground}");
            }, "timer", 0, 3000);

            //var timer2 = new System.Threading.Timer(state =>
            //{
            //    Console.WriteLine($"{state?.ToString()}每秒执行一次的定时任务,当前线程Id={System.Threading.Thread.CurrentThread.ManagedThreadId},IsThreadPoolThread={System.Threading.Thread.CurrentThread.IsThreadPoolThread},IsBackground={System.Threading.Thread.CurrentThread.IsBackground}");
            //}, "timer2", 0, 3000);

            var timer3 = new System.Timers.Timer()
            {
                Interval = 1000 * 6
            };
            //timer3.Start();
            timer3.Elapsed += (obj, events) =>
            {
                try
                {
                    Console.WriteLine($"timer3每秒执行一次的定时任务,当前线程Id={System.Threading.Thread.CurrentThread.ManagedThreadId},IsThreadPoolThread={System.Threading.Thread.CurrentThread.IsThreadPoolThread},IsBackground={System.Threading.Thread.CurrentThread.IsBackground}");
                    //timer3.Stop();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            };
            Console.ReadKey();
        }
    }
}
