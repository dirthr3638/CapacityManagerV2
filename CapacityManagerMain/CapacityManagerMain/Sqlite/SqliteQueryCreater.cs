using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapacityManagerMain.Model;

namespace CapacityManagerMain.Sqlite
{
    static class SqliteQueryCreater
    {
        static private String sqlPullPath = PathHelper.UserPath.getAppdataPath() + "\\" + Properties.Resources.FILENAME_SQLITE_MAIN;

        static public string SqlDbPath
        {
            get
            {
                return sqlPullPath;
            }
        }

        static public string insertDrive(DriveModel drive)
        {
            return @"INSERT INTO DRIVE_INFO(drive_name, drive_type, drive_hash, use_yn) VALUES ('" + 
                drive.drive_name + "','" +
                drive.drive_type + "'," +
                drive.drive_hash + "," +
                drive.use_yn + 
                ");";
        }

        static public string insertFolder(FolderInfoModel folder)
        {
            return @"INSERT INTO FOLDER_INFO(parent_fd_cd, drive_code, folder_name, last_write_time, create_time) VALUES (" +
                folder.parent_fd_cd + "," +
                folder.drive_code + "," +
                folder.folder_name + "," +
                folder.last_write_time + "," +
                folder.create_time +
                ");";
        }

        static public string insertFile(FIleInfoModel file)
        {
            return @"INSERT INTO FILE_INFO(folder_code, file_name, file_ext, file_path, file_volume, last_write_time, create_time) VALUES (" +
                file.folder_code + "," +
                file.file_name + "," +
                file.file_ext + "," +
                file.file_path + "," +
                file.file_volume +
                file.last_write_time +
                file.create_time +
                ");";
        }

        static public string updateQuery()
        {

            return "";
        }

        static public string selectQuery()
        {

            return "";
        }

        static public string deleteQuery()
        {
            return "";
        }
    }
}
