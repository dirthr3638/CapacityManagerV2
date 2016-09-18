using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CapacityManagerMain.FileHelper
{
    class DirectorySearcher
    {
        //해당 폴더의 모든 파일 폴더를 검색함
        public long getDirectoryWriteTime(String folder)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(folder);
            return dirInfo.LastWriteTime.Ticks;
        }
    }
}
