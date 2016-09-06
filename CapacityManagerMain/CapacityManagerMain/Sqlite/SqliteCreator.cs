using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace CapacityManagerMain.Sqlite
{
    class SqliteCreator
    {
        public void createSqlFile()
        {
            SQLiteConnection.CreateFile(SqliteQueryCreater.SqlDbPath);
        }

        public void createScheme()
        {
            String ConnectionInfo = "Data Source=" + SqliteQueryCreater.SqlDbPath + ";Version=3;";

            SQLiteConnection m_dbConnection = new SQLiteConnection(ConnectionInfo);
            m_dbConnection.Open();

            //드라이브테이블 생성
            string sql = 
                @"CREATE TABLE `DRIVE_INFO` (	
                    `drive_code`    INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,	
                    `drive_name`	TEXT NOT NULL,	
                    `drive_type`	TEXT NOT NULL,	
                    `drive_hash`	INTEGER NOT NULL,	
                    `use_yn`	INTEGER NOT NULL DEFAULT 1 )";

            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            //폴더테이블 생성
            sql = @"CREATE TABLE `FOLDER_INFO` (	
                    `folder_code`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,		
                    `parent_fd_cd`	INTEGER,	
                    `drive_code`	INTEGER NOT NULL,	
                    `folder_name`	TEXT NOT NULL,
                    `last_write_time`	NUMERIC NOT NULL,	
                    `create_time`	NUMERIC NOT NULL,	
                FOREIGN KEY(`parent_fd_cd`) REFERENCES `FOLDER_INFO`(`folder_code`),	
                FOREIGN KEY(`drive_code`) REFERENCES `DRIVE_INFO`(`drive_code`)) ";

            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            //파일테이블 생성
            sql = @"CREATE TABLE `FILE_INFO` (	
                    `file_code`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,	
                    `folder_code`	INTEGER NOT NULL,	
                    `file_name`	TEXT NOT NULL,	
                    `file_ext`	TEXT NOT NULL,	
                    `file_path`	TEXT NOT NULL,	
                    `file_volume`	NUMERIC NOT NULL,	
                    `last_write_time`	NUMERIC NOT NULL,	
                    `create_time`	NUMERIC NOT NULL,	
                FOREIGN KEY(`folder_code`) REFERENCES `FOLDER_INFO`(`folder_code`))";

            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            
            m_dbConnection.Close();
        }


    }
}
