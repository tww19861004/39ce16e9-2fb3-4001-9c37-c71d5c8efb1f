using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 主线程_前台线程_后台线程winform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //前台线程和后台线程唯一区别就是:应用程序必须运行完所有的前台线程才可以退出；
            //而对于后台线程，应用程序则可以不考虑其是否已经运行完毕而直接退出，
            //所有的后台线程在应用程序退出时都会自动结束。
            MessageBox.Show("点击按钮启动了一个前台线程。\r\n前台线程：“既然我上台了，我一定要表演完，不然我跟你没完！”");
            //前台线程
            Thread t1 = new Thread(Say);
            t1.IsBackground = false;
            t1.Name = "前台线程";
            t1.Start();
        }



        private void Say()
        {
            for (int i = 0; i < 10; i++)
            {
                MessageBox.Show("我演，我卖力的演！\n\r把主窗口关了，你看我还在不在？" + i.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("点击按钮启动了一个后台线程。\r\n后台线程：“既然我没上台，我受制于应用程序，它关了我就自动关了（舞台都关了，我在后台再干活就没意思了）！”");
            //后台线程
            Thread t2 = new Thread(Say);
            t2.IsBackground = true;
            t2.Name = "后台线程";
            t2.Start();
        }
    }
}
