using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AsyncFunc1
{
    public class RunServiceHelper
    {
        public static async Task<Res> RunAsync<Req,Res>(string module, string logfilter, Req request, Func<Req, Task<Res>> func, Action<string, string> resultfunc = null)
        {
            var task = func(request);
            await Task.Delay(100);
            return await task;
        }

        public static async Task<HttpResponseMessage> CallAsyncMethod()
        {
            Console.WriteLine("Calling Youtube");
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://www.youtube.com/watch?v=_OBlgSz8sSM");
            Console.WriteLine("Got Response from youtube");
            return response;
        }
    }    

    class Program
    {
        static void Main(string[] args)
        {
            Task<HttpResponseMessage> myTask = RunServiceHelper.CallAsyncMethod();
            Func<Task<HttpResponseMessage>> myFun = async () => { return await myTask; };
            Func<Task<HttpResponseMessage>> myFun1 = async () => await myTask;
        }
    }
}
