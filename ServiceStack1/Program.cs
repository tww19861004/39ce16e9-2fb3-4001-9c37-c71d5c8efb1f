using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStack1
{
    public class ContactInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string phone { get; set; }
    }

    public class Name
    {
        /// <summary>
        /// 
        /// </summary>
        public string firstName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string lastName { get; set; }
    }

    public class GuestInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public Name name { get; set; }
    }

    public class RoomsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public GuestInfo guestInfo { get; set; }
    }

    public class OrderListItem
    {
        /// <summary>
        /// 
        /// </summary>
        public ContactInfo contactInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string elongOrderId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string merchantOrderId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string operatorType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<RoomsItem> rooms { get; set; }
    }

    public class Root
    {
        /// <summary>
        /// 正常
        /// </summary>
        public string errorMsg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<OrderListItem> orderList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int retCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string uid { get; set; }
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

            string str = "{\"errorMsg\":\"正常\",\"orderList\":[{\"contactInfo\":{\"email\":\"Intl_H_CS@corp.elong.com\",\"phone\":\"4009333333\"},\"elongOrderId\":\"20181210391550913\",\"merchantOrderId\":\"\",\"operatorType\":\"confirm\",\"pid\":\"5034332,61741,211,2019-02-05,2019-02-08,1488,1,2,0\",\"rooms\":[{\"guestInfo\":{\"name\":{\"firstName\":\"Xuyan\",\"lastName\":\"Wu\"}}},{\"guestInfo\":{\"name\":{\"firstName\":\"Pingping\",\"lastName\":\"Gu\"}}}]}],\"retCode\":0,\"uid\":\"cc357930-7dd3-4430-85ed-5643d3ece9da\"} ";
            Root root = Newtonsoft.Json.JsonConvert.DeserializeObject<Root>(str);

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
