using PraksaWebAPI.BLL.Interfaces;
using PraksaWebAPI.DAL.Interfaces;
using PraksaWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PraksaWebAPI.BLL
{
    public class DocumentBLL : IDocumentBLL
    {
        private readonly IDocumentDAL _documentDAL;
        
        public DocumentBLL(IDocumentDAL documentDAL)
        {
            this._documentDAL = documentDAL;
        }

        public void UploadDocument(FileModel model, long employeeID)
        {
            _documentDAL.UploadDocument(model, employeeID);
        }

        public List<Document> MyDocuments(long employeeID)
        {
            return _documentDAL.MyDocuments(employeeID);
        }

        public List<Document> SharedWithUser(long id)
        {
            return _documentDAL.SharedWithUser(id);
        }
    }
}
