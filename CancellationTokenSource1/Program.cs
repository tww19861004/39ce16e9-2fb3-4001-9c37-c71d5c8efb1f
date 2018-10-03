using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CancellationTokenSource1
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            Task<Int32> t = new Task<Int32>(() => Sum(cts.Token, 10000), cts.Token);
            //可以现在开始，也可以以后开始 

            t.Start();

            //在之后的某个时间，取消CancellationTokenSource 以取消Task

            cts.Cancel();//这是个异步请求，Task可能已经完成了。我是双核机器，Task没有完成过

            //注释这个为了测试抛出的异常
            //Console.WriteLine("This sum is:" + t.Result);
            try
            {
                //如果任务已经取消了，Result会抛出AggregateException

                Console.WriteLine("This sum is:" + t.Result);
            }
            catch (AggregateException x)
            {
                //将任何OperationCanceledException对象都视为已处理。
                //其他任何异常都造成抛出一个AggregateException，其中
                //只包含未处理的异常

                x.Handle(e => e is OperationCanceledException);
                Console.WriteLine("Sum was Canceled");
            }
        }

        private static Int32 Sum(CancellationToken ct, Int32 i)
        {
            Int32 sum = 0;
            for (; i > 0; i--)
            {
                //在取消标志引用的CancellationTokenSource上如果调用   
                //Cancel，下面这一行就会抛出OperationCanceledException

                ct.ThrowIfCancellationRequested();

                checked { sum += i; }
            }

            return sum;
        }

        static async void RunAsync()
        {
            CancellationTokenSource ts = new CancellationTokenSource();
            CancellationToken c = ts.Token;

            await Task.Run(() =>
            {
                //此处放置你的任务……

            }, c).ContinueWith
            ((t) =>
            {                
                //继续你的任务……
            }, c);

            c.Register(() =>
            {
                //当全部Task终止之后，请在此处逐一终止全部的线程
            });
            //停止全部活动
            ts.Cancel();
        }
    }
}
