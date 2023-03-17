using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAssembly.Shared.Models
{
    public class Message
    {
        public Guid id { get; set; }
        public Guid senderId { get; set; }
        public Guid receiverId { get; set; }
        public DateTime sendDate { get; set; }
        public string message { get; set; }
        public bool isDeleted { get; set; }
    }
}
