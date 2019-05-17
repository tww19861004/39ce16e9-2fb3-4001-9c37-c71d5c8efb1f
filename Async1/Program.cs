using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Async1
{
    //await 不会开启新的线程
    //当前线程会一直往下走直到遇到真正的Async方法（比如说HttpClient.GetStringAsync），
    //这个方法的内部会用Task.Run或者Task.Factory.StartNew 去开启线程。也就是如果方法不是.NET为我们提供的Async方法，
    //我们需要自己创建Task，才会真正的去创建线程。
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(()=>
            {
                int i = 0;
                double j = 10 / i;
            }).ContinueWith(r=>
            {
                if (r.Exception != null && r.Exception.InnerExceptions != null && r.Exception.InnerExceptions.Count > 0)
                {

                }
            });
            Console.ReadKey(); 
        }

        static async Task Test()
        {
            var getname = GetName();            
            Console.WriteLine($"方法:Test；Current Thread Id :{Thread.CurrentThread.ManagedThreadId},是否托管线程池：{Thread.CurrentThread.IsThreadPoolThread}");
            // 方法打上async关键字，就可以用await调用同样打上async的方法
            // await 后面的方法将在另外一个线程中执行
            
            Console.WriteLine($"Current Thread Id :{Thread.CurrentThread.ManagedThreadId};my name is "+ await getname);
        }

        static async Task<string> GetName()
        {
            Console.WriteLine($"方法:GetName1；Current Thread Id :{Thread.CurrentThread.ManagedThreadId},是否托管线程池：{Thread.CurrentThread.IsThreadPoolThread}");
            return await Task.Run(() =>
            {
                Thread.Sleep(10000);
                Console.WriteLine($"方法:GetName2；Current Thread Id :{Thread.CurrentThread.ManagedThreadId},是否托管线程池：{Thread.CurrentThread.IsThreadPoolThread}");
                return "Jesse";
            });
        }
    }
}
