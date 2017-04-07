using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapacityManagerMain.FileHelper;
using CapacityManagerMain.Model;
using CapacityManagerMain.Sqlite;
using System.Data.SQLite;
using System.IO;

namespace CapacityManagerMain.Searcher
{
    class MainSearcher
    {
        /*
         * Main Search
         * 1. 드라이브 검색을 완료함
         * 2. 해당 드라이브의 폴더
        */
        public void mainSearch()
        {
            WriteSQLDriveInfo();
            WriteSQLDirectoryFileInfo();
        }


        public void WriteSQLDriveInfo()
        {
            DriveSearcher sDrive = new DriveSearcher();
            DriveModels driveModels = new DriveModels();

            driveModels.AddRange(sDrive.searchDriveList());

            SQLiteConnection m_dbConnection = new SQLiteConnection(SqliteQueryCreater.SqlConnectionString);
            m_dbConnection.Open();

            SqliteExcuteQuery query = new SqliteExcuteQuery();
            query.InsertDrivesTable(driveModels, m_dbConnection);
        }

        //SQL의 드라이브정보를 가져와 모든 드라이브 밑의 파일들정보를 SQL에 기록하는 메인 메서드
        //폴더 검색 재귀함수를 수행시켜 준다
        public void WriteSQLDirectoryFileInfo()
        {
            DriveSearcher sDrive = new DriveSearcher();
            DriveModels driveModels = new DriveModels();

            driveModels.AddRange(sDrive.getDriveListFromSql());


            foreach(DriveModel item in driveModels)
            {
                DirectorySearcher ds = new DirectorySearcher();
                FileSearcher fs = new FileSearcher();

                try
                {
                    //드라이브내의 파일들을 기록
                    using (SQLiteConnection conn = new SQLiteConnection(SqliteQueryCreater.SqlConnectionString))
                    {
                        conn.Open();
                        WriteSQLDirectoriesByDirs(ds.getDirectoryInDrive(item), conn);
                        conn.Close();
                    }
                }
                catch(IOException iE)  //준비되지않은 장치 일경우(CD-ROM 등)
                {
                    Console.Out.WriteLine(iE.Message);
                    break;
                }
            }
        }

        private void WriteSQLDirectoriesByDirs(List<FolderInfoModel> directories, SQLiteConnection conn)
        {
            foreach(FolderInfoModel item in directories)
            {
                try { 
                    reflectionDirectorySearch(item, conn);
                }
                catch (IOException iE)
                {
                    Console.Out.WriteLine(iE.Message);
                    continue;
                }
            }
        }

        //해당 폴더의 파일과 폴더가 없을때까지 검색한다
        private void reflectionDirectorySearch(FolderInfoModel folder, SQLiteConnection conn)
        {
            SqliteExcuteQuery query = new SqliteExcuteQuery();
            long index = query.InsertFolderTable(folder, conn);
            folder.folder_code = Int32.Parse(index.ToString());
            try
            {
                WriteSQLFiles(folder, conn);
            }catch(Exception e)
            {
                Console.Out.WriteLine(e.Message + " \n " + e.Source);
            }

            DirectorySearcher ds = new DirectorySearcher();
            try
            {
                WriteSQLDirectoriesByDirs(ds.getDirectoryInDirectory(folder), conn);
            }catch(Exception e)
            {
                Console.Out.WriteLine(e.Message + " \n " + e.Source);
            }
        }

        private void WriteSQLFiles(FolderInfoModel folder, SQLiteConnection conn)
        {
            DirectoryInfo dir = new DirectoryInfo(folder.folder_path);

            foreach(FileInfo item in dir.GetFiles())
            {
                FIleInfoModel fModel = new FIleInfoModel();
                fModel.folder_code = folder.folder_code;
                fModel.file_name = item.Name;
                fModel.file_ext = item.Extension;
                fModel.file_volume = item.Length;
                fModel.last_write_time = item.LastWriteTime.Ticks;
                fModel.create_time = item.CreationTime.Ticks;

                SqliteExcuteQuery query = new SqliteExcuteQuery();
                query.InsertFileTable(fModel, conn);
            }
        }

    }
}
