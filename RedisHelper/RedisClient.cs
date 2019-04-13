using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedisHelper
{
    public class RedisClient
    {
        protected IDatabase Core;

        protected ConnectionMultiplexer RedisConn;
        public RedisClient(ConnectionMultiplexer redisConn)
        {
            var redis = redisConn.GetDatabase();
            this.Core = redis;
            this.RedisConn = redisConn;
            String = new RedisString(redis);
            List = new RedisList(redis);
            Set = new RedisSet(redis);
            Hash = new RedisHash(redis);
            Key = new RedisKey(redis);
        }

        //~MyRedisClient()
        //{
        //    if (this.RedisConn != null)
        //    {
        //        this.RedisConn.Close();
        //        this.RedisConn.Dispose();
        //    }
        //}

        /// <summary>
        /// 字符串类型操作
        /// </summary>
        public RedisString String { get; set; }

        /// <summary>
        /// 列表类型操作
        /// </summary>
        public RedisList List { get; set; }

        /// <summary>
        /// Set类型操作
        /// </summary>
        public RedisSet Set { get; set; }

        /// <summary>
        /// Key类型操作
        /// </summary>
        public RedisKey Key { get; set; }

        /// <summary>
        /// Hash类型操作
        /// </summary>
        public RedisHash Hash { get; set; }
    }

    /// <summary>
    /// Redis操作基类
    /// </summary>
    public class BaseRedis
    {
        protected IDatabase Core;
        public BaseRedis(IDatabase redis)
        {
            this.Core = redis;
        }
    }


    /// <summary>
    /// key操作
    /// </summary>
    public class RedisKey : BaseRedis
    {
        public RedisKey(IDatabase redis) : base(redis)
        {

        }

        /// <summary>
        /// 键是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> KeyExistsAsync(string key)
        {
            return await Core.KeyExistsAsync(key);
        }

        /// <summary>
        /// 键是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyExists(string key)
        {
            return Core.KeyExists(key);
        }

        /// <summary>
        /// 设置键的失效时间
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> KeyExpireAsync(string key, TimeSpan? time = null)
        {
            return await Core.KeyExpireAsync(key, time);
        }


        /// <summary>
        /// 设置键的失效时间
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyExpire(string key, TimeSpan? time = null)
        {
            return Core.KeyExpire(key, time);
        }


        /// <summary>
        /// 设置键的失效时间
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> KeyExpireAsync(string key, DateTime? time = null)
        {
            return await Core.KeyExpireAsync(key, time);
        }


        /// <summary>
        /// 设置键的失效时间
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyExpire(string key, DateTime? time = null)
        {
            return Core.KeyExpire(key, time);
        }

        /// <summary>
        /// 删除键
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> KeyDeleteAsync(string key)
        {
            return await Core.KeyDeleteAsync(key);
        }

        /// <summary>
        /// 删除键
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyDelete(string key)
        {
            return Core.KeyDelete(key);
        }
    }


    /// <summary>
    /// 字符串操作
    /// </summary>
    public class RedisString : BaseRedis
    {
        public RedisString(IDatabase redis) : base(redis)
        {

        }
        #region 赋值
        /// <summary>
        /// 设置key的value
        /// </summary>
        public async Task<bool> SetAsync(string key, string value, TimeSpan? dt = null)
        {
            return await Core.StringSetAsync(key, value, dt);
        }

        /// <summary>
        /// 设置key的value
        /// </summary>
        public bool Set(string key, string value, TimeSpan? dt = null)
        {
            return Core.StringSet(key, value, dt);
        }

        #endregion
        #region 追加
        /// <summary>
        /// 在原有key的value值之后追加value
        /// </summary>
        public async Task<long> AppendAsync(string key, string value)
        {
            return await Core.StringAppendAsync(key, value);
        }
        /// <summary>
        /// 在原有key的value值之后追加value
        /// </summary>
        public long Append(string key, string value)
        {
            return Core.StringAppend(key, value);
        }
        #endregion
        #region 获取值
        /// <summary>
        /// 获取key的value值
        /// </summary>
        public async Task<string> GetAsync(string key)
        {
            return await Core.StringGetAsync(key);
        }
        /// <summary>
        /// 获取key的value值
        /// </summary>
        public string Get(string key)
        {
            return Core.StringGet(key);
        }
        #endregion
        #region 获取旧值赋上新值
        /// <summary>
        /// 获取旧值赋上新值
        /// </summary>
        public async Task<string> GetAndSetValueAsync(string key, string value)
        {
            return await Core.StringGetSetAsync(key, value);
        }
        /// <summary>
        /// 获取旧值赋上新值
        /// </summary>
        public string GetAndSetValue(string key, string value)
        {
            return Core.StringGetSet(key, value);
        }
        #endregion
        #region 辅助方法
        /// <summary>
        /// 获取值的长度
        /// </summary>
        public async Task<long> GetCountAsync(string key)
        {
            return await Core.StringLengthAsync(key);
        }
        /// <summary>
        /// 获取值的长度
        /// </summary>
        public long GetCount(string key)
        {
            return Core.StringLength(key);
        }
        /// <summary>
        /// 自增1，返回自增后的值
        /// </summary>
        public async Task<long> IncrAsync(string key)
        {
            return await Core.StringIncrementAsync(key);
        }
        /// <summary>
        /// 自增1，返回自增后的值
        /// </summary>
        public long Incr(string key)
        {
            return Core.StringIncrement(key);
        }
        /// <summary>
        /// 自增count，返回自增后的值
        /// </summary>
        public async Task<double> IncrByAsync(string key, double count)
        {
            return await Core.StringIncrementAsync(key, count);
        }
        /// <summary>
        /// 自增count，返回自增后的值
        /// </summary>
        public double IncrBy(string key, double count)
        {
            return Core.StringIncrement(key, count);
        }
        /// <summary>
        /// 自减1，返回自减后的值
        /// </summary>
        public async Task<long> DecrAsync(string key)
        {
            return await Core.StringDecrementAsync(key);
        }
        /// <summary>
        /// 自减1，返回自减后的值
        /// </summary>
        public long Decr(string key)
        {
            return Core.StringDecrement(key);
        }

        /// <summary>
        /// 自减count ，返回自减后的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<long> DecrByAsync(string key, int count)
        {
            return await Core.StringDecrementAsync(key, count);
        }
        /// <summary>
        /// 自减count ，返回自减后的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public long DecrBy(string key, int count)
        {
            return Core.StringDecrement(key, count);
        }


        #endregion
    }

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

    /// <summary>
    /// RedisSet操作
    /// </summary>
    public class RedisSet : BaseRedis
    {
        public RedisSet(IDatabase redis) : base(redis)
        {

        }
        #region 添加
        /// <summary>
        /// key集合中添加value值
        /// </summary>
        public async Task AddAsync(string key, string value)
        {
            await Core.SetAddAsync(key, value);
        }

        /// <summary>
        /// key集合中添加value值
        /// </summary>
        public void Add(string key, string value)
        {
            Core.SetAdd(key, value);
        }

        /// <summary>
        /// key集合中添加value值
        /// </summary>
        public async Task AddAsync(string key, string[] value)
        {
            await Core.SetAddAsync(key, value.Select(z => (RedisValue)z).ToArray());
        }

        /// <summary>
        /// key集合中添加value值
        /// </summary>
        public void Add(string key, string[] value)
        {
            Core.SetAdd(key, value.Select(z => (RedisValue)z).ToArray());
        }

        #endregion
        #region 获取
        /// <summary>
        /// 随机获取key集合中的一个值
        /// </summary>
        public async Task<string> GetRandomItemFromSetAsync(string key)
        {
            return await Core.SetRandomMemberAsync(key);
        }

        /// <summary>
        /// 随机获取key集合中的一个值
        /// </summary>
        public string GetRandomItemFromSet(string key)
        {
            return Core.SetRandomMember(key);
        }

        /// <summary>
        /// 获取key集合值的数量
        /// </summary>
        public async Task<long> GetCountAsync(string key)
        {
            return await Core.SetLengthAsync(key);
        }
        /// <summary>
        /// 获取key集合值的数量
        /// </summary>
        public long GetCount(string key)
        {

            return Core.SetLength(key);
        }

        /// <summary>
        /// 键中是否包含
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Contains(string key, string value)
        {
            return Core.SetContains(key, value);
        }

        /// <summary>
        /// 键中是否包含
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<bool> ContainsAsync(string key, string value)
        {
            return await Core.SetContainsAsync(key, value);
        }

        /// <summary>
        /// 获取所有key集合的值
        /// </summary>
        public async Task<HashSet<string>> SetMembersAsync(string key)
        {
            var result = await Core.SetMembersAsync(key);
            if (result != null)
            {
                return new HashSet<string>(result.Select(z => z.ToString()).ToList());
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取所有key集合的值
        /// </summary>
        public HashSet<string> SetMembers(string key)
        {
            var result = Core.SetMembers(key);
            if (result != null)
            {
                return new HashSet<string>(result.Select(z => z.ToString()).ToList());
            }
            else
            {
                return null;
            }
        }
        #endregion
        #region 删除
        /// <summary>
        /// 随机删除key集合中的一个值
        /// </summary>
        public async Task<string> SetPopAsync(string key)
        {
            return await Core.SetPopAsync(key);
        }
        /// <summary>
        /// 随机删除key集合中的一个值
        /// </summary>
        public string SetPop(string key)
        {
            return Core.SetPop(key);
        }

        /// <summary>
        /// 删除key集合中的value
        /// </summary>
        public async Task RemoveItemFromSetAsync(string key, string value)
        {
            await Core.SetRemoveAsync(key, value);
        }

        /// <summary>
        /// 删除key集合中的value
        /// </summary>
        public void RemoveItemFromSet(string key, string value)
        {
            Core.SetRemove(key, value);
        }
        #endregion
    }



    /// <summary>
    /// RedisHash操作
    /// </summary>
    public class RedisHash : BaseRedis
    {
        public RedisHash(IDatabase redis) : base(redis)
        {

        }
        #region 添加
        /// <summary>
        /// 向key集合中添加key/value
        /// </summary>       
        public async Task<bool> HashSetAsync(string key, string fieldname, string value)
        {
            return await Core.HashSetAsync(key, fieldname, value);
        }

        /// <summary>
        /// 向key集合中添加key/value
        /// </summary>       
        public bool HashSet(string key, string fieldname, string value)
        {
            return Core.HashSet(key, fieldname, value);
        }
        #endregion
        #region 获取
        /// <summary>
        /// 获取key中制定fieldname的数据
        /// </summary>
        public async Task<string> HashGetAsync(string key, string fieldname)
        {
            return await Core.HashGetAsync(key, fieldname);
        }
        /// <summary>
        /// 获取key中制定fieldname的数据
        /// </summary>
        public string HashGet(string key, string fieldname)
        {
            return Core.HashGet(key, fieldname);
        }

        /// <summary>
        /// 获取所有key数据集的key/value数据集合
        /// </summary>
        public async Task<Dictionary<string, string>> HashGetAllAsync(string key)
        {
            var result = await Core.HashGetAllAsync(key);
            if (result == null)
            {
                return null;
            }
            return result.ToDictionary(z => z.Name.ToString(), z => z.Value.ToString());
        }
        /// <summary>
        /// 获取所有key数据集的key/value数据集合
        /// </summary>
        public Dictionary<string, string> HashGetAll(string key)
        {
            var result = Core.HashGetAll(key);
            if (result == null)
            {
                return null;
            }
            return result.ToDictionary(z => z.Name.ToString(), z => z.Value.ToString());
        }

        /// <summary>
        /// 获取key数据集中的数据总数
        /// </summary>
        public async Task<long> HashLengthAsync(string key)
        {
            return await Core.HashLengthAsync(key);
        }
        /// <summary>
        /// 获取key数据集中的数据总数
        /// </summary>
        public long HashLength(string key)
        {
            return Core.HashLength(key);
        }

        #endregion
        #region 删除
        #endregion
        /// <summary>
        /// 删除key数据集中的fieldname数据
        /// </summary>
        public async Task<bool> HashDeleteAsync(string key, string fieldname)
        {
            return await Core.HashDeleteAsync(key, fieldname);
        }
        /// <summary>
        /// 删除key数据集中的fieldname数据
        /// </summary>
        public bool HashDelete(string key, string fieldname)
        {
            return Core.HashDelete(key, fieldname);
        }
        #region 其它
        /// <summary>
        /// 判断key数据集中是否存在fieldname的数据
        /// </summary>
        public async Task<bool> HashContainsEntryAsync(string key, string fieldname)
        {
            return await Core.HashExistsAsync(key, fieldname);
        }
        /// <summary>
        /// 判断key数据集中是否存在fieldname的数据
        /// </summary>
        public bool HashContainsEntry(string key, string fieldname)
        {
            return Core.HashExists(key, fieldname);
        }

        /// <summary>
        /// 给key数据集fieldname的value加countby，返回相加后的数据
        /// </summary>
        public async Task<double> HashIncrementAsync(string key, string fieldname, double countBy)
        {
            return await Core.HashIncrementAsync(key, fieldname, countBy);
        }
        /// <summary>
        /// 给key数据集fieldname的value加countby，返回相加后的数据
        /// </summary>
        public double HashIncrement(string key, string fieldname, double countBy)
        {
            return Core.HashIncrement(key, fieldname, countBy);
        }

        /// <summary>
        /// 给key数据集fieldname的value加countby，返回相减后的数据
        /// </summary>
        public async Task<double> IncrementValueInHashAsync(string key, string fieldname, double countBy)
        {
            return await Core.HashDecrementAsync(key, fieldname, countBy);
        }

        /// <summary>
        /// 给key数据集fieldname的value加countby，返回相减后的数据
        /// </summary>
        public double IncrementValueInHash(string key, string fieldname, double countBy)
        {
            return Core.HashDecrement(key, fieldname, countBy);
        }
        #endregion
    }
}
