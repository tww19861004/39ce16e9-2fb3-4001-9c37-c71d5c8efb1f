using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace excel2
{
    class Program
    {
        static void Main(string[] args)
        {
            string Path = @"D:\MyConfiguration\tww24098\Downloads\20190313133224.xls";
            DataSet schemaTable = ExcelToDS(Path);
            string str = schemaTable.Tables[0].Rows[0]["OrderMemberMobile"].ToString().Trim();

            Console.WriteLine($"{Encoding.UTF8.GetByteCount(str)},{str.Length},{ReturnCleanASCII(str).Length}");

            Console.ReadKey();

        }

        public static string ReturnCleanASCII(string s)
        {
            StringBuilder sb = new StringBuilder(s.Length);
            foreach (char c in s)
            {
                if ((int)c > 127) // you probably don't want 127 either
                    continue;
                if ((int)c < 32)  // I bet you don't want control characters 
                    continue;
                if (c == ',')
                    continue;
                if (c == '"')
                    continue;
                sb.Append(c);
            }
            return sb.ToString();
        }

        public static DataSet ExcelToDS(string Path)
        {
            try
            {
                string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + Path + ";" + "Extended Properties=Excel 8.0;";
                OleDbConnection conn = new OleDbConnection(strConn);
                conn.Open();
                string strExcel = "";
                OleDbDataAdapter myCommand = null;
                DataSet ds = null;
                strExcel = "select * from [sheet0$]";
                myCommand = new OleDbDataAdapter(strExcel, strConn);
                ds = new DataSet();
                myCommand.Fill(ds, "table1");
                return ds;
            }
            catch(Exception ex)
            {
                return null;
            }
            
        }
    }
}
