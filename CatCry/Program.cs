using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatCry
{
    //C#中的，订阅发布
    ///<summary>
    /// 猫叫事件参数
    ///</summary>
    public class CatCryEventArgs : EventArgs
    {
        //发出叫声的猫的名字
        private string _catname;
        ///<summary>
        /// 构造函数
        ///</summary>
        public CatCryEventArgs(string catname) : base()
        {
            _catname = catname;
        }

        ///<summary>
        /// 输出参数内容
        ///</summary>
        public override string ToString()
        {
            return "猫 " + _catname + " 叫了";
        }
    }
    //猫叫，主人醒，老鼠跑（事件的处理）
    public class Cat
    {
        //猫类：定义一个猫叫事件
        //猫名
        private string _name;
        ///<summary>
        /// 构造函数
        ///</summary>
        ///<param name="name">名字参数</param>
        public Cat(string name)
        {
            _name = name;
        }
        //猫叫事件
        public event EventHandler<CatCryEventArgs> CatCryEvent;
        public event Action<object, CatCryEventArgs> CatCryEvent1;
        ///<summary>
        /// 定义一个猫叫的方法
        ///</summary>
        public void CatCry()
        {
            Console.WriteLine("小猫叫了一声....");
            //如果这个事件不是空的（就意味着猫叫的事件是被订阅的）
            if (CatCryEvent!=null)
            {
                CatCryEventArgs args = new CatCryEventArgs(_name);
                Console.WriteLine(args);
                CatCryEvent(this, args);
            }            
        }
    }
    public class Mouse
    {
        //老鼠类：订阅猫叫事件，在猫发出叫声这个事件后，老鼠逃跑；
        public void Run<CatCryEventArgs>(object sender, CatCryEventArgs e)
        {
            Console.WriteLine($"{e.ToString()}，就开始逃窜");
        }
    }
    public class People
    {
        //类似于老鼠类，在猫发出叫声这个事件后，主人醒来
        /// <summary>
        /// 主人起床的方法
        /// </summary>
        public void WakeUp<CatCryEventArgs>(object sender, CatCryEventArgs e)
        {
            Console.WriteLine($"{e.ToString()},主人被吵醒了");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Cat c = new Cat("唐二"); //实例化一个猫类
            Mouse h = new Mouse(); //实例化一个老鼠类
            People p = new People(); //实例化一个主人 类
            //c.CatCryEvent += new EventHandler<CatCryEventArgs>(h.r);
        }
    }
}
