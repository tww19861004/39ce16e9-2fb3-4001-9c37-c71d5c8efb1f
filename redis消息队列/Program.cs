using RedisHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace redis消息队列
{    
    class Program
    {

        static void Main(string[] args)
        {
            //主要借鉴的方法为ListLeftPop及ListRightPush
            //RedisStore.RedisCache.KeyDelete("201904102002");
            //先进先出
            //一个生产者，多个消费者的情况
            //Redis的列表是使用双向链表实现的，保存了头尾节点，所以在列表头尾两边插取元素都是非常快的

            //https://www.cnblogs.com/stopfalling/p/5375492.html

            //https://www.cnblogs.com/cklovefan/p/7821862.html

            RedisClient myRedisClient = new RedisClient(RedisSingletonConnection.Instance);

            List<string> str = myRedisClient.List.Get("2019-04-14-MessageQueue");

            myRedisClient.Key.KeyDelete("2019-04-14-MessageQueue");
            //消息进队列
            var timerPush = new System.Threading.Timer(state =>
            {
                //Console.WriteLine($"{state?.ToString()}每5秒执行一次的定时任务,当前线程Id={System.Threading.Thread.CurrentThread.ManagedThreadId},IsThreadPoolThread={System.Threading.Thread.CurrentThread.IsThreadPoolThread},IsBackground={System.Threading.Thread.CurrentThread.IsBackground}");
                //消息进列
                string pushMsg = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm ss.sss");
                myRedisClient.List.RPush("2019-04-14-MessageQueue", pushMsg);
                Console.WriteLine($"++++++++++++++进列:消息{pushMsg}");

            }, "timerPush", 0, 3000);

            //消息出列
            var timerPop = new System.Threading.Timer(state =>
            {
                //Console.WriteLine($"{state?.ToString()}每5秒执行一次的定时任务,当前线程Id={System.Threading.Thread.CurrentThread.ManagedThreadId},IsThreadPoolThread={System.Threading.Thread.CurrentThread.IsThreadPoolThread},IsBackground={System.Threading.Thread.CurrentThread.IsBackground}");
                //消息出列
                string popMsg = myRedisClient.List.LPop("2019-04-14-MessageQueue");
                if(!string.IsNullOrEmpty(popMsg))
                {
                    Console.WriteLine("----------出列:" + popMsg);
                }                

            }, "timerPop", 0, 5000);            

            Console.ReadKey();
        }
    }
}
