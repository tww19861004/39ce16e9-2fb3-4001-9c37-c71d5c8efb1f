using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStack1
{
    public sealed class RedisStore
    {

        //在很多常见的情况下，StackExchange.Redis 将会自动的配置多个设置选项，包括服务器类型和版本，连接超时和主/从关系配置。可是有时候在Redis服务器这个命令是被禁止的。在这种情况下，提供更多的信息是非常有用的：
        private static ConfigurationOptions configOptions = new ConfigurationOptions
        {
            EndPoints =
            {
                { "127.0.0.1", 6379 }
            },
            CommandMap = CommandMap.Create(new HashSet<string>
            {
                // 排除几个命令
                //"INFO", "CONFIG", "CLUSTER", "PING", "ECHO", "CLIENT"
            }, available: false),
            AllowAdmin = true,
            Proxy = Proxy.Twemproxy,
            Password = "12345",
        };

        private static readonly Lazy<ConnectionMultiplexer> LazyConnection = new Lazy<ConnectionMultiplexer>(()=> ConnectionMultiplexer.Connect(configOptions));

        private RedisStore()
        {
        }

        public static ConnectionMultiplexer Connection => LazyConnection.Value;

        public static IDatabase RedisCache => Connection.GetDatabase();
    }

    class Program
    {
        static void Main(string[] args)
        {
            var redis = RedisStore.RedisCache;            

            if (redis.StringSet("tww", "1234556"))
            {
                var val = redis.StringGet("tww");

                Console.WriteLine(val);
            }
            Console.ReadKey();
        }
    }
}
