using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContinueWith1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("主线程 Thread id {0}, 是否是线程池线程: {1}",
                Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);

            var firstTask = new Task<int>(() => TaskMethod("第一个任务", 3));

            var secondTask = new Task<int>(() => TaskMethod("第二个任务", 2));

            firstTask.ContinueWith(t => Console.WriteLine("后续：第一个任务的返回结果为 {0}. Thread id {1}, 是否是线程池线程: {2}",
                t.Result,
                Thread.CurrentThread.ManagedThreadId,
                Thread.CurrentThread.IsThreadPoolThread),
                TaskContinuationOptions.OnlyOnRanToCompletion);

            firstTask.Start();
            secondTask.Start();

            Task.WhenAll(firstTask, secondTask);

            //1.然而，如果把4秒钟的休眠注释掉，那么由于主线程很早就结束了，
            //因此secondTask只能接受到OnlyOnRanToCompletion，因此还是运行在线程池中。
            //2.这里主线程休眠了足足4秒钟，足以让firstTask和secondTask两个任务完成运行，而后，
            //由于secondTask的后续除了接受OnlyOnRanToCompletion外，还接受ExecuteSynchronously。
            //因此，后续运行中，由于主线程还没有结束，因此ExecuteSynchronously得到认可，故secondTask的后续是在主线程上运行。           
            //Thread.Sleep(TimeSpan.FromSeconds(4)); //给予足够时间，让firstTask、secondTask及其后续操作执行完毕。

            Task continuation = secondTask.ContinueWith(
                t => Console.WriteLine("后续：第二个任务的返回结果为 {0}. Thread id {1}, 是否是线程池线程: {2}", 
                t.Result, 
                Thread.CurrentThread.ManagedThreadId, 
                Thread.CurrentThread.IsThreadPoolThread), 
                TaskContinuationOptions.OnlyOnRanToCompletion | TaskContinuationOptions.ExecuteSynchronously);

            Console.ReadKey();
                        
        }

        static int TaskMethod(string name, int seconds)
        {
            Console.WriteLine("Task Method : Task {0} is running on a thread id {1}. Is thread pool thread: {2}", name, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread); Thread.Sleep(TimeSpan.FromSeconds(seconds)); return 42 * seconds;
        }

    }
}

