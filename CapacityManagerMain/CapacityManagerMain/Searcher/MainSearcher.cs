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
                    ds.getDirectoryInDrive(item);
                }catch(IOException iE)  //준비되지않은 장치 일경우(CD-ROM 등)
                {
                    break;
                }

            }
            

        }
    }
}
