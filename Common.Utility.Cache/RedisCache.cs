using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using static Common.Utility.Cache.RedisStore;

namespace Common.Utility.Cache
{    
    public abstract class RedisCache : IRedisCache
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool Exists(string key)
        {
            return Instance.KeyExists(key);            
        }

        public T Get<T>(string key)
        {
            throw new NotImplementedException();
        }

        public long Increment(string key)
        {
            return Instance.StringIncrement(key);            
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public bool Set<T>(string key, T value, int expired = 10000)
        {
            throw new NotImplementedException();
        }
    }

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

        private static readonly Lazy<ConnectionMultiplexer> LazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(configOptions));

        private RedisStore()
        {
        }

        public static ConnectionMultiplexer Connection => LazyConnection.Value;

        public static IDatabase Instance => Connection.GetDatabase();
    }
}
