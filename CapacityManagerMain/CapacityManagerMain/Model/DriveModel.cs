using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapacityManagerMain.Model
{
    class DriveModel
    {
        public int drive_code { get; set; } = -1;
        public string drive_name { get; set; } = "";
        public string drive_type { get; set; } = "NONE";
        public int search_yn { get; set; } = 0;
        public int use_yn { get; set; } = 0;
    }
}
