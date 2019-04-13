using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStack.Redis_和_StackExchange.Redis_性能比较
{
    class Program
    {
        static void Main(string[] args)
        {
            //开始比较 客户端调用时, ServiceStack.Redis 与 StackExchange.Reids 的性能
            //http://www.cnblogs.com/shuxiaolong/p/ServiceStack_Redis_StackExchange_Redis.html

            //ServiceStack.Redis 居然崩溃了：每小时只能调用 6000次，除非购买商用版。【别说商用了，6000次，我个人使用都不够啊

        }
    }
}
