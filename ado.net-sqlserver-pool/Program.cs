using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ado.net_sqlserver_pool
{
    class Program
    {
        static string connectPool = "xxxxxxxxxxxxx";

        static string connectWithoutPool = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";

        static void Main(string[] args)
        {
            for(int i=0;i<10;i++)
            {
                using (SqlConnection conn = new SqlConnection(connectWithoutPool))
                {
                    conn.Open();
                    Console.WriteLine(conn.ClientConnectionId.ToString());
                }
            }
            Console.ReadKey();
        }
    }
}
