using PraksaWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PraksaWebAPI.DAL.Interfaces
{
    public interface IDocumentDAL
    {
        public void UploadDocument(FileModel model, long employeeID);
        public List<Document> MyDocuments(long EmployeeID);
        public List<Document> SharedWithUser(long id);
    }
}
