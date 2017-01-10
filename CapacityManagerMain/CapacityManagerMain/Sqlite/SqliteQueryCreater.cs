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

        static public string SqlConnectionString
        {
            get
            {
                return "Data Source=" + sqlPullPath + ";";
            }
        }

        static public string createDrive()
        {
            return @"CREATE TABLE `DRIVE_INFO` (	
                    `drive_code`    INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,	
                    `drive_name`	TEXT NOT NULL,	
                    `drive_type`	TEXT NOT NULL,                
                    `size`	    INTEGER NOT NULL,
                    `search_yn`	INTEGER NOT NULL,	
                    `use_yn`	INTEGER NOT NULL DEFAULT 1 )";
        }

        static public string createFolder()
        {
            return @"CREATE TABLE `FOLDER_INFO` (	
                    `folder_code`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,		
                    `parent_fd_cd`	INTEGER,	
                    `drive_code`	INTEGER NOT NULL,	
                    `folder_name`	TEXT NOT NULL,
                    `last_write_time`	NUMERIC NOT NULL,	
                    `search_yn`	INTEGER NOT NULL,	
                    `create_time`	NUMERIC NOT NULL,	
                FOREIGN KEY(`parent_fd_cd`) REFERENCES `FOLDER_INFO`(`folder_code`),	
                FOREIGN KEY(`drive_code`) REFERENCES `DRIVE_INFO`(`drive_code`)) ";
        }

        static public string createFile()
        {
            return @"CREATE TABLE `FILE_INFO` (	
                    `file_code`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,	
                    `folder_code`	INTEGER NOT NULL,	
                    `file_name`	TEXT NOT NULL,	
                    `file_ext`	TEXT NOT NULL,	
                    `file_path`	TEXT NOT NULL,	
                    `file_volume`	NUMERIC NOT NULL,	
                    `last_write_time`	NUMERIC NOT NULL,	
                    `create_time`	NUMERIC NOT NULL,
                FOREIGN KEY(`folder_code`) REFERENCES `FOLDER_INFO`(`folder_code`))"; 
        }

        static public string insertDrive(DriveModel drive)
        {
            return @"INSERT INTO DRIVE_INFO(drive_name, drive_type, size, search_yn, use_yn) VALUES ('" + 
                drive.drive_name + "','" +
                drive.drive_type + "'," +
                drive.size + "," +
                drive.search_yn + "," +
                drive.use_yn + 
                ");";
        }

        static public string insertFolder(FolderInfoModel folder)
        {
            return @"INSERT INTO FOLDER_INFO(parent_fd_cd, drive_code, folder_name, search_yn, last_write_time, create_time) VALUES (" +
                folder.parent_fd_cd + "," +
                folder.drive_code + "," +
                folder.folder_name + "," +
                folder.search_yn + "," +
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

        static public string lastIndex()
        {
            return @"select last_insert_rowid()";
        }


        static public string selectDriveInfoList()
        {
            return @"SELECT drive_code,drive_name,drive_type,size,search_yn,use_yn FROM DRIVE_INFO";
        }

    }
}
