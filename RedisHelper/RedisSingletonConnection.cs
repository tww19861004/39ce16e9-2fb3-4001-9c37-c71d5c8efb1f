using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedisHelper
{
    public sealed class RedisSingletonConnection
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

        private static readonly Lazy<ConnectionMultiplexer> LazyConnection = new Lazy<ConnectionMultiplexer>(
            () =>
                {
                    var connect = ConnectionMultiplexer.Connect(configOptions);
                    //注册如下事件                    
                    connect.ConnectionFailed += MuxerConnectionFailed;
                    connect.ConnectionRestored += MuxerConnectionRestored;
                    connect.ErrorMessage += MuxerErrorMessage;
                    connect.ConfigurationChanged += MuxerConfigurationChanged;
                    connect.HashSlotMoved += MuxerHashSlotMoved;
                    connect.InternalError += MuxerInternalError;
                    connect.ConfigurationChangedBroadcast += ConnMultiplexer_ConfigurationChangedBroadcast;
                    return connect;
                }
        , LazyThreadSafetyMode.ExecutionAndPublication);

        private RedisSingletonConnection()
        {
            Console.WriteLine("Private RedisSingletonConnection Constructing");
        }

        public static ConnectionMultiplexer Connection => LazyConnection.Value;

        public static IDatabase Instance => Connection.GetDatabase();

        #region 事件

        /// <summary>
        /// 重新配置广播时（通常意味着主从同步更改）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_ConfigurationChangedBroadcast(object sender, EndPointEventArgs e)
        {
            Console.WriteLine($"{nameof(ConnMultiplexer_ConfigurationChangedBroadcast)}: {e.EndPoint}");
        }

        /// <summary>
        /// 配置更改时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerConfigurationChanged(object sender, EndPointEventArgs e)
        {
            Console.WriteLine("Configuration changed: " + e.EndPoint);
        }

        /// <summary>
        /// 发生错误时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerErrorMessage(object sender, RedisErrorEventArgs e)
        {
            Console.WriteLine("ErrorMessage: " + e.Message);
        }

        /// <summary>
        /// 重新建立连接之前的错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerConnectionRestored(object sender, ConnectionFailedEventArgs e)
        {
            Console.WriteLine("ConnectionRestored: " + e.EndPoint);
        }

        /// <summary>
        /// 连接失败 ， 如果重新连接成功你将不会收到这个通知
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            Console.WriteLine("重新连接：Endpoint failed: " + e.EndPoint + ", " + e.FailureType + (e.Exception == null ? "" : (", " + e.Exception.Message)));
        }

        /// <summary>
        /// 更改集群
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerHashSlotMoved(object sender, HashSlotMovedEventArgs e)
        {
            Console.WriteLine("HashSlotMoved:NewEndPoint" + e.NewEndPoint + ", OldEndPoint" + e.OldEndPoint);
        }

        /// <summary>
        /// redis类库错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerInternalError(object sender, InternalErrorEventArgs e)
        {
            Console.WriteLine("InternalError:Message" + e.Exception.Message);
        }

        #endregion 事件
    }
}
