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
        public void driveinfo()
        {
            foreach (var drive in DriveInfo.GetDrives())
            {
                double freeSpace = 0;
                double totalSpace = 0;
                double percent = 0;
                long availFreeSize = 0;
                long totalSize = 0;
                float num = 0;

                try
                { 
                    freeSpace = drive.TotalFreeSpace;
                    totalSpace = drive.TotalSize;
                    percent = (freeSpace / totalSpace) * 100;
                    num = (float)percent;
                    availFreeSize = drive.AvailableFreeSpace;
                    totalSize = drive.TotalSize;
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                }

                //console.writeline("drive:{0} with {1} % free", drive.name, num);
                //console.writeline("space remaining:{0}", availfreesize);
                //console.writeline("percent free space:{0}", percent);
                //console.writeline("space used:{0}", totalsize);
                //console.writeline("type: {0}", drive.drivetype);
            }
        }

        public void searchDrive()
        {
            foreach (var drive in DriveInfo.GetDrives())
            {
                DriveModel model = new DriveModel();
                model.drive_name = drive.Name;
                model.drive_type = drive.DriveType.ToString();

                SqliteExcuteQuery exQuery = new SqliteExcuteQuery();
                exQuery.InsertDriveTable(model);


            }
        }

    }
}
