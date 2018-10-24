using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SingletonLazy1
{
    public sealed class Singleton
    {
        private static readonly Lazy<Singleton> lazy =
            new Lazy<Singleton>(() => new Singleton(), LazyThreadSafetyMode.PublicationOnly);

        public static Singleton Instance { get { return lazy.Value; } }

        private Singleton()
        {
            Console.WriteLine("Singleton Constructing");
        }
    }

    public class User
    {
        private User()
        {
            this.id = Guid.NewGuid().ToString();
        }
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Singleton<T>
    {
        private static Lazy<T> _factoryLazy = new Lazy<T>(
            () => (T)Activator.CreateInstance(typeof(T)),
            LazyThreadSafetyMode.ExecutionAndPublication);

        public static T Instance
        {
            get
            {
                return _factoryLazy.Value;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Parallel.For(0, 10000, n =>
              {
                  Singleton.Instance.ToString();
              });
            //Parallel.For(0, 100, n =>
            //  {
            //      Singleton<User>.Instance.ToString();
            //  });
            Console.ReadKey();
        }
    }
}
