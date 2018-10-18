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

    class Program
    {
        static void Main(string[] args)
        {
            Parallel.For(0, 100, n =>
              {
                  Singleton.Instance.ToString();
              });
            Console.ReadKey();
        }
    }
}
