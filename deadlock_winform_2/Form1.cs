using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace deadlock_winform_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //https://www.cnblogs.com/OpenCoder/p/4434574.html
        

        // My "top-level" method.
        private void button1_Click(object sender, EventArgs e)
        {
            var jsonTask = GetJsonAsync("https://www.cnblogs.com/OpenCoder/p/4434574.html");
            textBox1.Text = jsonTask.Result.ToString();
        }

        // My "library" method.
        public static async Task<int> GetJsonAsync(string uri)
        {
            using (var client = new HttpClient())
            {
                var jsonString = await client.GetStringAsync(uri);
                return jsonString.Length;
            }
        }

        //1.The top-level method calls GetJsonAsync (within the UI/ASP.NET context).
        //2.1 GetJsonAsync starts the REST request by calling HttpClient.GetStringAsync 
        //2.2 (still within the context，这里的within the context表示的是GetJsonAsync方法依然用的是执行top-level method的线程来执行，也就是主线程).
        //3 GetStringAsync returns an uncompleted Task, indicating the REST request is not complete.
        //4.1 GetJsonAsync awaits the Task returned by GetStringAsync. 
        //4.2 The context（这里的context依然指的是执行top-level method的线程，当GetStringAsync方法执行完毕返回后，GetJsonAsync会继续用执行top-level method的线程来执行await关键字之后的代码，
        //4.3 这也是造成本例中代码会死锁的原因） is captured and will be used to continue running the GetJsonAsync method later. 
        //4.4 GetJsonAsync returns an uncompleted Task, indicating that the GetJsonAsync method is not complete.
        //5 The top-level method synchronously blocks on the Task returned by GetJsonAsync. This blocks the context thread.
        //6 … Eventually, the REST request will complete. This completes the Task that was returned by GetStringAsync.
        //7. The continuation for GetJsonAsync is now ready to run, and it waits for the context to be available so it can execute in the context.

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
    }
}
