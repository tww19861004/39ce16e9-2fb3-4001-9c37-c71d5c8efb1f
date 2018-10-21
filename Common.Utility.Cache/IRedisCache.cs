using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Cache
{
    public interface IRedisCache:ICache
    {
        long Increment(string key);
    }
}
