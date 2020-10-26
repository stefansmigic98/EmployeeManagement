using PraksaWebAPI.BLL.Interfaces;
using PraksaWebAPI.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PraksaWebAPI.BLL
{
    public class ShareBLL: IShareBLL
    {
        private readonly IShareDAL _shareDAL;

        public ShareBLL(IShareDAL shareDAL)
        {
            this._shareDAL = shareDAL;
        }

        public string CreateShare(string shareId, string fileId, string user)
        {
            return _shareDAL.CreateShare(shareId, fileId, user);
        }
    }
}
