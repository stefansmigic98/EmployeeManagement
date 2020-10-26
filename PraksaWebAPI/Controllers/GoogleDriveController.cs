using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PraksaWebAPI.BLL.Interfaces;
using PraksaWebAPI.Helpers;
using PraksaWebAPI.Models;

namespace PraksaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GoogleDriveController : ControllerBase
    {
        private readonly IGoogleDriveApiHelper _googleDriveApiHelper;
        private readonly IDocumentBLL _documentBLL;
        private readonly IShareBLL _shareBLL;
        private readonly ILogger<GoogleDriveController> _logger;

        public GoogleDriveController(IGoogleDriveApiHelper googleDriveApiHelper , IDocumentBLL documentBLL, IShareBLL shareBLL, ILogger<GoogleDriveController> logger)
        {
            this._googleDriveApiHelper = googleDriveApiHelper;
            this._documentBLL = documentBLL;
            this._shareBLL = shareBLL;
            this._logger = logger;
        }


        [HttpGet]
        [Route("ListFiles")]
        public IActionResult ListFiles()
        {
            return Ok(_googleDriveApiHelper.GetFiles());
        }

        [HttpGet]
        [Route("DownloadFile")]

        public IActionResult DownloadFile(string id, string savePath)
        {
            string name = _googleDriveApiHelper.GetFileName(id);
            _googleDriveApiHelper.DownloadFile(id, savePath+ "//"+name);

            return Ok();
        }
        [HttpPost]
        [Route("UploadFile")]

        public IActionResult UploadFile(string path)
        {

            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            string role = claimsIdentity.FindFirst(ClaimTypes.Role).Value;
            long id = long.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);

            FileModel file = _googleDriveApiHelper.UploadFile(path);
            _documentBLL.UploadDocument(file, id);

            return Ok();
        }

        [HttpPost]
        [Route("shareFiles")]

        public string ShareFile(string id, string user)
        {
            string res = _googleDriveApiHelper.ShareFile(id, user);

            _shareBLL.CreateShare(res, id, user);
            return res;
        }

        [HttpGet]
        [Route("myDocuments")]

        public IActionResult MyDocuments()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            string role = claimsIdentity.FindFirst(ClaimTypes.Role).Value;
            long id = long.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);

            return Ok(_documentBLL.MyDocuments(id));
        }
        [HttpGet]
        [Route("SharedWithUser")]

        public IActionResult SharedWithUser()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            string role = claimsIdentity.FindFirst(ClaimTypes.Role).Value;
            long id = long.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);

            return Ok(_documentBLL.SharedWithUser(id));
        }
    }
}