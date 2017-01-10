using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CapacityManagerMain.Sqlite;
using CapacityManagerMain.Model;

namespace CapacityManagerMain.FileHelper
{
    class DirectorySearcher
    {
        public long getDirectoryWriteTime(String folder)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(folder);
            return dirInfo.LastWriteTime.Ticks;
        }

        public List<DirectoryInfo> getDirectoryInDrive(DriveModel drive)
        {
            List<DirectoryInfo> derectoreis = new List<DirectoryInfo>();

            DirectoryInfo dir = new DirectoryInfo(drive.drive_name);

            foreach(DirectoryInfo item in dir.GetDirectories())
            {
                derectoreis.Add(item);
            }

            return derectoreis;
        }



    }
}
