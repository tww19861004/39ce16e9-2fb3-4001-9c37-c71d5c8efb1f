using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace transationDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
           优势：

            l         所有的事务逻辑包含在一个单独的调用中。

            l         拥有运行一个事务的最佳性能。

            l         独立于应用程序。

            限制：

            l         事务上下文仅存在于数据库调用中。

            l         数据库代码与数据库系统有关。
             */
            Console.WriteLine("SQL和存储过程级别的事务");
            Console.ReadKey();
        }
    }
}
