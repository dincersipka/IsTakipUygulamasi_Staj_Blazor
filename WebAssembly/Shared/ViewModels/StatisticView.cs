using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAssembly.Shared.Models;

namespace WebAssembly.Shared.ViewModels
{
    [NotMapped]
    public class StatisticView : Statistic
    {
        public string departmentName { get; set; }
    }
}
