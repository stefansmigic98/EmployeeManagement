using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PraksaWebAPI.BLL.Interfaces;
using PraksaWebAPI.Helpers;
using PraksaWebAPI.Models;
using System;
using System.Linq;
using System.Security.Claims;

namespace PraksaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeBLL _employeeBLL;
        private readonly IRoleBLL _roleBLL;
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmployeeController> _logger;
        public EmployeeController(IEmployeeBLL employeeBLL, IRoleBLL roleBLL, IConfiguration configuration, ILogger<EmployeeController> logger)
        {
            this._employeeBLL = employeeBLL;
            this._roleBLL = roleBLL;
            this._configuration = configuration;
            JwtHelper.Singletion.SetConfig(configuration);
            this._logger = logger;
        }

        //[Authorize]
        [HttpGet]

        public IActionResult GetEmployees(string name)
        {
            _logger.LogInformation("Get employees");
            return Ok(_employeeBLL.GetEmployees(name));
        }

        [HttpGet]
        [Route("role/{roleID}")]

        public IActionResult GetEmployeesForRole(long roleID)
        {
            return Ok(_employeeBLL.GetEmployeesForRole(roleID));
        }

        [HttpGet]
        [Route("dep/{depID}")]

        public IActionResult GetEmployeesForDep(long depID)
        {
            return Ok(_employeeBLL.GetEmployeesForDep(depID));
        }

        [HttpPost]
        [Authorize(Roles="Admin")]
        public IActionResult AddEmployee([FromBody] Employee newEmployee)
        {

            
            var res = _employeeBLL.AddEmployee(newEmployee);
            if (res == 0)
                return Ok("Zaposleni uspesno dodat!");

            return BadRequest("Korisnik nije dodat");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteEmployee(long id)
        {

            var res = _employeeBLL.DeleteEmployee(id);
            if (res == 1)
                return Ok("Zaposleni sa id=" + id + " uspesno obrisan");
            else
                return NotFound("Zaposleni ne postoji");

        }

        [HttpGet]
        [Authorize]
        [Route("BestSalary")]
        public IActionResult BestSalary()
        {
            return Ok(_employeeBLL.BestSalary());
        }

        [HttpPut]
        [Authorize]
        public IActionResult UpdateEmploye([FromBody] Employee update)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            string role = claimsIdentity.FindFirst(ClaimTypes.Role).Value;
           
            long id = long.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);

            var res = _employeeBLL.UpdateEmploye(update,role,id);
            if (res == 0)
                return Ok("Podaci za zaposlenog id=" + update.ID + " promenjeni!");
            else
                return BadRequest("Zaposleni ne postoji");
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("createToken")]
        public IActionResult CreateToken([FromBody] LoginModel loginModel)
        {
             IActionResult response = Unauthorized();

             Employee employee = _employeeBLL.Login(loginModel);
             
             if (employee != null)
             {
                Role role = _roleBLL.GetRoleByID(employee.RoleID);
                var token = JwtHelper.Singletion.BuildToken(employee, role.RoleName);
                 response = Ok(new{ token = token});
             }
             else
            {
                return BadRequest();
            }
             return response;
           
        }


    }
}