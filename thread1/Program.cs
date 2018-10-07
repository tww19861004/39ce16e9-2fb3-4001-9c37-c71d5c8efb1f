using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace thread1
{
    class Program
    {
        private static bool _isDone = false;
        static void Main()
        {
            //1.线程之间共享数据的问题
            new Thread(Done).Start();
            new Thread(Done).Start();

            //2.其它线程的异常，主线程可以捕获到么？
            try
            {
                new Thread(Go).Start();
            }
            catch (Exception ex)
            {
                // 其它线程里面的异常，我们这里面是捕获不到的。
                Console.WriteLine("Exception!");
            }

            //升级了的Task呢？其它线程的异常，主线程可以捕获到么？
            try
            {
                var task = Task.Run(() => { Go(); });
                task.Wait();  // 在调用了这句话之后，主线程才能捕获task里面的异常
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception!");
            }

            Console.ReadLine();
        }

        static void Done()
        {
            if (!_isDone)
            {
                Console.WriteLine("Done");
                _isDone = true; // 第二个线程来的时候，就不会再执行了(也不是绝对的，取决于计算机的CPU数量以及当时的运行情况)                
            }
        }

        static void Go() { throw null; }
    }
}
