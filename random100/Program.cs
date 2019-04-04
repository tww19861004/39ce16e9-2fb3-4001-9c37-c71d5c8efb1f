using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace random100
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] res = Shuffle();
            foreach(var item in res)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }

        private static int[] GenerateRandomNumber1()
        {
            int[] result = new int[100];
            List<int> arrayList = new List<int>();
            Random random = new Random();
            for (int i = 0; i < 100; i++)
            {
                arrayList.Add(i + 1);
            }
            int k = 0;
            while (arrayList.Count > 0)
            {
                int i = random.Next(0, arrayList.Count);
                result[k] = arrayList[i];
                k++;
                arrayList.RemoveAt(i);
            }
            return result;
        }

        private static int[] GenerateRandomNumber2()
        {
            int[] arr = new int[100];
            for (int i = 0; i < arr.Length; i++) arr[i] = i + 1;
            Random rnd = new Random();
            Array.Sort(arr, delegate (int a, int b) { return rnd.Next(); });
            return arr;
        }

        //想法就是，先把数组初始化为1到100,然后每次随机两个下标出来，互换它的值

        public static int[] GenerateRandomNumber3()
        {
            //用于存放1到100这100个数
            int[] container = new int[100];
            for (int i = 0; i < container.Length; i++) container[i] = i + 1;
            //用于保存返回结果            
            Random random = new Random();
            int index = 0;            
            for (int i = 0; i < 100; i++)
            {
                //随机一个索引
                index = random.Next(0, container.Length -1 -i);
                //获取这个值和最后一个交换
                int temp = container[index];
                container[index] = container[container.Length - 1 - i];
                container[container.Length - 1 - i] = temp;
            }            
            return container;
        }

        public static int[] Sort(int[] GenerateRandomNumber)
        {
            //用于保存返回结果            

            for (int i = 0; i < GenerateRandomNumber.Length - 1; i++)
            {
                //在 i-(nums.Length-1) 范围内，将该范围内最小的数字提到i
                for (int j = i + 1; j < GenerateRandomNumber.Length; j++)
                {
                    if (GenerateRandomNumber[i] > GenerateRandomNumber[j])
                    {
                        //交换
                        int temp = GenerateRandomNumber[i];
                        GenerateRandomNumber[i] = GenerateRandomNumber[j];
                        GenerateRandomNumber[j] = temp;
                    }
                }
            }
            return GenerateRandomNumber;
        }

        public static string[] Shuffle()
        {
            string[] result = new string[52];
            string[] cardType = { "红桃","黑桃","方块","梅花" };
            string[] cardValue = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
            for(int i=0;i<cardType.Length;i++)
            {
                for(int j=0;j<cardValue.Length;j++)
                {
                    result[i* cardValue.Length + j] = $"{cardType[i]}{cardValue[j]}";
                }
            }
            //用于保存返回结果            
            Random random = new Random();
            int index = 0;
            for (int i = 0; i < result.Length; i++)
            {
                //随机一个索引
                index = random.Next(0, result.Length - 1 - i);
                //获取这个值和最后一个交换
                string temp = result[index];
                result[index] = result[result.Length - 1 - i];
                result[result.Length - 1 - i] = temp;
            }
            return result;
        }
    }
}
