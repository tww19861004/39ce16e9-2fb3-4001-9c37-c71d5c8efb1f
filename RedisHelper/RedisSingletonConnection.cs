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
        private RedisSingletonConnection() { }
        private static ConnectionMultiplexer _Instance;
        private static readonly Object locker = new Object();

        public static ConnectionMultiplexer Instance
        {
            get
            {
                if (_Instance == null || !_Instance.IsConnected)
                {
                    lock (locker)
                    {
                        if (_Instance == null || !_Instance.IsConnected)
                        {
                            _Instance = CreateConnection();
                        }
                    }
                }
                return _Instance;
            }
        }

        private static ConnectionMultiplexer CreateConnection()
        {
            //在很多常见的情况下，StackExchange.Redis 将会自动的配置多个设置选项，包括服务器类型和版本，连接超时和主/从关系配置。可是有时候在Redis服务器这个命令是被禁止的。在这种情况下，提供更多的信息是非常有用的：
            ConfigurationOptions configOptions = new ConfigurationOptions
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

            var connect = ConnectionMultiplexer.Connect(configOptions);
            Console.WriteLine("create new redis connection success.");
            LogAsync("create new redis connection success.");
            RegisterConnectionEvent(connect);
            return connect;
        }

        private static void LogAsync(string msg, string path = "")
        {
            Action<string, string> logAction = (msg1, path1) =>
            {
                path1 = string.IsNullOrEmpty(path1) ? $"d:\\{DateTime.Now.ToString("yyyy-MM-dd")}redisconnectionlog.txt" : path1;
                try
                {
                    System.IO.File.AppendAllText(path1, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.sss")}:{msg1}{Environment.NewLine}");
                }
                catch (Exception ex)
                {
                    System.IO.File.AppendAllText(path1, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.sss")}:{ex.Message}{Environment.NewLine}");
                }
            };
            logAction.BeginInvoke(msg, path, null, null);

        }

        private static void RegisterConnectionEvent(ConnectionMultiplexer connect)
        {
            //注册如下事件
            connect.ConnectionFailed += MuxerConnectionFailed;
            connect.ConnectionRestored += MuxerConnectionRestored;
            connect.ErrorMessage += MuxerErrorMessage;
            connect.ConfigurationChanged += MuxerConfigurationChanged;
            connect.HashSlotMoved += MuxerHashSlotMoved;
            connect.InternalError += MuxerInternalError;
            connect.ConfigurationChangedBroadcast += ConnMultiplexer_ConfigurationChangedBroadcast;
        }

        #region 事件

        /// <summary>
        /// 重新配置广播时（通常意味着主从同步更改）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_ConfigurationChangedBroadcast(object sender, EndPointEventArgs e)
        {
            Console.WriteLine($"{nameof(ConnMultiplexer_ConfigurationChangedBroadcast)}: {e.EndPoint}");
            LogAsync($"{nameof(ConnMultiplexer_ConfigurationChangedBroadcast)}: {e.EndPoint}");
        }

        /// <summary>
        /// 配置更改时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerConfigurationChanged(object sender, EndPointEventArgs e)
        {
            Console.WriteLine("Configuration changed: " + e.EndPoint);
            LogAsync("Configuration changed: " + e.EndPoint);
        }

        /// <summary>
        /// 发生错误时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerErrorMessage(object sender, RedisErrorEventArgs e)
        {
            Console.WriteLine("ErrorMessage: " + e.Message);
            LogAsync("ErrorMessage: " + e.Message);
        }

        /// <summary>
        /// 重新建立连接之前的错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerConnectionRestored(object sender, ConnectionFailedEventArgs e)
        {
            Console.WriteLine("ConnectionRestored: " + e.EndPoint);
            LogAsync("ConnectionRestored: " + e.EndPoint);
        }

        /// <summary>
        /// 连接失败 ， 如果重新连接成功你将不会收到这个通知
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            Console.WriteLine("重新连接：Endpoint failed: " + e.EndPoint + ", " + e.FailureType + (e.Exception == null ? "" : (", " + e.Exception.Message)));

            LogAsync("重新连接：Endpoint failed: " + e.EndPoint + ", " + e.FailureType + (e.Exception == null ? "" : (", " + e.Exception.Message)));
        }

        /// <summary>
        /// 更改集群
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerHashSlotMoved(object sender, HashSlotMovedEventArgs e)
        {
            Console.WriteLine("HashSlotMoved:NewEndPoint" + e.NewEndPoint + ", OldEndPoint" + e.OldEndPoint);
            LogAsync("HashSlotMoved:NewEndPoint" + e.NewEndPoint + ", OldEndPoint" + e.OldEndPoint);
        }

        /// <summary>
        /// redis类库错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerInternalError(object sender, InternalErrorEventArgs e)
        {
            Console.WriteLine("InternalError:Message" + e.Exception.Message);
            LogAsync("InternalError:Message" + e.Exception.Message);
        }

        #endregion 事件
    }
}
