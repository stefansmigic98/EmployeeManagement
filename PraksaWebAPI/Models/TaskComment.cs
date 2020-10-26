using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PraksaWebAPI.Models
{
    public class TaskComment
    {
        public long ID { get; set; }
        public string Content { get; set; }
        public long EmployeeID { get; set; }
        public long TaskID { get; set; }
    }
}
