using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common.Utility.Cache
{
    public class HttpRuntimeCache : ICache
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool Exists(string key)
        {
            throw new NotImplementedException();
        }

        public T GetCache<T>(string key)
        {
            throw new NotImplementedException();
        }

        public void RemoveCache(string key)
        {
            throw new NotImplementedException();
        }

        public bool SetCache<T>(string key, T value, int expired = 10000)
        {
            throw new NotImplementedException();
        }
    }
}
