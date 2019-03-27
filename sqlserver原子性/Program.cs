using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqlserver原子性
{
    class Program
    {
        private static int getorderhotelid()
        {
            string conString = "xxxxxxxxxxxxx";

            SqlConnection myConnection = new SqlConnection(conString);
            myConnection.Open();            

            //为事务创建一个命令
            SqlCommand myCommand = new SqlCommand();
            myCommand.Connection = myConnection;

            try
            {
                myCommand.CommandText = "select orderhotelid from dbo.table (nolock) where orderno = 'xxxxx'";
                //DataSet dataset = new DataSet();      //创建一个DataSet集,用于存放查询到的数据
                //SqlDataAdapter adapter = new SqlDataAdapter(myCommand);  //指定要执行的语句
                //adapter.Fill(dataset);           //将查询到的数据填充到dataset中
                //DataTable table = dataset.Tables[0]; //查询第一张表                
                return Convert.ToInt32(myCommand.ExecuteScalar());
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                myConnection.Close();
            }
        }
        private static void updateorderstatus()
        {
            string conString = "xxxxxxxx";

            SqlConnection myConnection = new SqlConnection(conString);
            myConnection.Open();

            //为事务创建一个命令
            SqlCommand myCommand = new SqlCommand();
            myCommand.Connection = myConnection;

            try
            {
                myCommand.CommandText = "update dbo.table set orderhotelid = orderhotelid+1 where orderno = 'xxxxxxxx'";
                myCommand.ExecuteNonQuery();
                Console.WriteLine("数据更新成功");
            }
            catch (Exception ex)
            {               
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                myConnection.Close();
            }
        }
        static void Main(string[] args)
        {            
            Console.WriteLine($"并发更新前，orderhotelid={getorderhotelid()}");
            Task task1 = Task.Run(() =>
            {
                updateorderstatus();
            });
            Task task2 = Task.Run(() =>
            {
                updateorderstatus();
            });
            Task task3 = Task.Run(() =>
            {
                updateorderstatus();
            });
            Task task4 = Task.Run(() =>
            {
                updateorderstatus();
            });
            Task.WaitAll(task1, task2,task3,task4);
            Console.WriteLine($"并发更新后，orderhotelid={getorderhotelid()}");
            Console.ReadKey();

        }
    }
}
