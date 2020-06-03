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
                Console.WriteLine($"{str}{(IsHuiWen(str) ? "是回文" : "不是回文")}");                
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
    }
}
