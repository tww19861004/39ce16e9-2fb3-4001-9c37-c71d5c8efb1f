using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task.DelayThread.Sleep
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Tasks.Task.Delay(5000);
            System.Threading.Thread.Sleep(5000);
            Console.WriteLine("12345");

        }
    }
}
