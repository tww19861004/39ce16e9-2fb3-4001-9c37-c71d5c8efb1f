using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 单向链表
{
    //=====单向链表元素=====
    class Node<T>
    {
        public T Value;
        public Node<T> NextNode;

        public Node() : this(default(T)) { }
        public Node(T value)
        {
            Value = value;
            NextNode = null;
        }
    }

    public class SinglyLinkedList<T>
    {

    }

    class Program
    {
        static void Main(string[] args)
        {

        }
    }
}
