using System;

namespace Common.Utility.Cache
{
    public interface ICache
    {
        bool Exists(string key);


        T GetCache<T>(string key);
        
        bool SetCache<T>(string key, T value, int expired = 10000);                

        void RemoveCache(string key);

        void Dispose();
    }
}
