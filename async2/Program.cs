using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace async2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Begin RunAsync");
            TaskAsyncHelper.AsyncHelper.RunAsync<string>(TimeConsumingMethod, CallBack);
            Console.WriteLine($"End RunAsync");
            Console.ReadKey();
            return;


        }

        private static string TimeConsumingMethod()
        {
            Thread.Sleep(4000);
            Console.WriteLine($"Test()");
            return Guid.NewGuid().ToString();
        }

        private static void CallBack(string str)
        {
            Console.WriteLine($"CallBack({str})");
        }

    }
}
