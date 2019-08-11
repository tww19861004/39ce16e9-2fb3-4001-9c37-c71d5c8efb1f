using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAccess2
{
    class Program
    {
        static string connectPool = "XXXXXXXXXXXXXXXXXXXXXXXXXXX";

        static string connectWithoutPool = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";

        static void Main(string[] args)
        {
            using (SqlConnection cnNorthwind = new SqlConnection(connectPool))
            {
                SqlCommand cmdProducts = null;
                SqlTransaction tranDebug = null;
                try
                {
                    cnNorthwind.Open();
                    string clientid = cnNorthwind.ClientConnectionId.ToString();
                    tranDebug = cnNorthwind.BeginTransaction();
                    cmdProducts = new SqlCommand() { Connection = cnNorthwind, Transaction = tranDebug };

                    //当查询条件为非主键字段时，为什么在SQLServer的更新锁(UPDLOCK)会整表锁定？
                    cmdProducts.CommandText = "Update dbo.order1detail set ODDistri1butorName = '12345' where odorderno = 'XXXXXXXXXXXXXXXXXX'";
                    //这个不会锁全表
                    //cmdProducts.CommandText = "Update dbo.orderdetail set ODDistributorName = '12345' where id = 11";
                    int i =cmdProducts.ExecuteNonQuery();

                    //当查询条件为非主键字段时，为什么在SQLServer的更新锁(UPDLOCK)会整表锁定？
                    cmdProducts.CommandText = "Update dbo.order1s set OrderCreati1onDate = getdate() where orderno = 'XXXXXXXXXXXXXXXXXXXXX'";
                    //这个不会锁全表
                    cmdProducts.CommandText = "Update dbo.order1s set OrderCreat1ionDate = getdate() where id = 12";
                    int j = cmdProducts.ExecuteNonQuery();
                    
                    //下面行下断点，发现仍无法读取数据，因为前面的Update已经锁定记录
                    //throw new Exception("Rollback()");
                    //tranDebug.Commit();
                }
                catch (SqlException ex)
                {
                    tranDebug.Rollback();
                    Console.Write(ex.Message);
                    //throw;
                }
                finally
                {
                    UpdatePool();//执行超时因为锁住了
                    //UpdateWithOutPool();//执行超时因为锁住了
                    //不会锁表了 但是数据没有commit还是脏数据
                    //cnNorthwind.Dispose();
                }
            }
            //执行超时不锁住了 因为 connection disponse了
            UpdatePool();
        }

        private static void UpdatePool()
        {
            using (SqlConnection cnNorthwind = new SqlConnection(connectPool))
            {
                SqlCommand cmdProducts = null;
                try
                {
                    //https://blog.csdn.net/weixin_34075551/article/details/85497553
                    cnNorthwind.Open();
                    string clientid = cnNorthwind.ClientConnectionId.ToString();

                    cmdProducts = new SqlCommand("select * from dbo.order1detail where odorderno = 'XXXXX'", cnNorthwind);
                    cmdProducts.ExecuteNonQuery();

                    cmdProducts = new SqlCommand("select * from dbo.order1s where orderno = 'XXXXXX'", cnNorthwind);
                    cmdProducts.ExecuteNonQuery();                                        

                    //下面行下断点，在MSMS查看数据记录是否可修改，并尝试修改：发现能修改数据
                    cmdProducts.CommandText = "Update dbo.order1s set OrderCreationDate = getdate() where orderno = 'XXXXXXXXX'";
                    cmdProducts.ExecuteNonQuery();
                    //下面行下断点，因上面的Update导致数据被锁，无法在MSMS环境查看和修改数据
                    cmdProducts.CommandText = "select * from dbo.order1s where orderno = 'XXXXXXX'";
                    cmdProducts.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Console.Write(ex.Message);
                    //throw;
                }
                finally
                {
                    cnNorthwind.Dispose();
                }
            }
        }

        //ReadCommitted：默认项，读取时加共享锁，避免脏读，数据在事务完成前可修改，可被外部读取
        //ReadUncommitted：可脏读，不发布共享锁，也无独占锁


        private static void Trans2()
        {
            using (TransactionScope tsCope = new TransactionScope())
            {
                using (var con = new SqlConnection(connectPool))
                {
                    SqlCommand cmd = new SqlCommand("insert into fenye(value) values('zz')", con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                using (var con = new SqlConnection(connectPool))
                {
                    SqlCommand cmd = new SqlCommand("insert into fenye2(value) values('zz2')", con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                tsCope.Complete();
            }
        }
    }
}