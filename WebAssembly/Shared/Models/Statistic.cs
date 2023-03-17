using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAssembly.Shared.Models
{
    public class Statistic
    {
        public int id { get; set; }
        public Int16 oee { get; set; }
        public Int16 quality { get; set; }
        public Int16 performance { get; set; }
        public Int16 availability { get; set; }
        public DateTime recordDate { get; set; }
        public int departmentId { get; set; }
        public bool isDeleted { get; set; }

    }
}
