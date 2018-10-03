using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskCompletionSource1
{
    class Program
    {
        static void Main(string[] args)
        {
            //TaskCompletionSource真正作用是创建一个不绑定线程的任务。例如，假设一个任务需要等待5秒钟，
            //然后返回数字42.我们可以使用Timer类实现，而不需要使用线程，由CLR在x毫秒之后触发一个事件：

        }

        static Task<int> GetAnswerToLife()
        {
            var tcs = new TaskCompletionSource<int>();
            // Create a timer that fires once in 5000 ms:
            var timer = new System.Timers.Timer(5000) { AutoReset = false };
            timer.Elapsed += delegate { timer.Dispose(); tcs.SetResult(42); };
            timer.Start();
            return tcs.Task;
        }
    }
}
