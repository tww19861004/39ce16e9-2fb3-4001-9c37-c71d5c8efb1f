﻿using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStack1
{
    public class RedisStore
    {
        private static readonly Lazy<ConnectionMultiplexer> LazyConnection;

        static RedisStore()
        {
            //var configurationOptions = new ConfigurationOptions
            //{
            //    EndPoints = { "127.0.0.1:6379,password=12345" }
            //};

            LazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect("127.0.0.1:6379,password=12345"));
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
