using RedisHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis主从架构
{
    /// <summary>
    /// 标记此类为可序列号，保证此类的实体对象可以被序列化
    /// </summary>
    [Serializable]
    public class ChatModels
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public string useId { get; set; }

        /// <summary>
        /// 对话内容
        /// </summary>
        public string chat { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //在程序终止或者类的实例被销毁的时候，请将订阅者实例注销掉，否则，在redis中一直存在这个订阅者

            RedisClient myRedisClient = new RedisClient(RedisSingletonConnection.Instance);

            Pub(myRedisClient);

            Sub(myRedisClient);

            Console.ReadKey();
        }

        static void Pub(RedisClient myRedisClient)
        {
            Console.WriteLine("请输入要发布向哪个通道？");
            var channel = Console.ReadLine();
            
            for (int i = 0; i < 2; i++)
            {
                myRedisClient.Publish(channel, i.ToString());
            }

        }

        static void Sub(RedisClient myRedisClient)
        {
            Console.WriteLine("请输入您要订阅哪个通道的信息？");
            var channelKey = Console.ReadLine();
            myRedisClient.Subscribe(channelKey, (channel, message) =>
            {
                Console.WriteLine("接受到发布的内容为：" + message);
            });
            Console.WriteLine("您订阅的通道为：<< " + channelKey + " >> ! 请耐心等待消息的到来！！");
        }


    }
}
