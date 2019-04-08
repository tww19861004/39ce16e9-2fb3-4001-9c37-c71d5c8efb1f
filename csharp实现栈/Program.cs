using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp实现栈
{
    interface IStackDS<T>
    {
        int Count { get; }//用来取得数据
        int GetLength();
        bool IsEmpty();
        void Clear();
        void Push(T item);
        T Peek();
        T Pop();
    }

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

        public T Peek()
        {
            T node = default(T);
            if (!IsEmpty())
            {
                node = _stack[_top-1];
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
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Person p1 = new Person
            {
                Name = "Flyee",
                Age = 24
            };
            Person p2 = new Person
            {
                Name = "Flyee2",
                Age = 18
            };

            Stack<Person> stack = new Stack<Person>(2);

            MyStack<Person> myStack = new MyStack<Person>(2);
            myStack.Push(p1);
            myStack.Push(p2);

            foreach (var p in myStack)
            {
                Console.WriteLine(p.Name);
            }
            Console.WriteLine(myStack[1].Age);
            Person result = myStack.Pop();
            Console.WriteLine(myStack.Length);
            Console.WriteLine("Name:" + result.Name + " Age:" + result.Age);
            foreach (var p in myStack)
            {
                Console.WriteLine(p.Name);
            }
            myStack.Pop();
            Console.WriteLine(myStack.Length);
            myStack.Dispose();

            Console.ReadKey();
        }
    }
}
