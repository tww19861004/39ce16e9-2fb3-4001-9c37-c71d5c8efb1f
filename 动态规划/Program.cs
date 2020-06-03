using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 动态规划
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("请输入:");
            while (true)
            {
                string str = Console.ReadLine();// 阻塞处  
                Console.WriteLine($"{str}{(IsHuiWen1(str) ? "是回文" : "不是回文")}");                
            }
            Console.ReadKey();
        }

        /// <summary>
        /// 判断一个字符串是否是回文
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static bool IsHuiWen(string str)
        {
            if(string.IsNullOrEmpty(str))
            {
                return false;
            }
            int count = str.Length / 2;            
            for(int i=0;i< count; i++)
            {
                if(str[i] != str[str.Length-1-i])
                {
                    return false;
                }
            }
            return true;
        }


        /// <summary>
        /// 判断回文 用堆栈
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static bool IsHuiWen1(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            Stack<char> stack = new Stack<char>();
            Queue<char> queue = new Queue<char>();

            for (int i = 0; i < str.Length; i++)
            {
                stack.Push(str[i]);
                queue.Enqueue(str[i]);
            }
            
            while (stack.Count > 0)
            {
                if (stack.Pop() != queue.Dequeue())   //只要发现有一个不等，就把isHui设置为假
                {
                    return false;
                }
            }

            return true;
        }
    }
}
