using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAsync1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // My "library" method.
        private async Task<string> GetAsync(string url)
        {
            using (HttpClient hc = new HttpClient())
            {
                var jsonString = await hc.GetAsync(url).ConfigureAwait(false);
                //接下里的代码不用UI content thread去跑了
                //不过这样回浪费线程
                return jsonString.ToString();
            }
        }

        // My "top-level" method.
        private async void button1_Click(object sender, EventArgs e)
        {
            var jsonTask = GetAsync("https://www.baidu.com/");
            //System.Threading.Thread.Sleep(150);
            textBox1.Text = jsonTask.Result;
        }
    }
}
//The top-level method calls GetJsonAsync(within the UI/ASP.NET context).
//当GetStringAsync方法执行完毕返回后，GetJsonAsync会继续用执行top-level method的线程来执行await关键字之后的代码
/*
 * 上面内容的大致意思就是说在使用await and async模式时，
 * await关键字这一行后面的代码块会被一个context（
 * 也就是上面提到的ASP.NET request contex和UI context）线程继续执行，
 * 如果我们将本例中调用top-level method的线程称为线程A（即context线程），
 * 由于GetJsonAsync方法也是由线程A调用的，所以当GetJsonAsync方法中await的GetStringAsync
 * 方法执行完毕后，GetJsonAsync需要重新使用线程A执行await代码行之后的代码，
 * 而现在由于线程A在top-level method的代码中因为访问了jsonTask.Result被阻塞了
 * （因为线程A调用top-level method代码中jsonTask.Result的时候，await的GetStringAsync的Task
 * 还没执行完毕，所以被线程A阻塞），所以GetJsonAsync无法重新使用线程A执行await代码行之后的代码块
 * ，也被阻塞，所以形成了死锁。也就是说top-level method代码中线程A因为等待GetJsonAsync
 * 中await的GetStringAsync结束被阻塞，而GetStringAsync也等待线程A在top-level method的阻塞结束获得
 * 线程A来执行GetJsonAsync中await代码行后面的代码也被阻塞，两个阻塞相互等待，相互死锁。
 * 
 * 什么情况下会产生死锁？
调用 Task.Wait() 或者 Task.Result 立刻产生死锁的充分条件： 
1. 调用 Wait() 或 Result 的代码位于 UI 线程； 
1. Task 的实际执行在其他线程。
 */
