using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapacityManagerMain.Model
{
    class FIleInfoModel
    {
        public int file_code { get; set; }
        public int folder_code { get; set; }
        public string file_name { get; set; }
        public string file_ext { get; set; }
        public string file_path { get; set; }
        public long file_volume { get; set; }
        public long last_write_time { get; set; }
        public long create_time { get; set; }
    }
}
