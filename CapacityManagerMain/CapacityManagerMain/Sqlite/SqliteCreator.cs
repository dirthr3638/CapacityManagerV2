using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace CapacityManagerMain.Sqlite
{
    class SqliteCreator
    {
        public void createSqlFile(bool force = false)
        {
            //파일이 존재하지 않거나 강제플래그가 TRUE이면 파일을만듬
            if (!File.Exists(SqliteQueryCreater.SqlDbPath) || force) { 
                SQLiteConnection.CreateFile(SqliteQueryCreater.SqlDbPath);
            }
        }

        public void createScheme()
        {
            String ConnectionInfo = "Data Source=" + SqliteQueryCreater.SqlDbPath + ";Version=3;";

            SQLiteConnection m_dbConnection = new SQLiteConnection(ConnectionInfo);
            m_dbConnection.Open();

            //드라이브테이블 생성
            string sql = SqliteQueryCreater.createDrive();

            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            //폴더테이블 생성
            sql = SqliteQueryCreater.createFolder();

            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            //파일테이블 생성
            sql = SqliteQueryCreater.createFile();

            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            
            m_dbConnection.Close();
        }


    }
}
