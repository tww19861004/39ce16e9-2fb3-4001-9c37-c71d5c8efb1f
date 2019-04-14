using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedisHelper
{
    /// <summary>
    /// Redis列表操作
    /// </summary>
    public class RedisList : BaseRedis
    {
        public RedisList(IDatabase redis) : base(redis)
        {

        }
        #region 赋值
        /// <summary>
        /// 从左侧向list中添加值
        /// </summary>
        public async Task LPushAsync(string key, string value)
        {
            await Core.ListLeftPushAsync(key, value);
        }

        /// <summary>
        /// 从左侧向list中添加值
        /// </summary>
        public void LPush(string key, string value)
        {
            Core.ListLeftPush(key, value);
        }

        /// <summary>
        /// 从左侧向list中添加值，设置过期时间
        /// </summary>
        public async Task LPushAsync(string key, string value, TimeSpan sp)
        {
            await Core.ListLeftPushAsync(key, value);
            await Core.KeyExpireAsync(key, sp);
        }

        /// <summary>
        /// 从左侧向list中添加值，设置过期时间
        /// </summary>
        public void LPush(string key, string value, TimeSpan sp)
        {
            Core.ListLeftPush(key, value);
            Core.KeyExpire(key, sp);
        }
        /// <summary>
        /// 从左侧向list中添加值
        /// </summary>
        public async Task RPushAsync(string key, string value)
        {
            await Core.ListRightPushAsync(key, value);
        }

        /// <summary>
        /// 从左侧向list中添加值
        /// </summary>
        public void RPush(string key, string value)
        {
            Core.ListRightPush(key, value);
        }

        /// <summary>
        /// 从右侧向list中添加值，并设置过期时间
        /// </summary>        
        public async Task RPushAsync(string key, string value, TimeSpan sp)
        {
            await Core.ListRightPushAsync(key, value);
            await Core.KeyExpireAsync(key, sp);

        }
        /// <summary>
        /// 从右侧向list中添加值，并设置过期时间
        /// </summary>        
        public void RPush(string key, string value, TimeSpan sp)
        {
            Core.ListRightPush(key, value);
            Core.KeyExpire(key, sp);

        }


        #endregion
        #region 获取值
        /// <summary>
        /// 获取list中key包含的数据数量
        /// </summary>  
        public async Task<long> CountAsync(string key)
        {
            return await Core.ListLengthAsync(key);
        }
        /// <summary>
        /// 获取list中key包含的数据数量
        /// </summary>  
        public long Count(string key)
        {
            return Core.ListLength(key);
        }
        /// <summary>
        /// 获取key包含的所有数据集合
        /// </summary>  
        public async Task<List<string>> GetAsync(string key, long start = 0, long stop = -1)
        {
            var res = await Core.ListRangeAsync(key, start, stop);
            if (res == null)
            {
                return null;
            }
            return res.Select(z => z.ToString()).ToList();
        }
        /// <summary>
        /// 获取key包含的所有数据集合
        /// </summary>  
        public List<string> Get(string key, long start = 0, long stop = -1)
        {
            var res = Core.ListRange(key, start, stop);
            if (res == null)
            {
                return null;
            }
            return res.Select(z => z.ToString()).ToList();
        }

        #endregion

        #region 删除
        /// <summary>
        /// 从尾部移除数据，返回移除的数据
        /// </summary>  
        public async Task<string> RPopAsync(string key)
        {
            return await Core.ListRightPopAsync(key);
        }
        /// <summary>
        /// 从尾部移除数据，返回移除的数据
        /// </summary>  
        public string RPop(string key)
        {
            return Core.ListRightPop(key);
        }

        /// <summary>
        /// 从list的头部移除一个数据，返回移除的数据
        /// </summary>  
        public async Task<string> LPopAsync(string key)
        {
            return await Core.ListLeftPopAsync(key);
        }

        /// <summary>
        /// 从list的头部移除一个数据，返回移除的数据
        /// </summary>  
        public string LPop(string key)
        {
            return Core.ListLeftPop(key);
        }

        #endregion

    }
}
