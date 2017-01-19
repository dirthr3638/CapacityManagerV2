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

        public List<FolderInfoModel> getDirectoryInDrive(DriveModel drive)
        {
            DirectoryInfo di = new DirectoryInfo(drive.drive_name);
            List<FolderInfoModel> derectories = new List<FolderInfoModel>();

            foreach (DirectoryInfo item in di.GetDirectories())
            {
                FolderInfoModel fItem = new FolderInfoModel();
                fItem.drive_code = drive.drive_code;
                fItem.folder_path = item.FullName;
                fItem.folder_name = item.Name;
                fItem.last_write_time = item.LastWriteTime.Ticks;
                fItem.create_time = item.CreationTime.Ticks;

                derectories.Add(fItem);
            }

            return derectories;
        }

        public List<FolderInfoModel> getDirectoryInDirectory(FolderInfoModel directory)
        {
            List<FolderInfoModel> derectories = new List<FolderInfoModel>();
            DirectoryInfo di = new DirectoryInfo(directory.folder_path);

            foreach (DirectoryInfo item in di.GetDirectories())
            {
                FolderInfoModel fItem = new FolderInfoModel();

                fItem.parent_fd_cd = directory.folder_code;
                fItem.drive_code = directory.drive_code;
                fItem.folder_path = item.FullName;
                fItem.folder_name = item.Name;
                fItem.last_write_time = item.LastWriteTime.Ticks;
                fItem.create_time = item.CreationTime.Ticks;

                derectories.Add(fItem);
            }

            return derectories;
        }
    }
}
