using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using IsolationLevel = System.Data.IsolationLevel;

namespace DataAccess2
{
    //https://www.cnblogs.com/CreateMyself/p/6362904.html 这篇文章讲的不错
    //从数据库系统的角度来看：分为独占锁（即排它锁），共享锁和更新锁
    class Program
    {
        //共享锁（S）：还可以叫他读锁。可以并发读取数据，但不能修改数据。也就是说当数据资源上存在共享锁的时候，所有的事务都不能对这个资源进行修改，直到数据读取完成，共享锁释放。
        //排它锁（X）：还可以叫他独占锁、写锁。就是如果你对数据资源进行增删改操作时，不允许其它任何事务操作这块资源，直到排它锁被释放，防止同时对同一资源进行多重操作。
        //NOLOCK
        //不要发出共享锁，并且不要提供排它锁。当此选项生效时，可能会读取未提交的事务或一组在读取中间回滚的页面。有可能发生脏读。仅应用于 SELECT 语句。 
        static string connectPool = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";

        static string connectWithoutPool = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";

        //如果没有目录，查汉语字典就要一页页的翻，而有了目录只要查询目录即可。为了提高检索的速度，可以为经常进行检索的列添加索引，相当于创建目录。
        //使用索引能提高查询效率，但是索引也是占据空间的，而且添加、更新、删除数据的时候也需要同步更新索引，因此会降低Insert、Update、Delete的速度。只在经常检索的字段上(Where)创建索引。
        //（*）即使创建了索引，仍然有可能全表扫描，比如like、函数、类型转换等

        static void Main(string[] args)
        {
            //ExecuteSqlReadUnCommitted();
            ExecuteSqlReadCommitted();
        }

        private static void ExecuteSqlReadUnCommitted()
        {
            //获取订单的
            Console.WriteLine("订单xxxxxxxx的ordercreationdate=" + ExecuteSqlWithoutTransaction("select ordercreationdate from dbo.orders where orderno = 'xxxxxxxx'"));

            System.Threading.Thread.Sleep(2000);

            using (SqlConnection cnNorthwind = new SqlConnection(connectPool))
            {
                SqlCommand cmdProducts = null;
                SqlTransaction tranDebug = null;
                try
                {
                    cnNorthwind.Open();
                    string clientid = cnNorthwind.ClientConnectionId.ToString();
                    //READ UNCOMMITTED 更改的是 SELECT 加锁的行为, 即不让 SELECT 加锁
                    tranDebug = cnNorthwind.BeginTransaction(IsolationLevel.ReadUncommitted);
                    cmdProducts = new SqlCommand() { Connection = cnNorthwind, Transaction = tranDebug };

                    //只是锁行 请求orders的排他锁，成功                    
                    //cmdProducts.CommandText = "Update dbo.orders set ordercreationdate = getdate() where orderno = 'xxxxxxxx'";
                    //cmdProducts.ExecuteNonQuery();
                    // id有索引 只是锁行
                    cmdProducts.CommandText = "Update dbo.orders set ordercreationdate = getdate() where id = 40809 ";
                    cmdProducts.ExecuteNonQuery();

                    //没锁 返回成功
                    string s1 = ExecuteSqlWithoutTransaction("select ordercreationdate from dbo.orders (Nolock) where orderno = 'xxxxxxxx'");
                    //行锁 返回失败
                    string s2 = ExecuteSqlWithoutTransaction("SET TRAN ISOLATION LEVEL READ UNCOMMITTED select ordercreationdate from dbo.orders where orderno = 'xxxxxxxx'");                    
                    //行锁 返回成功
                    string s3 = ExecuteSqlWithoutTransaction("select ordercreationdate from dbo.orders where id = 56");
                    //行锁 返回失败
                    string s4 = ExecuteSqlWithoutTransaction("select ordercreationdate from dbo.orders where orderno = 'xxxxxxxxxxx'");
                    //行锁 返回成功
                    string s5 = ExecuteSqlWithoutTransaction("SET TRAN ISOLATION LEVEL READ UNCOMMITTED select ordercreationdate from dbo.orders where orderno = 'xxxxxxxxxxx'");

                    //SET TRAN ISOLATION LEVEL READ UNCOMMITTED
                    //select ordercreationdate from dbo.orders where orderno = 'xxxxxxxxxxx'

                    //SET TRAN ISOLATION LEVEL READ COMMITTED
                    //select ordercreationdate from dbo.orders where orderno = 'xxxxxxxxxxx'

                    string s6 = "";
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
                    //UpdateWithOutPool();//执行超时因为锁住了
                    //不会锁表了 但是数据没有commit还是脏数据
                    cnNorthwind.Dispose();
                }
            }
        }

        private static void ExecuteSqlReadCommitted()
        {
            //获取订单的
            Console.WriteLine("订单xxxxxxxx的ordercreationdate=" + ExecuteSqlWithoutTransaction("select ordercreationdate from dbo.orders where orderno = 'xxxxxxxx'"));
            //string s31 = ExecuteSqlWithoutTransaction("drop index IX_OrderNo");
            //string s41 = ExecuteSqlWithoutTransaction("CREATE NONCLUSTERED INDEX [IX_OrderNo]ON [dbo].[Orders] ([OrderNo]) WITH (PAD_INDEX=OFF,STATISTICS_NORECOMPUTE=OFF,IGNORE_DUP_KEY=OFF,ALLOW_ROW_LOCKS=ON,ALLOW_PAGE_LOCKS=ON) ON [PRIMARY]");
            System.Threading.Thread.Sleep(2000);

            using (SqlConnection cnNorthwind = new SqlConnection(connectPool))
            {
                SqlCommand cmdProducts = null;
                SqlTransaction tranDebug = null;
                try
                {
                    cnNorthwind.Open();
                    string clientid = cnNorthwind.ClientConnectionId.ToString();                    
                    tranDebug = cnNorthwind.BeginTransaction(IsolationLevel.ReadCommitted);
                    cmdProducts = new SqlCommand() { Connection = cnNorthwind, Transaction = tranDebug };

                    //只是锁行 请求orders的排他锁，成功
                    cmdProducts.CommandText = "Update dbo.orders set ordercreationdate = getdate() where orderno = 'xxxxxxxx'";
                    cmdProducts.ExecuteNonQuery();
                    // id有索引 只是锁行
                    //cmdProducts.CommandText = "Update dbo.orders set ordercreationdate = getdate() where id = 40809 ";
                    //cmdProducts.ExecuteNonQuery();
                    
                    //没锁 返回成功
                    string s1 = ExecuteSqlWithoutTransaction("select ordercreationdate from dbo.orders (Nolock) where orderno = 'xxxxxxxx'");
                    //行锁 返回失败
                    string s2 = ExecuteSqlWithoutTransaction("SET TRAN ISOLATION LEVEL READ UNCOMMITTED select ordercreationdate from dbo.orders where orderno = 'xxxxxxxx'");
                    //行锁 返回成功
                    string s3 = ExecuteSqlWithoutTransaction("select ordercreationdate from dbo.orders where id = 56");
                    
                    //行锁 返回失败 orderno有非聚集索引 成功 反之失败
                    string s4 = ExecuteSqlWithoutTransaction("select ordercreationdate from dbo.orders where orderno = 'xxxxxxxxxxx'");                    
                    //行锁 返回成功
                    string s5 = ExecuteSqlWithoutTransaction("SET TRAN ISOLATION LEVEL READ UNCOMMITTED select ordercreationdate from dbo.orders where orderno = 'xxxxxxxxxxx'");

                    string s6 = "";
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
                    //UpdateWithOutPool();//执行超时因为锁住了
                    //不会锁表了 但是数据没有commit还是脏数据
                    cnNorthwind.Dispose();
                }
            }
        }

        private static string ExecuteSqlWithoutTransaction(string selectSql)
        {
            string result = string.Empty;
            using (SqlConnection cnNorthwind = new SqlConnection(connectPool))
            {
                SqlCommand cmdProducts = null;
                try
                {
                    //https://blog.csdn.net/weixin_34075551/article/details/85497553
                    cnNorthwind.Open();
                    string clientid = cnNorthwind.ClientConnectionId.ToString();

                    //这个不会锁
                    cmdProducts = new SqlCommand(selectSql, cnNorthwind);
                    cmdProducts.CommandTimeout = 3;
                    if(selectSql.Contains("select"))
                    {
                        result = cmdProducts.ExecuteScalar().ToString();
                    }
                    else
                    {
                        int j1 = cmdProducts.ExecuteNonQuery();
                        if(j1>0)
                        {
                            result = "执行成功";
                        }
                        else
                        {
                            result = "执行失败";
                        }
                    }
                }
                catch (SqlException ex)
                {
                    result = ex.Message;
                    Console.Write(ex.Message);
                    //throw;
                }
                finally
                {
                    cnNorthwind.Dispose();                    
                }
            }
            return result;
        }

        //ReadCommitted：默认项，读取时加共享锁，避免脏读，数据在事务完成前可修改，可被外部读取
        //ReadUncommitted：可脏读，不发布共享锁，也无独占锁
    }
}