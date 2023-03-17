using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAssembly.Shared.Models
{
    [NotMapped]
    public class UserLogin
    {
        public string mail { get; set; }
        public string password { get; set; }
    }
}
