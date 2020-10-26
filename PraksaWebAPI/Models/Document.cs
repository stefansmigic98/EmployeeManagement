using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PraksaWebAPI.Models
{
    public class Document
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string DrivePAth { get; set; }
        public DateTime CreationDate { get; set; }
        public string DriveDocumentID { get; set; }
        public long EmployeeID { get; set; }
    }
}
