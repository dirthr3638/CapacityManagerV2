using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapacityManagerMain.Model;
using System.Data.SQLite;

namespace CapacityManagerMain.Sqlite
{
    class SqliteExcuteQuery
    {
        //return value : drive index
        public int InsertDriveTable(DriveModel drive)
        {
            int lastId = -1;
            String ConnectionInfo = "Data Source=" + SqliteQueryCreater.SqlDbPath + ";Version=3;";

            using (SQLiteConnection conn = new SQLiteConnection(ConnectionInfo))
            {
                conn.Open();
                string sql = SqliteQueryCreater.insertDrive(drive);
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.ExecuteNonQuery();

                sql = SqliteQueryCreater.lastIndex();
                cmd = new SQLiteCommand(sql, conn);
                long lastId123 = (long)cmd.ExecuteScalar();

                conn.Close();
            }

            return lastId;
        }

        //return value : drive index
        public int InsertDriveTable(DriveModel drive, SQLiteConnection conn)
        {
            string sql = SqliteQueryCreater.insertDrive(drive);
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            cmd.ExecuteNonQuery();
            return 1;
        }

        public int InsertFolderTable(FolderInfoModel folder)
        {
            using (SQLiteConnection conn = new SQLiteConnection(SqliteQueryCreater.SqlDbPath))
            {
                conn.Open();
                string sql = SqliteQueryCreater.insertFolder(folder);
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            return 1;
        }

        public int InsertFileTable(FIleInfoModel file)
        {
            using (SQLiteConnection conn = new SQLiteConnection(SqliteQueryCreater.SqlDbPath))
            {
                conn.Open();
                string sql = SqliteQueryCreater.insertFile(file);
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            return 1;
        }

        public int InsertFileListTableTx(List<FIleInfoModel> files)
        {
            foreach(FIleInfoModel file in files){

            }
            return 1;
        }


    }
}
