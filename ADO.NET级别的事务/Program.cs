using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET级别的事务
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * 优势：

            l         简单。

            l         和数据库事务差不多快。

            l         事务可以跨越多个数据库访问。

            l         独立于数据库，不同数据库的专有代码被隐藏了。

            限制：事务执行在数据库连接层上，所以需要在执行事务的过程中手动地维护一个连接。

            注意：所有命令都必须关联在同一个连接实例上，ADO.NET事务处理不支持跨多个连接的事务处理。
            */
            string conString = "";

            SqlConnection myConnection = new SqlConnection(conString);

            myConnection.Open();

            //启动一个事务
            SqlTransaction myTrans = myConnection.BeginTransaction();

            //为事务创建一个命令
            SqlCommand myCommand = new SqlCommand();
            myCommand.Connection = myConnection;
            myCommand.Transaction = myTrans;

            try
            {
                myCommand.CommandText = "update dbo.table set orderstatusid = 2 where orderno = 'xxxxx'";
                myCommand.ExecuteNonQuery();
                myCommand.CommandText = "update dbo.table set ordernoofsupplier = '23235' where orderno = 'xxxxxxx'";
                myCommand.ExecuteNonQuery();
                myTrans.Commit();//提交
                Console.Write("两条数据更新成功");
            }
            catch (Exception ex)
            {
                myTrans.Rollback();//遇到错误，回滚
                Console.Write(ex.ToString());
            }
            finally
            {
                myConnection.Close();

            }
        }
    }
}
