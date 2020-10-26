using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PraksaWebAPI.Models
{
    public class ShareModel
    {
        public string Name { get; set; }
        public string DocumentID { get; set; }

        public string SharedBy { get; set; }
    }
}
