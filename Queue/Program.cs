using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Queue
{
    public class Product
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public int SellPrice { get; set; }
    }
    class Program
    {
        public static Queue<Product> productQueue = new Queue<Product>(50000);//定义一个队列-----存在并发风险
                                                                              //Net 4.0以后，微软提供了线程安全的先进先出集合 
        public static ConcurrentQueue<Product> productCQ = new ConcurrentQueue<Product>();//无需考虑并发
        static void Main(string[] args)
        {
            Parallel.For(0, 11, (i) =>
              {
                  Product model = new Product() { Name = "商品" + i, Category = "水果", SellPrice = i };
                  productQueue.Enqueue(model);

                  //Console.WriteLine(string.Format("Beginning iteration {0},Time = {1},IsThreadPool={2},ManagedThreadId={3}", i, DateTime.Now.ToString("yyyy-mm-dd hh-mm-ss"),Thread.CurrentThread.IsThreadPoolThread,Thread.CurrentThread.ManagedThreadId));
                  //Thread.Sleep(10);
              });
            
            Console.ReadKey();
        }

        public static void RuDui() //定义一个入队方法  先进先出
        {
            for (int i = 1; i < 10001; i++)
            {
                Product model = new Product() { Name = "商品" + i, Category = "水果", SellPrice = 10 };
                productQueue.Enqueue(model);
            }
        }

        public static void RuDuiCC() //保证线程安全的入队方法
        {
            for (int i = 1; i < 10001; i++)
            {
                Product model = new Product() { Name = "商品" + i, Category = "水果", SellPrice = 10 };
                productCQ.Enqueue(model);
            }
        }
    }

}
