using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LongRunning
{
    class Program
    {
        static void Main(string[] args)
        {
            //如果任务的代码需要运行很长时间，
            //就应该使用TaskCreationOptions.LongRunning告诉任务调度器创建一个新线程
            //而不是使用线程池中的线程
            //此时线程可以不由线程池管理。当线程来自线程池时，
            //任务调度器可以决定等待已经运行的任务完成，然后使用这个线程，
            //而不是在线程池中创建一新的线程。
            //对于长时间运行的线程，任务调度器会立即知道等待他们完成不是明智做法。

        }

        private static void RunSynchronousTask()
        {
            TaskMethod("just the main thread");
            var t1 = new Task(TaskMethod, "run sync");
            t1.RunSynchronously();
        }

        private static void LongRunningTask()
        {
            var t1 = new Task(TaskMethod, "long running", TaskCreationOptions.LongRunning);
            t1.Start();
        }

        public static void TaskMethod(object dd)
        {
            Console.WriteLine(dd);
            Console.WriteLine("Task id :{0},Thread :{1}", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Is pool thread :{0}", Thread.CurrentThread.IsThreadPoolThread);
            Console.WriteLine("Is background thread:{0}", Thread.CurrentThread.IsBackground);
            Console.WriteLine("");
        }
    }
}
