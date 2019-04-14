using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisHelper
{
    public class RedisLock : BaseRedis
    {
        public RedisLock(IDatabase redis) : base(redis)
        {

        }

        public bool LockTake(string key,string value,TimeSpan ts)
        {            
            return Core.LockTake(key, value, ts);
        }

        public bool LockRelease(string key, string value)
        {
            return Core.LockRelease(key, value);
        }
    }
}
