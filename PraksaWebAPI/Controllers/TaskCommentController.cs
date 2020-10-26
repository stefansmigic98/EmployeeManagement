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
using PraksaWebAPI.Models;

namespace PraksaWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskCommentController : ControllerBase
    {
        private readonly ITaskCommentBLL _taskCommentBLL;
        private readonly ILogger<TaskCommentController> _logger;
        public TaskCommentController(ITaskCommentBLL taskComment, ILogger<TaskCommentController> logger)
        {
            this._taskCommentBLL = taskComment;
            this._logger = logger;
        }


        [HttpPost]

        public IActionResult AddTask([FromBody] TaskComment taskComment)
        {
            
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            string role = claimsIdentity.FindFirst(ClaimTypes.Role).Value;

            long id = long.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);

            taskComment.EmployeeID = id;

            _taskCommentBLL.AddTaskComment(taskComment);
            _logger.LogInformation($"user {id} added task");
            return Ok();
        }

        [HttpGet]
        [Route("Task")]
        public IActionResult CommentsForTask(int taskid)
        {
            return Ok(_taskCommentBLL.CommentsForTask(taskid));
        }

    }
}