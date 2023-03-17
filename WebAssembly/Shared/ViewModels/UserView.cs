using WebAssembly.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAssembly.Shared.ViewModels
{
    [NotMapped]
    public class UserView : User
    {
        public string role { get; set; }
    }
}
