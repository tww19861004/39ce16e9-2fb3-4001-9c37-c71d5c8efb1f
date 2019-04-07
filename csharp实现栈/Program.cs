using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp实现栈
{
    public class MyStack<T> : IEnumerable<T>, IDisposable
    {
        private int _top = 0;
        private int _size = 0;
        private T[] _stack = null;

        public int Top
        {
            get
            {
                return _top;
            }
        }

        public int Size
        {
            get
            {
                return _size;
            }
        }

        public int Length
        {
            get
            {
                return _top;
            }
        }

        public T this[int index]
        {
            get
            {
                return _stack[index];
            }
        }

        public MyStack(int size)
        {
            _size = size;
            _top = 0;
            _stack = new T[size];
        }

        public bool IsEmpty()
        {
            return _top == 0;
        }

        public bool IsFull()
        {
            return _top == _size;
        }

        public void Clear()
        {
            _top = 0;
        }

        /// <summary>
        /// 入栈
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool Push(T node)
        {
            if (!IsFull())
            {
                _stack[_top] = node;
                _top++;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 出栈
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            T node = default(T);
            if (!IsEmpty())
            {
                _top--;
                node = _stack[_top];
            }
            return node;
        }

        public bool Contains(T obj)
        {
            int count = _size;
            while(count-->0)
            {
                if( _stack[count] == null)
                {
                    if (obj == null)
                        return true;
                }
                else
                {
                    return _stack[count].Equals(obj);
                }
            }
            return false;
        }

        public void Traverse()
        {
            for (int i = 0; i < _top; i++)
            {
                Console.WriteLine(_stack[i]);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _stack.Length; i++)
            {
                if (_stack[i] != null)
                {
                    yield return _stack[i];
                }
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public void Dispose()
        {
            _stack = null;
        }
    }
    public class Product
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public int SellPrice { get; set; }
    }


    class Program
    {
        static void Main(string[] args)
        {
            //队列在现实生活中的例子数不胜数。例如：排队打饭，排队购买机票，打印队列中等待处理的打印业务等
            //栈在生活中的例子也不少。例如：物流装车，火车调度等
            //https://www.cnblogs.com/yezhu008/p/5726234.html
            Product product = new Product() { Name = "1", Category = "2", SellPrice = 10 };
            MyStack<Product> list = new MyStack<Product>(1);
            list.Push(product);
            bool exists = list.Contains(product);
            Console.ReadKey();
        }
    }
}
