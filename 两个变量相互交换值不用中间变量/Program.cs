using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 两个变量相互交换值不用中间变量
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 1, 2, 3 };
            arr[1] = arr[1] + arr[2];
            arr[2] = arr[1] - arr[2];
            arr[1] = arr[1] - arr[2];
            for(int i=0;i<arr.Length;i++)
            {
                Console.WriteLine($"{arr[i]}\t");
            }

            ///<summary>
            /// 异或运算
            ///</summary>
            int x = 5;
            int y = 3;
            y ^= x;   //等价与y=y^x 而异或的算法就是，两个二进制数的每一位进行比较，如果相同则为0，不同则为1
            Console.WriteLine(y);        ////输出结果为6

            int[] arr1 = { 1, 2, 3 };
            arr1[1] = arr1[1] ^ arr1[2];
            arr1[2] = arr1[1] ^ arr1[2];
            arr1[1] = arr1[1] ^ arr1[2];
            for (int i = 0; i < arr1.Length; i++)
            {
                Console.WriteLine($"{arr1[i]}\t");
            }

            Console.ReadKey();
        }
    }
}
