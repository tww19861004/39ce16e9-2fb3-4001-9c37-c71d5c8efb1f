using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace timerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var timer = new Timer(state =>
            {
                Console.WriteLine(state?.ToString()+"每秒执行一次的定时任务,当前线程Id:{0}", Thread.CurrentThread.ManagedThreadId);
            }, "timer", 0, 5000);

            var timer2 = new Timer(state =>
            {
                Console.WriteLine(state?.ToString()+"每秒执行一次的定时任务,当前线程Id:{0}", Thread.CurrentThread.ManagedThreadId);
            }, "timer2", 0, 5000);
            Console.ReadKey();
        }
    }
}
