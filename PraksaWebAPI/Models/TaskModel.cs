using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PraksaWebAPI.Models
{
    public class TaskModel
    {
        public long ID { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool Finished { get; set; } = false;
        public long CreatedBy { get; set; }
        public long[] Employees { get; set; }
    }
}
