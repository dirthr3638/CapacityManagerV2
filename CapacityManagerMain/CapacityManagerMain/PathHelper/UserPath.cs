using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CapacityManagerMain.PathHelper
{
    static class UserPath
    {
        //"C:\\Documents and Settings\\[USER]\\Application Data"
        static public string getAppdataPath()
        {
//            string path = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)).FullName;
            String path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
 
            //Vista/Win7 or XP and without using environment variables
            if (Environment.OSVersion.Version.Major >= 6)
            {
               // path = Directory.GetParent(path).ToString();
            }

            string returnPath = path + "\\" + Properties.Resources.STRING_PROJECT_NAME;

            if (!Directory.Exists(returnPath))
            {
                Directory.CreateDirectory(returnPath);
            }

            return returnPath;
        }
    }
}
