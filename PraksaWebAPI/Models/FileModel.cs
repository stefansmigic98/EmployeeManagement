using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PraksaWebAPI.Models
{
    public class FileModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string MimeType { get; set; }
        public string DrivePath { get; set; }
    }
}
