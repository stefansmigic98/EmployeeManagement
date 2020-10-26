using PraksaWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PraksaWebAPI.Helpers
{
    public interface IGoogleDriveApiHelper
    {
        public void DownloadFile(string id, string savePath);
        public FileModel UploadFile(string path);
        public IList<Google.Apis.Drive.v3.Data.File> GetFiles();
        public string GetFileName(string id);
        public string ShareFile(string id, string user);
    }
}
