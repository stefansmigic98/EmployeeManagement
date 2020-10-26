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
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskBLL _taskBLL;
        private readonly ILogger<TaskController> _logger;
        public TaskController(ITaskBLL taskBLL, ILogger<TaskController> logger)
        {
            this._taskBLL = taskBLL;
            this._logger = logger;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetTasks()
        {
            return Ok(_taskBLL.GetTasks());
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddTask([FromBody] TaskModel newTask)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            string role = claimsIdentity.FindFirst(ClaimTypes.Role).Value;
            long id = long.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
            newTask.CreatedBy = id;

            _taskBLL.AddTask(newTask);
            return Ok("Task uspesno dodat");
        }

        [HttpDelete]
        [Authorize(Roles="Admin")]
        [Route("{id}")]
        public IActionResult DeleteTask(long id)
        {
            var res = _taskBLL.DeleteTask(id);
            if (res == 1)
                return Ok("Task id=" + id + " uspesno obrisano");
            else
                return BadRequest("Brisanje neuspesno");
        }
        [Authorize]
        [HttpGet]
        [Route("finish/{id}")]
        public IActionResult FinishTask(long id)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            string role = claimsIdentity.FindFirst(ClaimTypes.Role).Value;

            var res = _taskBLL.FinishTask(id, role);
            
            return Ok("Task id=" + id + " je zavrsen");
           
        }

        [Authorize]
        [HttpGet]
        [Route("tasksforuser/{id}")]
        public IActionResult TasksForUser(long id)
        {

            return Ok(_taskBLL.TasksForUser(id));

        }
        [Authorize]
        [HttpGet]
        [Route("currentUser")]

        public IActionResult CurrentUser()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            string role = claimsIdentity.FindFirst(ClaimTypes.Role).Value;
            long id = long.Parse( claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
            return Ok(_taskBLL.TasksForUser(id));
        }

    }
}