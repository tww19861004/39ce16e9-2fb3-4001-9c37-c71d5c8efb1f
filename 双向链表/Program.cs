using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 双向链表
{
    interface MyList<T>
    {
        int GetLength();                            //获取链表长度
        void Clear();                               //清空链表				
        bool IsEmpty();                             //判断链表是否为空
        void Add(T item);                           //在链表尾部添加新节点
        void AddPre(T item, int index);             //在指定节点前添加新节点
        void AddPost(T item, int index);                //在指定节点后添加新节点
        T Delete(int index);                        //按索引删除节点
        T Delete(T item, bool isSecond = true);     //按内容删除节点，如果有多个内容相同点，则删除第一个
        T this[int index] { get; }                  //实现下标访问
        T GetElem(int index);                       //根据索引返回元素
        int GetPos(T item);                         //根据元素返回索引地址
        void Print();                               //打印
    }

    /// <summary>
	/// 单向链表节点
	/// </summary>
	/// <typeparam name="T"></typeparam>
	class Node<T>
    {
        private T data;                     //内容域
        private Node<T> next;               //下一节点

        public Node()
        {
            this.data = default(T);
            this.next = null;
        }

        public Node(T value)
        {
            this.data = value;
            this.next = null;
        }

        public Node(T value, Node<T> next)
        {
            this.data = value;
            this.next = next;
        }

        public T Data
        {
            get { return data; }
            set { data = value; }
        }

        public Node<T> Next
        {
            get { return next; }
            set { next = value; }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //LinkedList更适合大量的循环，并且循环时进行插入或者删除
            //ArrayList更适合大量的存取和删除操作。


            //双向链表(双链表)是链表的一种。和单链表一样，双链表也是由节点组成，它的每个数据结点中都有两个指针，
            //分别指向直接后继和直接前驱。
            LinkedList<string> link = new LinkedList<string>();
            LinkedListNode<string> node1 = new LinkedListNode<string>("node1");
            LinkedListNode<string> node2 = new LinkedListNode<string>("node2");
            LinkedListNode<string> node3 = new LinkedListNode<string>("node3");
            LinkedListNode<string> node4 = new LinkedListNode<string>("node4");
            link.AddFirst(node1);
            link.AddAfter(node1, node2);
            link.AddAfter(node2, node3);
            link.AddAfter(node3, node4);
            Console.WriteLine("元素数量：" + link.Count);
            Console.WriteLine("遍历元素列表");
            foreach (var item in link)
            {

                Console.WriteLine(item);
            }
            Console.WriteLine("通过While读取数据");
            LinkedListNode<string> current = link.First;
            while (current != null)
            {
                Console.WriteLine(current.Value);
                current = current.Next;
            }
            Console.WriteLine("链表是否包含元素：node1:True or False  :" + link.Contains("node1"));
            LinkedListNode<string> itemlink = link.Find("node2");
            Console.WriteLine("输出找到的元素：" + itemlink.Value);
            Console.WriteLine("找到元素的上个节点：" + itemlink.Previous.Value);
            Console.WriteLine("找到元素的下个节点：" + itemlink.Next.Value);

            Console.WriteLine("当前链表最后一个节点：" + link.Last.Value);
            Console.WriteLine("移除第一个节点后遍历元素");
            link.RemoveFirst();
            foreach (var item in link)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("移除node3后，遍历元素");
            link.Remove("node3");//严格区分大小写
            foreach (var item in link)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }
    }
}
