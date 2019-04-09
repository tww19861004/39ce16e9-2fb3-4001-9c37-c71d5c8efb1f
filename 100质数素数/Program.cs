using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _100质数素数
{
    //质数又称素数，有无限个。质数定义为在大于1的自然数中，除了1和它本身以外不再有其他因数的数称为质数,质数不包括负数
    class Program
    {
        static void Main(string[] args)
        {
            Test(15);
            Console.ReadKey();
            return;
            //A、B、C、D、E五名学生有有可能参加计算机竞赛
            //00100,00101,10111....
            int a, b, c, d, e;
            for (a = 0; a < 2; a++)
                for (b = 0; b < 2; b++)
                    for (c = 0; c < 2; c++)
                        for (d = 0; d < 2; d++)
                            for (e = 0; e < 2; e++)
                            {
                                //Console.WriteLine($"{a}{b}{c}{d}{e}");
                                //A参加时，B也参加；
                                if (a == 1)
                                {
                                    b = 1;
                                }
                                //B和C只有一个人参加；
                                if (b == 1 ^ c == 1)
                                {
                                    //Ｃ和Ｄ或者都参加，或者都不参加；
                                    if (c == d)
                                    {
                                        //Ｄ和Ｅ中至少有一个人参加；
                                        if (d == 1 || e == 1)
                                        {
                                            //如果Ｅ参加，那么Ａ和Ｄ也都参加
                                            if (e == 1 && (a == 0 || d == 0))
                                            {
                                                continue;
                                            }
                                            Console.WriteLine($"{a}{b}{c}{d}{e}");
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                else
                                {
                                    continue;
                                }
                            }

            Console.ReadKey();
        }

        //判断一个数是否为质数
        private static bool IsZhiShu(int n)
        {
            if (n < 2)
                return false;
            for (int i = 2; i < n; i++)
            {
                //能被其他整数相除，不是质数
                if (n % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static int Foo(int i)
        {
            if (i <= 0)
                return 0;
            else if (i > 0 && i <= 2)
                return 1;
            else return Foo(i - 1) + Foo(i - 2);
        }

        private static string Reverse(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            string[] strArray = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int count = strArray.Length / 2;
            for (int i = 0; i < count; i++)
            {
                string temp = strArray[i];
                strArray[i] = strArray[strArray.Length - i - 1];
                strArray[strArray.Length - i - 1] = temp;
            }
            return String.Join(" ", strArray);
        }

        //输入一个正数s，打印出所有和为s的连续正数序列（至少含有两个数字）。例如输入15，由于1+2+3+4+5=4+5+6=7+8=15，
        //所以打印出3个连续序列1 ~5，4~6和7 ~8
        private static void Test(int k)
        {
            int temp = 0;
            for (int i = 1; i < k; i++)
            {
                temp = 0;
                for (int j = i; j < k; j++)
                {
                    temp += j;
                    if (temp == k)
                    {
                        for (int m = i; m <= j; m++)
                        {
                            Console.Write(m.ToString() + "+");
                        }
                        Console.WriteLine("=" + k.ToString());
                    }
                    else if (temp > k)
                    {
                        continue;
                    }
                }
            }
        }

        private static void findContinueSequence(int s)
        {
            Boolean flag = false;
            //因为连续正数序列至少包含两个数，所以s大于等于3
            if (s < 3)
            {
                return;
            }
            int start = 1;
            int end = 2;
            int mid = (1 + s) / 2;
            int sum = start + end;
            while (start < mid)
            {
                if (sum == s)
                {
                    printNum(start, end);
                } while (sum > s && start < mid)
                {
                    sum -= start;
                    start++;
                    if (sum == s)
                    {
                        printNum(start, end);
                    }
                }
                end++;
                sum += end;

            }
        }





    }
}
