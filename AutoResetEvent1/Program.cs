using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BootCampAutoResetEvent
{
    class Program
    {
        //AutoResetEvent就好象地铁闸门，你过去的时候刷一下卡，它让一个人过去，然后就会自动恢复为关闭状态

        //定义两个信号锁 
        //true：有信号，子线程的WaitOne方法会被自动调用
        //假设 AutoResetEvent 初始化为 true，就是已经占用了一个信号
        public static AutoResetEvent event_1 = new AutoResetEvent(true);//打开的地铁闸门，可通过一个线程
        //false：无信号，子线程的WaitOne方法不会被自动调用
        public static AutoResetEvent event_2 = new AutoResetEvent(false);//关闭的地铁闸门，线程到此阻塞
        static void Main(string[] args)
        {
            Console.WriteLine("Press Enter to create three threads and start them\r\n" +
                "The threads wait on AutoResetEvent #1, which was created\r\n" +
                "in the signaled state, so the first thread is released.\r\n" +
                "This puts AutoResetEvent #1 into the unsignaled stated.");

            Console.ReadLine();

            //Parallel.For(1, 4, i =>
            //{
            //    Thread t = new Thread(ThreadProc);
            //    t.Name = "Thread_" + i;
            //    t.Start();
            //});

            for (int i = 1; i < 4; i++)
            {
                //三个人，每个人代表一个线程
                Thread t = new Thread(ThreadProc);
                t.Name = "唐伟伟_" + i;
                t.Start();
            }
            Thread.Sleep(250);

            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine("按确认开启砸门.");
                Console.ReadLine();
                event_1.Set(); //刷卡操作              
                Thread.Sleep(250);
            }

            Console.WriteLine("\r\nAll threads are now waiting on AutoResetEvent #2");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Press Enter to release a thread.");
                Console.ReadLine();
                event_2.Set();
                Thread.Sleep(250);
            }
        }

        static void ThreadProc()
        {
            string name = Thread.CurrentThread.Name;

            Console.WriteLine("{0} waits on AutoResetEvent event_1", name);
            //地铁的刷卡进站,就是那个刷卡器
            event_1.WaitOne();
            Console.WriteLine("{0} is released from AutoResetEvent event_1.", name);

            Console.WriteLine("{0} waits on AutoResetEvent event_2.", name);
            event_2.WaitOne();
            Console.WriteLine("{0} is released from AutoResetEvent event_2", name);

            Console.WriteLine("{0} ends.", name);

        }
    }
}