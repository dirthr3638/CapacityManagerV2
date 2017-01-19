using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CapacityManagerMain.Model;
using CapacityManagerMain.Sqlite;

namespace CapacityManagerMain.FileHelper
{
    class DriveSearcher
    {
        //드라이브리스트를 가져옴
        public List<DriveModel> searchDriveList()
        {
            List<DriveModel> resultList = new List<DriveModel>();

            foreach (var drive in DriveInfo.GetDrives())
            {
                DriveModel model = new DriveModel();
                model.drive_name = drive.Name;
                model.drive_type = drive.DriveType.ToString();

                if (drive.DriveType.Equals(DriveType.Fixed))
                    model.size = drive.TotalSize;

                resultList.Add(model);
            }

            return resultList;
        }

        //SQLite 에서 드라이브 정보를 가져옴
        public List<DriveModel> getDriveListFromSql()
        {
            
            SqliteExcuteQuery exQuery = new SqliteExcuteQuery();
            List<DriveModel> result = exQuery.SelectDriveModel();

            return result;
        }



    }
}
