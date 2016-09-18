using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using CapacityManagerMain.FileHelper;

namespace CapacityManagerMain
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //DirectorySearcher dirs = new DirectorySearcher();
            //dirs.getDirectoryWriteTime("D:/");

            //            FileHelper.FileSearcher fSearcher = new FileHelper.FileSearcher();
            //            fSearcher.fileListInFolder("X:/");

            Sqlite.SqliteCreator createor = new Sqlite.SqliteCreator();
            createor.createSqlFile();
            createor.createScheme();

            FileHelper.DriveSearcher fDrive = new FileHelper.DriveSearcher();
            fDrive.searchDrive();

            //DriveSearcher Drive = new DriveSearcher();
            //Drive.getDriveListFromSql();

        }
    }
}
