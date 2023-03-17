using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAssembly.Shared.Models
{
    public class User
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string mail { get; set; }
        public string phone { get; set; }
        public string password { get; set; }
        public int roleId { get; set; }
        public int departmentId { get; set; }
        public bool isDeleted { get; set; }
    }
}
