using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task2019
{
    class Program
    {
        static void Main(string[] args)
        {
            var tf = new TaskFactory();
            //方式1：实例化TaskFactory类，在把其中的TaskMethod方法传入StartNew方法中，线程会立即启动
            tf.StartNew(TaskMethod,"using a task factory");

            //方式2：通过Task类调用静态属性Factory来访问TaskFactory，并调用StartNew
            Task t2 = Task.Factory.StartNew(TaskMethod, "factory via a task");

            //方式3：使用Task类的构造函数，实例化了以后并不会立即执行，而是指定Created状态，然后在调用task类的start方法
            var t3 = new Task(TaskMethod, "using a task constrctor and start");
            t3.Start();

            //方式4：4.5版本新增，task类中的run使用lambda
            Task t4 = Task.Run(() => TaskMethod("using the run method"));

            Console.ReadKey();
        }

        private static void TaskMethod(object o)
        {
            Console.WriteLine(o);
            Console.WriteLine("Task id :{0},Thread :{1}", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Is pool thread :{0}", Thread.CurrentThread.IsThreadPoolThread);
            Console.WriteLine("Is background thread:{0}", Thread.CurrentThread.IsBackground);
            Console.WriteLine("");
        }
    }
}
