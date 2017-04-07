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
        public int InsertDrivesTable(DriveModels drive)
        {
            long lastId = -1;
            
            using (SQLiteConnection conn = new SQLiteConnection(SqliteQueryCreater.SqlDbPath))
            {
                conn.Open();

                foreach(DriveModel item in drive)
                {
                    string sql = SqliteQueryCreater.insertDrive(item);
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    sql = SqliteQueryCreater.lastIndex();
                    cmd = new SQLiteCommand(sql, conn);
                    lastId = (long)cmd.ExecuteScalar();
                }
                
                conn.Close();
            }
            return unchecked((int)lastId);
        }

        //return value : drive index
        public void InsertDrivesTable(DriveModels drive, SQLiteConnection conn)
        {
            foreach (DriveModel item in drive)
            {
                string sql = SqliteQueryCreater.insertDrive(item);
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
        }

        public List<DriveModel> SelectDriveModel()
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection(SqliteQueryCreater.SqlConnectionString);
            m_dbConnection.Open();
            
            string sql = SqliteQueryCreater.selectDriveInfoList();
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            List<DriveModel> result = new List<DriveModel>();

            while (reader.Read()) {
                DriveModel driveModel = new DriveModel();
                driveModel.drive_code = Int32.Parse(reader["drive_code"].ToString());
                driveModel.drive_name = reader["drive_name"].ToString();
                driveModel.drive_type = reader["drive_type"].ToString();
                driveModel.size = long.Parse(reader["size"].ToString());
                driveModel.search_yn = Int32.Parse(reader["search_yn"].ToString());
                driveModel.use_yn = Int32.Parse(reader["use_yn"].ToString());

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
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    e.Source = sql;
                    throw e;
                }
                conn.Close();
            }

            return 1;
        }

        public long InsertFolderTable(FolderInfoModel folder, SQLiteConnection conn)
        {
            string sql = SqliteQueryCreater.insertFolder(folder);
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                e.Source = sql;
                throw e;
            }
            sql = SqliteQueryCreater.lastIndex();
            cmd = new SQLiteCommand(sql, conn);
            long lastId = (long)cmd.ExecuteScalar();

            return lastId;
        }

        public int InsertFileTable(FIleInfoModel file)
        {
            using (SQLiteConnection conn = new SQLiteConnection(SqliteQueryCreater.SqlDbPath))
            {
                conn.Open();
                string sql = SqliteQueryCreater.insertFile(file);
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    e.Source = sql;
                    throw e;
                }
                conn.Close();
            }

            return 1;
        }

        public void InsertFileTable(FIleInfoModel file, SQLiteConnection conn)
        {
            string sql = SqliteQueryCreater.insertFile(file);
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            try
            { 
                cmd.ExecuteNonQuery();
            }catch(Exception e)
            {
                e.Source = sql;
                throw e;
            }
        }


        public int InsertFileListTableTx(List<FIleInfoModel> files)
        {
            foreach(FIleInfoModel file in files){

            }
            return 1;
        }


    }
}
