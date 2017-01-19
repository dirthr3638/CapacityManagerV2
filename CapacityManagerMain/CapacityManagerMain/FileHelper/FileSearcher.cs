using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CapacityManagerMain.Model;

namespace CapacityManagerMain.FileHelper
{
    class FileSearcher
    {

        public List<FIleInfoModel> getFilesInFolder(FolderInfoModel folder)
        {
            DirectoryInfo di = new DirectoryInfo(folder.folder_name);
            List<FIleInfoModel> files = new List<FIleInfoModel>();

            foreach (FileInfo item in di.GetFiles())
            {
                FIleInfoModel fItem = new FIleInfoModel();
                fItem.folder_code = folder.folder_code;
                fItem.file_name = item.Name;
                fItem.file_ext = item.Extension;
                fItem.file_volume = item.Length;
                fItem.last_write_time = item.LastWriteTime.Ticks;
                fItem.create_time = item.CreationTime.Ticks;

                files.Add(fItem);
            }

            return files;
        }


    }
}
