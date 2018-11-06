using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace excel1
{
    class Program
    {
        static void Main(string[] args)
        {
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + "D:\\MyConfiguration\\tww24098\\Downloads\\tcdatacheck20181101112510.xls" + ";" + "Extended Properties=Excel 8.0;";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            string strExcel = "";
            OleDbDataAdapter myCommand = null;
            DataSet ds = null;
            strExcel = "select * from [sheet0$]";
            myCommand = new OleDbDataAdapter(strExcel, strConn);
            ds = new DataSet();
            myCommand.Fill(ds, "table1");

            string ordermembermobile = ds.Tables[0].Rows[0]["OrderMemberMobile"].ToString();
            string str = ordermembermobile.Trim();
        }
    }
}
