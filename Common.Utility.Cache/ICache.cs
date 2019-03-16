using System;

namespace Common.Utility.Cache
{
    public interface ICache
    {
        /// <summary>
        /// 缓存过期时间
        /// </summary>
        int TimeOut { set; get; }

        bool Exists(string key);
        
        T Get<T>(string key);
        
        bool Set<T>(string key, T value, int expired = 10000);                

        void Remove(string key);

        void Dispose();
    }
}
