using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace taskList1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> lst = new List<int>() { 1,3,4,6,7};
            foreach(var item in lst)
            {
                Task task = new Task(n => test((int)n), item);
                task.Start();
                task.ContinueWith(n =>
                {                    
                    System.IO.File.AppendAllText(@"D:\Project\1.txt", n.Exception?.InnerException?.Message+"\r\n");
                    //n.Dispose();
                }, TaskContinuationOptions.OnlyOnFaulted);
                System.Threading.Thread.Sleep(500);
            }

            //if (listTask.IsNotEmpty())
            //{
            //    try
            //    {
            //        Task.WaitAll(listTask.ToArray());
            //    }
            //    catch { }

            //    foreach (var item in listTask)
            //    {
            //        if (item.IsFaulted)//线程报错了
            //        {
            //            EmailSendHelper.AsyncSendMail(AppSettings.OrderExceptionEmailReceiver, "推送新订单到财务出现失败", $"Message:{item.Exception?.InnerException?.Message},StackTrace:{item.Exception?.InnerException?.StackTrace}");

            //            TianWangLogHelper.SaveTWErrorLog(new TianWangLogTypeModel()
            //            {
            //                module = "",
            //                category = ""
            //            }, "pushordertofinance", $"Message:{item.Exception?.InnerException?.Message},StackTrace:{item.Exception?.InnerException?.StackTrace}");
            //        }
            //    }
            //}

            Console.ReadKey();
        }

        static void test(int obj)
        {
            if(obj == 4)
            {
                throw new Exception("模拟异常");
            }
            else
            {
                System.IO.File.AppendAllText(@"D:\Project\1.txt", obj.ToString()+"\r\n");
            }
        }
    }
}
