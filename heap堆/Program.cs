using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace heap堆
{
    public struct SomeValue
    {
        public int NumberA { get; set; }
    }
    public class SomeClass
    {
        public int NumberA { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //1.栈：当程序进入一个方法时，会为这个方法单独分配一块私属存储空间，
            //用于存储这个方法内部的局部变量，当这个方法结束时，分配给这个方法的栈会被释放，这个栈中的变量也将随之释放。
            //存放基本类型 的变量数据和对象的引用，
            //但对象本身不存放在栈中，而是存放在堆（new出来的对象）或者常量池中（字符串常量对象存放的常量池中）
            //（方法中的局部变量使用final修饰后，放在堆中，而不是栈中）】

            //https://www.cnblogs.com/wjk921/p/4771602.html
            //值传递：传的是对象的值。
            //引用传递：传的是栈中对象的地址。


            int i = 110;//定义整数类型变量I的时候，这个变量占用的内存是内存栈中分配的
            object obj = i;//装箱操作将变量 110存放到了内存堆中,而定义object对象类型的变量obj则在内存栈中，
            //并指向int类型的数值110，而该数值是付给变量i的数值副本
            i = 220;
            Console.WriteLine("i={0},obj={1}", i, obj);
            obj = 330;
            Console.WriteLine("i={0},obj={1}", i, obj);
            int j = (int)obj;
            j = 440;
            Console.WriteLine("j={0},obj={1}", i, obj);

            //https://blog.csdn.net/ljb81565248/article/details/52601533



            Console.ReadKey();
        }

        public static int AddFive(int pValue)
        {
            int result;
            result = pValue + 5;
            return result;
        }

    }
}
