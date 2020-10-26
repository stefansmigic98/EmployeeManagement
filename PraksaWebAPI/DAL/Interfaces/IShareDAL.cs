using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PraksaWebAPI.DAL.Interfaces
{
    public interface IShareDAL
    {
        public string CreateShare(string shareId, string fileId, string user);
    }
}
