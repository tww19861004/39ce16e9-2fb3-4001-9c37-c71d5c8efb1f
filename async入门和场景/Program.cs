using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace async入门和场景
{
    public class HttpService
    {
        public static string GetMsg1()
        {
            Thread.Sleep(3000);
            return "GetMsg1";
        }

        public static string GetMsg2()
        {
            Thread.Sleep(5000);
            return "GetMsg2";
        }

        //利用async封装同步业务的方法
        private static async Task<string> GetMsg1Async()
        {
            Thread.Sleep(3000);
            //其它同步业务
            return "GetMsg1Async";
        }
        private static async Task<string> GetMsg2Async()
        {
            Thread.Sleep(5000);
            //其它同步业务
            return "GetMsg2Async";
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //需要的时间大约为：3s + 5s = 8s,
            #region 案例1(传统同步方式 耗时8s左右)
            {
                Stopwatch watch = Stopwatch.StartNew();
                Console.WriteLine("传统同步方式 耗时8s左右\r\n开始执行");

                string t1 = HttpService.GetMsg1();
                string t2 = HttpService.GetMsg2();

                Console.WriteLine("我是主业务");
                Console.WriteLine($"{t1},{t2}");
                watch.Stop();
                Console.WriteLine($"耗时：{watch.ElapsedMilliseconds}");
            }
            #endregion

            //需要的时间大约为：Max(3s,5s) = 5s
            #region 案例2(开启新线程分别执行两个耗时操作 耗时5s左右)
            {
                Stopwatch watch = Stopwatch.StartNew();
                Console.WriteLine("");
                Console.WriteLine("开启新线程分别执行两个耗时操作 耗时5s左右\r\n开始执行");

                var task1 = Task.Run(() =>
                {
                    return HttpService.GetMsg1(); 
                });

                var task2 = Task.Run(() =>
                {
                    return HttpService.GetMsg2();
                });

                Console.WriteLine("我是主业务");
                //主线程进行等待
                Task.WaitAll(task1, task2);
                Console.WriteLine($"{task1.Result},{task2.Result}");
                watch.Stop();
                Console.WriteLine($"耗时：{watch.ElapsedMilliseconds}");
            }
            #endregion

            #region 案例3(使用系统类库自带的异步方法 耗时5s左右)
            {
                Stopwatch watch = Stopwatch.StartNew();
                HttpClient http = new HttpClient();
                var httpContent = new StringContent("", Encoding.UTF8, "application/json");
                Console.WriteLine("开始执行");
                //执行业务
                var r1 = http.PostAsync("http://localhost:2788/Home/GetMsg1", httpContent);
                var r2 = http.PostAsync("http://localhost:2788/Home/GetMsg2", httpContent);
                Console.WriteLine("我是主业务");

                //通过异步方法的结果.Result可以是异步方法执行完的结果
                Console.WriteLine(r1.Result.Content.ReadAsStringAsync().Result);
                Console.WriteLine(r2.Result.Content.ReadAsStringAsync().Result);

                watch.Stop();
                Console.WriteLine($"耗时：{watch.ElapsedMilliseconds}");
            }
            #endregion

            Console.ReadKey();
        }
    }
}
