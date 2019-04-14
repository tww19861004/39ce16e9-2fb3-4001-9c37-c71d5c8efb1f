using StackExchange.Redis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisHelper
{
    public class RedisConnection
    {
        //在StackExchange.Redis 中最核心（中枢）的是 ConnectionMultiplexer 类，在所有调用之间它的实例对象应该被设计为在整个应用程序域中为共享和重用的，
        //并不应该为每一个操作都创建一个 ConnectionMultiplexer 对象实例，也就是说我们可以使用常见的单例模式进行创建。
        // 虽然 ConnectionMultiplexer实现了 IDisposable 接口，但这并不意味着需要使用using 进行释放，因为创建一个 ConnectionMultiplexer 对象是十分昂贵的 ， 所以最好的是我们一直重用一个 ConnectionMultiplexer 对象。
        //https://www.jb51.net/article/100446.htm

        private string _ResisConnectionName;
        private RedisConnection()
        {
            _ResisConnectionName = "default";
        }

        private List<string> _IpList;
        public List<string> IpList
        {
            get
            {
                return _IpList;
            }
        }

        private Lazy<ConnectionMultiplexer> _Connetion;
        public ConnectionMultiplexer Connetion
        {
            get
            {
                return _Connetion.Value;
            }
        }

        public RedisConnection(string resisConnectionName, List<string> ipList)
        {
            _IpList = ipList;
            _ResisConnectionName = resisConnectionName;
            _Connetion = new Lazy<ConnectionMultiplexer>(() =>
            {
                ConfigurationOptions config = new ConfigurationOptions
                {
                    EndPoints =
                {
                },
                    //AllowAdmin = true,
                    //Proxy = Proxy.Twemproxy,
                    Password = "12345"
                };
                ipList.ForEach((item) =>
                {
                    config.EndPoints.Add(item.Split(':')[0], Convert.ToInt32(item.Split(':')[1]));
                });
                return CreateConnection(config);
            });
        }

        private ConnectionMultiplexer CreateConnection(string connectionString)
        {
            var connect = ConnectionMultiplexer.Connect(connectionString);
            LogAsync($"redisconnection create success,name={(_ResisConnectionName)},iplist={(string.Join(",", IpList))}");
            RegisterConnectionEvent(connect);
            return connect;
        }

        private void LogAsync(string msg, string path = "")
        {            
            Action<string, string> logAction = (msg1, path1) =>
             {
                 path1 = string.IsNullOrEmpty(path1) ? $"d:\\{DateTime.Now.ToString("yyyy-MM-dd")}redisconnectionlog.txt" : path1;
                 try
                 {                     
                     System.IO.File.AppendAllText(path1, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.sss")}:{msg1}{Environment.NewLine}");
                 }
                 catch(Exception ex)
                 {
                     System.IO.File.AppendAllText(path1, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.sss")}:{ex.Message}{Environment.NewLine}");
                 }
             };
            logAction.BeginInvoke(msg, path, null, null);

        }

        private ConnectionMultiplexer CreateConnection(ConfigurationOptions config)
        {
            var connect = ConnectionMultiplexer.Connect(config);
            LogAsync($"redisconnection create success,name={(_ResisConnectionName)},iplist={(string.Join(",",IpList))}");
            RegisterConnectionEvent(connect);
            return connect;
        }

        private void RegisterConnectionEvent(ConnectionMultiplexer connect)
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
        private void ConnMultiplexer_ConfigurationChangedBroadcast(object sender, EndPointEventArgs e)
        {
            Console.WriteLine($"{nameof(ConnMultiplexer_ConfigurationChangedBroadcast)}: {e.EndPoint}");
            LogAsync($"{nameof(ConnMultiplexer_ConfigurationChangedBroadcast)}: {e.EndPoint}");
        }

        /// <summary>
        /// 配置更改时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MuxerConfigurationChanged(object sender, EndPointEventArgs e)
        {
            Console.WriteLine("Configuration changed: " + e.EndPoint);
            LogAsync("Configuration changed: " + e.EndPoint);
        }

        /// <summary>
        /// 发生错误时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MuxerErrorMessage(object sender, RedisErrorEventArgs e)
        {
            Console.WriteLine("ErrorMessage: " + e.Message);
            LogAsync("ErrorMessage: " + e.Message);
        }

        /// <summary>
        /// 重新建立连接之前的错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MuxerConnectionRestored(object sender, ConnectionFailedEventArgs e)
        {
            Console.WriteLine("ConnectionRestored: " + e.EndPoint);
            LogAsync("ConnectionRestored: " + e.EndPoint);
        }

        /// <summary>
        /// 连接失败 ， 如果重新连接成功你将不会收到这个通知
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MuxerConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            Console.WriteLine("重新连接：Endpoint failed: " + e.EndPoint + ", " + e.FailureType + (e.Exception == null ? "" : (", " + e.Exception.Message)));

            LogAsync("重新连接：Endpoint failed: " + e.EndPoint + ", " + e.FailureType + (e.Exception == null ? "" : (", " + e.Exception.Message)));
        }

        /// <summary>
        /// 更改集群
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MuxerHashSlotMoved(object sender, HashSlotMovedEventArgs e)
        {
            Console.WriteLine("HashSlotMoved:NewEndPoint" + e.NewEndPoint + ", OldEndPoint" + e.OldEndPoint);
            LogAsync("HashSlotMoved:NewEndPoint" + e.NewEndPoint + ", OldEndPoint" + e.OldEndPoint);
        }

        /// <summary>
        /// redis类库错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MuxerInternalError(object sender, InternalErrorEventArgs e)
        {
            Console.WriteLine("InternalError:Message" + e.Exception.Message);
            LogAsync("InternalError:Message" + e.Exception.Message);
        }

        #endregion 事件
    }
}
