using RedisHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace redis集群1
{
    class Program
    {
        static void Main(string[] args)
        {
            //https://segmentfault.com/a/1190000018106844?utm_source=tag-newest

            //假设有3个用户同时购买一件商品，商品库存只剩下1，如果3个用户同时购买，
            //负载均衡把3个用户分别指向站点1、2、3，
            //那结果将会是3个用户都购买成功。下面我们使用分布式锁解决这个问题。

            //1、在分布式系统环境下，一个锁在同一时间只能被一个服务器获取；（这是所有分布式锁的基础）
            //2、高性能的获取锁和释放锁；（锁用完了，要及时释放，以供别人继续使用）
            //3、具备锁失效机制，防止死锁；（防止因为某些意外，锁没有得到释放，那别人将永远无法使用）
            //4、具备非阻塞锁特性，即没有获取到锁将直接返回获取锁失败。（满足等待锁的同时，也要满足非阻塞锁特性，便于多样性的业务场景使用）

            RedisClient myRedisClient = new RedisClient(RedisSingletonConnection.Instance);

            Parallel.For(0, 100, (i) =>
              {
                  if (myRedisClient.Lock.LockTake("testKey", "testValue", TimeSpan.FromSeconds(10)))
                  {
                      try
                      {
                          Console.WriteLine("10秒内进来的请求");
                          System.Threading.Thread.Sleep(9000);
                      }
                      finally
                      {
                          myRedisClient.Lock.LockRelease("testKey", "testValue");
                      }
                  }
                  else
                  {
                      Console.WriteLine("该请求被拒绝");
                  }
              });

            Console.WriteLine("Over..........");
            Console.ReadKey();
        }
    }
}
