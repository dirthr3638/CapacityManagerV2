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
        private int FileCount = 0;
        private int FolderCount = 0;

        //해당 폴더의 모든 파일 폴더를 검색함
        public void fileListInFolder(String folder)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(folder);
            consoleOutAllFileList(dirInfo);

            Console.WriteLine("폴더갯수 ::: " + FolderCount + ", 파일갯수::: " + FileCount);
        }

        //재귀함수
        private void consoleOutAllFileList(DirectoryInfo dirInfo)
        {
            FileInfo[] files = dirInfo.GetFiles("*.*");
            //파라미터폴더 내의 모든 폴더 획득
            DirectoryInfo[] folders = dirInfo.GetDirectories("*");

            //디렉토리를 해쉬값 비교

            FolderListDisposer(folders);
            FileListDisposer(files);
        }

        //폴더를 돌며 파일 검색 (폴더정보: 부모폴더코드, 드라이브코드, 마지막쓰기일시, 생성일시)
        private void FolderListDisposer(DirectoryInfo[] folders, int driveCode = -1, int folderCode=-1)
        {
            foreach (DirectoryInfo oneFolder in folders)
            {
                //Console.WriteLine(oneFolder.Name);

                try {
                    FolderInfoModel folder = new FolderInfoModel();

                    folder.parent_fd_cd = folderCode;
                    folder.drive_code = driveCode;
                    folder.folder_name = oneFolder.Name;
                    folder.last_write_time = oneFolder.LastWriteTime.Ticks;
                    folder.create_time = oneFolder.CreationTime.Ticks;

                    consoleOutAllFileList(oneFolder);
                    FolderCount++;
                }
                catch(Exception e)
                {
                    Console.WriteLine(oneFolder.FullName + " || " + e.Message);
                }
            }
 
        }

        //폴더코드, 파일명, 확장자, 파일경로, 용량, 마지막수정일시, 생성일자
        private void FileListDisposer(FileInfo[] files, int folderCode = -1)
        {
            List<FIleInfoModel> fileList = new List<FIleInfoModel>();

            foreach (FileInfo oneFile in files)
            {
                FIleInfoModel file = new FIleInfoModel();
                file.folder_code = folderCode;
                file.file_name = oneFile.Name;
                file.file_ext = oneFile.Extension;
                file.file_path = oneFile.DirectoryName;
                file.file_volume = oneFile.Length;
                file.last_write_time = oneFile.CreationTime.Ticks;
                file.create_time = oneFile.LastWriteTime.Ticks;

                fileList.Add(file);

                FileCount++;
                //Console.WriteLine(oneFile.Name);
            }

 
        }

    }
}
