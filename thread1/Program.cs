using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace thread1
{
    class Program
    {        
        static void Main()
        {
            //所有的异步线程都来自于.NET线程池
            //每次执行一次异步调用，便产生一个新的线程；同时可用线程数目减少            
            Action ac = () =>
            {
                int intAvailableThreads, intAvailableIoAsynThreds;

                // 取得线程池内的可用线程数目，我们只关心第一个参数即可
                ThreadPool.GetAvailableThreads(out intAvailableThreads,
                out intAvailableIoAsynThreds);

                // 线程信息
                string strMessage =
                String.Format("是否是线程池线程：{0},线程托管ID：{1},可用线程数：{2}",
                Thread.CurrentThread.IsThreadPoolThread.ToString(),
                Thread.CurrentThread.GetHashCode(),
                intAvailableThreads);

                Console.WriteLine(strMessage);

                Thread.Sleep(3000);
            };

            for (int i = 0; i < 30; i++)
            {
                // 以异步的形式，调用Sleep函数30次
                ac.BeginInvoke(null, null);
            }

            Console.ReadKey();
        }

    }
}
