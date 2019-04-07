using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp实现队列
{
    class Queue
    {
        object[] data;  //数据元素  
        int maxsize;    //最大容量  
        int front;      //指向队头  
        int rear;       //指向队尾  

        //初始化队列  
        public Queue(int size)
        {

            this.maxsize = size;
            data = new object[size];
            front = rear = -1;
        }

        //最大容量属性  
        public int MaxSize
        {
            get
            {
                return this.maxsize;
            }
            set
            {
                this.maxsize = value;
            }
        }

        //队尾属性  
        public int Rear
        {
            get
            {
                return this.rear;
            }
        }

        //队头属性  
        public int Front
        {
            get
            {
                return this.front;
            }
        }

        //数据属性  
        public object this[int index]
        {
            get
            {
                return data[index];
            }
        }

        //获得队列的长度  
        public int GetQueueLength()
        {
            return rear - front;
        }

        //判断队列是否满  
        public bool IsFull()
        {
            if (GetQueueLength() == maxsize)
                return true;
            else
                return false;
        }

        //判断队列是否为空  
        public bool IsEmpty()
        {
            if (rear == front)
                return true;
            else
                return false;
        }

        //清空队列  
        public void ClearQueue()
        {
            rear = front = -1;
        }

        //入队  
        public void In(object e)
        {

            if (IsFull())
            {
                Console.WriteLine("队列已满！");
                return;
            }
            data[++rear] = e;
        }

        //出队  
        public object Out()
        {

            if (IsEmpty())
            {
                Console.WriteLine("队列为空！");
                return null;
            }

            if (rear - front > 0)
            {
                object tmp = data[++front];
                return tmp;
            }
            else
            {
                Console.WriteLine("全出队了！");
                ClearQueue();
                return null;
            }
        }

        //获得队头元素  
        public object GetHead()
        {

            if (IsEmpty())
            {
                Console.WriteLine("队列为空！");
                return null;
            }
            return data[front + 1];
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
}
