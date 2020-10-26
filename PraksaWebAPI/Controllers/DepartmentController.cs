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
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentBLL _departmentBLL;
        private readonly ILogger<DepartmentController> _logger;

        public DepartmentController(IDepartmentBLL departmentBLL, ILogger<DepartmentController> logger)
        {
            this._departmentBLL = departmentBLL;
            this._logger = logger;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetDepartments()
        {
            return Ok(_departmentBLL.GetDepartments());
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteDepartment(long id)
        {
            var res = _departmentBLL.DeleteDepartment(id);
            if (res == 1)
                return Ok("Department sa id=" + id + " je obrisan");
            else
                return BadRequest("Brisanje nije uspesno");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddDepartment([FromBody] Department department)
        {
            if (department.Name == null)
                return BadRequest("Unesite ime");

            _departmentBLL.AddDepartment(department);
            return Ok("Department uspesno dodat");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IActionResult UpdateDepartment([FromBody] Department department)
        {
           

            if (department.Name == null || department.Name == "")
                return BadRequest("Nepravilno RoleName");

            var res = _departmentBLL.UpdateDepartment(department);
            return Ok("Department id=" + department.ID + " uspesno promenjen");
        }

        [HttpGet]
        [Authorize]
        [Route("mostemployees")]
        public IActionResult WithMostEmployees()
        {
            return Ok(_departmentBLL.WithMostEmployees());
        }
    }
}