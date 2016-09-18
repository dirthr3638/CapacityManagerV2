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
            long lastId = -1;
            String ConnectionInfo = "Data Source=" + SqliteQueryCreater.SqlDbPath + ";Version=3;";

            using (SQLiteConnection conn = new SQLiteConnection(ConnectionInfo))
            {
                conn.Open();
                string sql = SqliteQueryCreater.insertDrive(drive);
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.ExecuteNonQuery();

                sql = SqliteQueryCreater.lastIndex();
                cmd = new SQLiteCommand(sql, conn);
                lastId = (long)cmd.ExecuteScalar();

                conn.Close();
            }
            return unchecked((int)lastId);
        }

        //return value : drive index
        public int InsertDriveTable(DriveModel drive, SQLiteConnection conn)
        {
            string sql = SqliteQueryCreater.insertDrive(drive);
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            cmd.ExecuteNonQuery();
            return 1;
        }

        public List<DriveModel> SelectDriveModel()
        {
            String ConnectionInfo = "Data Source=" + SqliteQueryCreater.SqlDbPath + ";Version=3;";

            SQLiteConnection m_dbConnection = new SQLiteConnection(ConnectionInfo);
            m_dbConnection.Open();
            
            string sql = SqliteQueryCreater.selectDriveInfoList();
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            List<DriveModel> result = new List<DriveModel>();

            while (reader.Read()) {
                DriveModel driveModel = new DriveModel();
                Console.WriteLine("drive_code: " + reader["drive_code"] + "\tdrive_name: " + reader["drive_name"] + "\tdrive_type: " + reader["drive_type"]);
                result.Add(driveModel);
            }

            m_dbConnection.Close();

            return result;
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
