using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 冒泡算法
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 17, 5, 19, 27, 8, 12, 1 };

            Console.WriteLine("最大的数往最右边排\t");
            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = 0; j < arr.Length - 1 - i; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }
            // 利用 for 循环将元素逐个显示出来
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(arr[i] + "\t");
            }
            Console.WriteLine("最小的数往最左边排\t");
            int[] arr1 = { 17, 5, 19, 27, 8, 12, 1 };
            for(int j=1;j<arr1.Length-1;j++)
            {
                for (int i = arr1.Length - 1; i >= j; i--)
                {
                    if (arr1[i] < arr1[i - 1])
                    {
                        int temp = arr1[i - 1];
                        arr1[i - 1] = arr1[i];
                        arr1[i] = temp;
                    }
                }
            }            
            // 利用 for 循环将元素逐个显示出来
            for (int i = 0; i < arr1.Length; i++)
            {
                Console.WriteLine(arr1[i] + "\t");
            }
            Console.ReadKey();
        }
    }
}
