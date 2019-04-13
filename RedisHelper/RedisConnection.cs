﻿using StackExchange.Redis;
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
        //　在 StackExchange.Redis 中最核心（中枢）的是 ConnectionMultiplexer 类，在所有调用之间它的实例对象应该被设计为在整个应用程序域中为共享和重用的，
        // 并不应该为每一个操作都创建一个 ConnectionMultiplexer 对象实例，也就是说我们可以使用常见的单例模式进行创建。
        // 虽然 ConnectionMultiplexer实现了 IDisposable 接口，但这并不意味着需要使用using 进行释放，因为创建一个 ConnectionMultiplexer 对象是十分昂贵的 ， 所以最好的是我们一直重用一个 ConnectionMultiplexer 对象。
        //https://www.jb51.net/article/100446.htm

        private string _ResisConnectionName;
        private RedisConnection()
        {
            _ResisConnectionName = "default";
        }

        private Lazy<ConnectionMultiplexer> _Connetion;
        public ConnectionMultiplexer Connetion
        {
            get
            {
                return _Connetion.Value;
            }
        }

        public RedisConnection(string resisConnectionName,string connectionString = "", ConfigurationOptions config = null)
        {
            _ResisConnectionName = resisConnectionName;
            if(string.IsNullOrEmpty(connectionString) && config == null)
            {
                throw new ArgumentException("connectionString or config 必须有一个有值");
            }
            _Connetion = new Lazy<ConnectionMultiplexer>(() =>
            {
                if(!string.IsNullOrEmpty(connectionString))
                {
                    ConnectionMultiplexer.Connect(connectionString);
                }
                else
                {
                    ConnectionMultiplexer.Connect(config);
                }
                return null;
            });
        }                

        private ConnectionMultiplexer CreateConnection(string connectionString)
        {
            var connect = ConnectionMultiplexer.Connect(connectionString);
            RegisterConnectionEvent(connect);
            return connect;
        }

        private ConnectionMultiplexer CreateConnection(ConfigurationOptions config)
        {
            var connect = ConnectionMultiplexer.Connect(config);
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
