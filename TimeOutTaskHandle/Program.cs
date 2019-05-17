using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TimeOutTaskHandle
{
    public static class TaskHelper
    {
        // 有返回值
        public static async Task<TResult> TimeoutAfter<TResult>(this Task<TResult> task, TimeSpan timeout)
        {
            using (var timeoutCancellationTokenSource = new CancellationTokenSource())
            {
                var completedTask = await Task.WhenAny(task, Task.Delay(timeout, timeoutCancellationTokenSource.Token));
                if (completedTask == task)
                {
                    timeoutCancellationTokenSource.Cancel();
                    return await task;  // Very important in order to propagate exceptions
                }
                else
                {
                    throw new TimeoutException("The operation has timed out.");
                }
            }
        }

        // 无返回值
        public static async Task TimeoutAfter(this Task task, TimeSpan timeout)
        {
            using (var timeoutCancellationTokenSource = new CancellationTokenSource())
            {
                var completedTask = await Task.WhenAny(task, Task.Delay(timeout, timeoutCancellationTokenSource.Token));
                if (completedTask == task)
                {
                    timeoutCancellationTokenSource.Cancel();
                    await task;  // Very important in order to propagate exceptions
                }
                else
                {
                    timeoutCancellationTokenSource.Cancel();
                    throw new Exception("The operation has timed out.");
                    //throw new TimeoutException("The operation has timed out.");
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(() =>
            {
                System.Threading.Thread.Sleep(1000);
                int i = 0;
                double j = 10 / i;
            }).TimeoutAfter(TimeSpan.FromMilliseconds(20)).ContinueWith(r =>
            {
                if (r.Exception != null && r.Exception.InnerExceptions != null && r.Exception.InnerExceptions.Count > 0)
                {
                    Console.WriteLine("task throw exception:"+string.Join(Environment.NewLine, r.Exception.InnerExceptions.Select(r1=>r1.Message)));
                }
            });
            Console.ReadKey();
        }
    }
}
