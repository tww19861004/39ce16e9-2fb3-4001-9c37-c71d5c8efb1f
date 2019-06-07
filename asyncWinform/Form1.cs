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

namespace asyncWinform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Show("button1_Click Before");
            Test();
            Show("button1_Click End");
        }

        private async void Test()
        {

            Show("Test.Before");
            await Task.Run(() => { Thread.Sleep(2000); }).ConfigureAwait(true);
            Show("Test.After");//输出字符串和当前线程

        }

        public void Show(string str)
        {
            try
            {
                richTextBox1.AppendText($"{str}：{Thread.CurrentThread.ManagedThreadId}\r\n");
            }
            catch(Exception ex)
            {
                richTextBox1.AppendText($"{ex.Message}\r\n");
            }            
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            //线程间操作无效: 从不是创建控件“richTextBox1”的线程访问它。”
            Show("button2_Click.Before");
            await Task.Run(() => { Thread.Sleep(2000); }).ConfigureAwait(false);
            Show("button2_Click.After");//输出字符串和当前线程
        }
    }
}
