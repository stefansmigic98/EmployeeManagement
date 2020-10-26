using System;
using System.Collections.Generic;
using System.Linq;
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
    public class RoleController : ControllerBase
    {

        private readonly IRoleBLL _roleBLL;
        private readonly ILogger<RoleController> _logger;
        public RoleController(IRoleBLL roleBLL, ILogger<RoleController> logger)
        {
            this._roleBLL = roleBLL;
            this._logger = logger;
        }

        [Authorize(Roles="Admin")]
        [HttpPost]
        public IActionResult AddArole([FromBody] Role newRole)
        {

            _roleBLL.AddArole(newRole);
            return Ok("Rola uspesno dodata");

        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteRole(long id)
        {

            var res = _roleBLL.DeleteRole(id);
            if (res == 1)
                return Ok("Rola id=" + id + " uspesno obrisana");
            else
                return BadRequest("Brisanje neuspesno, id ne postoji");
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetRoles(string name)
        {
            return Ok(_roleBLL.GetRoles(name));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IActionResult UpdateRole([FromBody] Role update )
        {
            

            if (update.RoleName == null || update.RoleName == "")
                return BadRequest("Nepravilno RoleName");

            var res = _roleBLL.UpdateRole(update);
            if (res >=0)
              return Ok("Rola id=" + update.ID + " uspesno promenjeno");
            else
                return NotFound("Id ne postoji");
        }
        
    }
}