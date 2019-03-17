using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 进程和应用域和线程
{
    class Program
    {
        static void Main(string[] args)
        {
            //https://blog.csdn.net/whilelie/article/details/38579617
            //1、进程 Process
            //1.1 包含一个应用程序所需要的资源，一般来说，一个应用程序是一个进程（google浏览器有多个进程）
            //1.2 相当于程序的边界，进程是独立的，隔离的，不同进程之间的数据无法交互（除非使用Socket，WebServies,WCF）
            //System.Diagnostics.Process.Start();


            //2、应用程序域 AppDomain
            //2.1 一个程序运行的逻辑区域，它可以视为一个轻量级的进程，.Net程序集正是在应用程序域里面运行的，
            //2.2 一个进程可以包含多个应用程序域，一个应用程序域可以包含多个程序集。(.net平台下有的)
            //CreateDomain(),ExecuteAssembly()

            //3、线程 Thread
            //3.1 任务调度的最小单位，程序中的执行流（代码流，所有代码都必须在线程中，有了线程才能执行代码）。
            //void ParameterizedThreadStart(object o):有参无返回值的委托
            //void ThreadStart()：无参无返回值的委托
            //Start(arg)：启动线程，导致操作系统将当前实例的状态更改为运行状态，线程到底什么时候执行，要看操作系统
            //IsBackground：后台线程，当主线程关闭时，后台线程一起关闭
            //Priority:指示线程的调度优先级（有不有用还得看操作系统）
            //ThreadState：当前线程的状态
            //Join()：在继续执行之前，阻塞调用线程，直到某个线程终止为止。带参数的重载表示超时的时间，如果超过
            //超时时间，则线程不再阻塞继续执行

            //3.2 a 线程同步
            //资源共享：			//lock (ref)
            //跨线程访问：control.CheckForIllegalCrossThreadCalls=false;control.Invoke()//调用前最好用InvokeRequired属性判断一下 是不是在子线程里面调用的

            //3.3 b、死锁

            //3.4 c.异步委托
            //系统自动创建一个线程，去执行委托里面的方法
            //del.BeginInvoke(args,AsyncCallBack call,object obj)//异步调用
            //del.EndInvoke(IAsyncResult)//获取异步委托所用方法的结果。会阻塞线程，一般写在回调函数里面

            //3.5 d、线程池
            //该线程池可用于发送工作项，处理异步I/O,代表其他线程等待及处理计时器，线性池允许在后台运行多个工作，
            //而不需要为每个任务频繁地创建和销毁单独的线程，从而减少了开销
            //ThreadPool.QueueUserWorkItem(WaitCallback callBack)
            //(1)、线程池中的线程都是后台线程
            //(2)、不能手动设置每个线程的属性
            //(3)、当执行一些比较短的任务时可以考虑使用线程池，长时间执行的任务不要使用线程池来创建，而要手动创建一个线程

            //3.6 e、并行计算
            //Parallel

            //4.进程、应用程序域、线程之间的关系
            //4.1 一个进程内可以包含多个应用程序域，也有包括多个线程，线程也可以穿梭于多个应用程序域当中。
            //4.2 但在同一个时刻，线程只处于一个应用程序域中。
            //4.3 在一个进程中可以包含多个应用程序域，一个应用程序域可以装载一个可执行程序（*.exe）或者多个程序集（*.dll）。
            //这样可以使应用程序域之间实现深度隔离，即使进程中的某个应用程序域出现错误，也不会影响其他应用程序域的正常运作。


            //在AppDomain中加载程序集
            var appDomain = AppDomain.CreateDomain("NewAppDomain");
            appDomain.Load("Model");
            foreach (var assembly in appDomain.GetAssemblies())
                Console.WriteLine(string.Format("{0}\n----------------------------",
                    assembly.FullName));
            Console.ReadKey();

            //大文件拷贝
            Thread thread = new Thread(() =>
            {
                using (FileStream fRead = new FileStream("../../file/file.rar", FileMode.Open))
                {
                    using (FileStream fWrite = new FileStream("../../file/BMS中间版bak.rar", FileMode.Create))
                    {
                        long countSize = fRead.Length;
                        int hasReadSize = 0;
                        byte[] bytes = new byte[1024 * 1024];
                        int num = 0;
                        //while ((num = fRead.Read(bytes, 0, bytes.Length)) > 0)
                        //{
                        //    fWrite.Write(bytes, 0, num);
                        //    hasReadSize += num;
                        //    progressBar1.Invoke(new Action<int>((o) => {
                        //        progressBar1.Value = o;
                        //    }), (int)((hasReadSize * 100.0 / countSize)));

                        //}
                    }
                }
            });
            thread.IsBackground = true;
            thread.Start();
        }
    }
}
