
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAssembly.Shared.Models
{
    [NotMapped]
    public class UserSession : User
    {
        public string role { get; set; }
        public string Token { get; set; }
        public int ExpiresIn { get; set; }
        public DateTime ExpiryTimeStamp { get; set; }
    }
}
