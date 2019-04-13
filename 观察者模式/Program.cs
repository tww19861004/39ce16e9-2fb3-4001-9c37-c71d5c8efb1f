using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace 观察者模式
{
    class RunComplatedEventArgs: EventArgs
    {
        public string PublisherName { get; set; }
    }

    //ConcreteSubject
    class Publisher
    {
        private string name;
        private Publisher() { }
        public Publisher(string name)
        {
            this.name = name;
        }
        //完成事件
        public event EventHandler<RunComplatedEventArgs> RunComplatedEvent;
        public void Run()
        {
            Console.WriteLine($"{name}发布了一个消息");
            System.Threading.Thread.Sleep(5000);
            if(RunComplatedEvent!=null)
            {
                RunComplatedEvent.Invoke(this, new RunComplatedEventArgs() { PublisherName = name });
            }
        }
    }

    //ConcreteObserver
    class Program
    {
        static void Main(string[] args)
        {
            /*
             *  1。 抽象主题（Subject）：它把所有观察者对象的引用保存到一个聚集里，每个主题都可以有任何数量的观察者。抽象主题提供一个接口，可以增加和删除观察者对象。

         2。 具体主题（ConcreteSubject）：将有关状态存入具体观察者对象；在具体主题内部状态改变时，给所有登记过的观察者发出通知。

        3。抽象观察者（Observer）：为所有的具体观察者定义一个接口，在得到主题通知时更新自己。

        4。具体观察者（ConcreteObserver）：实现抽象观察者角色所要求的更新接口，以便使本身的状态与主题状态协调。
             * */

            Publisher concreteSubject = new Publisher("唐伟伟");
            concreteSubject.RunComplatedEvent += Action;
            concreteSubject.Run();
            Console.ReadKey();

        }

        private static void Action(object sender, RunComplatedEventArgs args)
        {
            Console.WriteLine($"观察者Program收到来至于{args.PublisherName}的消息");
        }
    }
}
