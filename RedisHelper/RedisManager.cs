using StackExchange.Redis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedisHelper
{
    public class RedisManager
    {
        //这种单例模式的方式比每次需要使用都去new一个对象的方式可以节约一些开销。
        //但是这种方式只适合对同一台redis服务器进行操作。假如有3台redis服务器，
        //那么使用这种单例模式就不是很好来切换要连接的redis。反而是直接通过 new 的方式来创建更加适合来进行不同redis的链接操作，
        //但是使用new的方式又额外的带来了开销，各位大神 有没有什么好的方式可以解决这样矛盾的问题

        //ServiceStack.Redis 中GetClient()方法，只能拿到Master redis中获取连接，而拿不到slave 的readonly连接。
        //这样 slave起到了冗余备份的作用，读的功能没有发挥出来，如果并发请求太多的话，则Redis的性能会有影响 

        //https://www.jb51.net/article/100446.htm

        //private static PooledRedisClientManager _prcm;
        /// <summary>
        /// 静态构造方法，初始化链接池管理对象
        /// </summary>
        static RedisManager()
        {
            CreateManager();
        }

        /// <summary>
        /// 创建链接池管理对象
        /// </summary>
        private static void CreateManager()
        {
            //只有ServiceStack.Redis有，当前redis客户端不支持
        }

        private static ConcurrentDictionary<string, RedisInfo> RedisInfoDict = new ConcurrentDictionary<string, RedisInfo>();

        private static ConnectionMultiplexer GetConnection(ConfigurationOptions config)
        {
            return ConnectionMultiplexer.Connect(config);
        }

        public static void InitRedis(string redisName, Func<List<string>> IpList, bool isNeedCache = false, int CacheTime = 300)
        {
            var nowipList = IpList();
            nowipList = nowipList.OrderByDescending(z => z).ToList();

        }

        public static RedisClient GetClient(string RedisName)
        {
            return null;
        }
    }

    internal enum CacheGroupType
    {
        M,//主从
        S,//单机
        C,//集群
        P,//Redis Proxy
    }    

    internal class RedisInfo
    {
        /// <summary>
        /// 链接标记
        /// </summary>
        public ConnectionMultiplexer connection = null;

        /// <summary>
        /// Ip列表
        /// </summary>
        public List<string> IpList = new List<string>();

        public Timer time = null;
    }
}
