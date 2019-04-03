using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 队列实现栈
{
    class Program
    {
        static void Main(string[] args)
        {
            //使用队列实现栈的下列操作：

            //push(x)-- 元素 x 入栈
            //pop()-- 移除栈顶元素
            //top()-- 获取栈顶元素
            //empty()-- 返回栈是否为空

            //栈最大的一个特点就是先进后出(FILO—First - In / Last - Out)。
　　        //队列和栈不同的是，队列是一种先进先出(FIFO—first in first out)的数据结构


        }

        public class MyStack
        {

            private Queue<int> _queue = null;

            public MyStack()
            {
                _queue = new Queue<int>();
            }

            public void Push(int x)
            {
                //基本思路是反转原队列
                var queue = new Queue<int>();
                queue.Enqueue(x);
                foreach (var elemet in _queue)
                {
                    queue.Enqueue(elemet);
                }
                _queue = queue;
            }

            public int Pop()
            {
                return _queue.Dequeue();
            }

            public int Top()
            {
                return _queue.First();
            }

            public bool Empty()
            {
                return !_queue.Any();
            }

        }
    }
}
