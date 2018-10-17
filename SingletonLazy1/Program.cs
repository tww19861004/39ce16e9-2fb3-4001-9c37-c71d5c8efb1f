using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonLazy1
{
    public sealed class Singleton
    {
        private static readonly Lazy<Singleton> lazy =
            new Lazy<Singleton>(() => new Singleton());

        public static Singleton Instance { get { return lazy.Value; } }

        private Singleton()
        {
            Console.WriteLine("Singleton Constructing");
        }
    }

    /// <summary>
    /// 线程不安全
    /// </summary>
    public sealed class Singleton1
    {
        public static string id;
        static Singleton1()
        {
            Console.WriteLine("静态构造函数");
        }

        /// <summary>
        /// Prevents a default instance of the 
        /// <see cref="Singleton"/> class from being created.
        /// </summary>
        //public Singleton1()
        //{
        //    Console.WriteLine("共有构造函数");
        //}

        private Singleton1()
        {
            Console.WriteLine("私有构造函数");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //Parallel.For(0, 10, n =>
            //  {
            //      Console.Write(Singleton1.Instance.ToString()+"\r\n");                  
            //  });
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(Singleton1.id);
            }
            Console.ReadKey();
        }
    }
}
