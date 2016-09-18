using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace CapacityManagerMain.Sqlite
{
    class SqliteConnector
    {
        String sConnectionPath = @"Data Source=C:\Temp\mydb.db";

        public void sqlConnect()
        {
            SQLiteConnection conn = new SQLiteConnection(sConnectionPath);
            SQLiteConnection.CreateFile("cmdb");
        }


        public DataSet Select_Adapter()
        {
            DataSet ds = new DataSet();

            //SQLiteDataAdapter 클래스를 이용 비연결 모드로 데이타 읽기
            string sql = "SELECT * FROM member";
            var adpt = new SQLiteDataAdapter(sql, sConnectionPath);
            adpt.Fill(ds);
            return ds;
        }
    }
}
