using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ManualResetEvent
{
    class Program
    {
        const int count = 10;
        //赋值为false也就是没有信号
        static AutoResetEvent myResetEvent = new AutoResetEvent(false);
        static int number;
        static void Main(string[] args)
        {
            //AutoResetEvent和ManualResetEvent分别都有Set()改变为有信号 ,Reset()改变为无信号，
            //WaitOne()将会阻塞当前调用的线程，直到有信号为止，即执行了Set（）方法，WaitOne()方法还可以带指定时间的参数

            //AutoResetEvent与ManualResetEvent的区别是，AutoResetEvent.WaitOne()会自动改变事件对象的状态，即AutoResetEvent.WaitOne()每执行一次，事件的状态就改变一次，也就是从有信号变为无信号。
            //而ManualResetEvent则是调用Set（）方法后其信号量不会自动改变，除非再设置Reset（）方法。
        }
    }
}
