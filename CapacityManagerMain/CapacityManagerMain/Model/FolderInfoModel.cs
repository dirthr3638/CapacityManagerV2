using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapacityManagerMain.Model
{
    class FolderInfoModel
    {
        public int folder_code { get; set; }
        public int parent_fd_cd { get; set; }
        public int drive_code { get; set; }
        public string folder_name { get; set; }
        public long last_write_time { get; set; }
        public long create_time { get; set; }
        public int folder_hash { get; set; }
    }
}
