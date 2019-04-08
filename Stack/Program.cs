using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack1
{
    public class Product
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public int SellPrice { get; set; }
    }
    class Program
    {
        public static Stack<Product> productStack = new Stack<Product>(50000);//定义一个栈-----存在并发风险
        //Net 4.0以后，微软提供了线程安全的先进后出集合
        public static ConcurrentStack<Product> productSK = new ConcurrentStack<Product>();//无需考虑并发
        static void Main(string[] args)
        {
            Stack st = new Stack();

            st.Push('A');
            st.Push('M');
            st.Push('G');
            st.Push('W');

            Console.WriteLine("Current stack: ");
            foreach (char c in st)
            {
                Console.Write(c + " ");
            }
            Console.WriteLine();

            st.Push('V');
            st.Push('H');
            Console.WriteLine("The next poppable value in stack: {0}",
            st.Peek());
            Console.WriteLine("Current stack: ");
            foreach (char c in st)
            {
                Console.Write(c + " ");
            }
            Console.WriteLine();

            Console.WriteLine("Removing values ");
            st.Pop();
            st.Pop();
            st.Pop();

            Console.WriteLine("Current stack: ");
            foreach (char c in st)
            {
                Console.Write(c + " ");
            }
            Console.ReadKey();
        }

        public static void RuZhan() //定义一个入栈方法  先进后出
        {
            for (int i = 1; i < 10001; i++)
            {
                Product model = new Product() { Name = "商品" + i, Category = "水果", SellPrice = 10 };
                productStack.Push(model);
            }
        }

        public static void RuZhanCC() //保证线程安全的入栈方法
        {
            for (int i = 1; i < 10001; i++)
            {
                Product model = new Product() { Name = "商品" + i, Category = "水果", SellPrice = 10 };
                productSK.Push(model);
            }
        }
    }
}
