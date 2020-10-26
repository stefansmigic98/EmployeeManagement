using PraksaWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PraksaWebAPI.BLL.Interfaces
{
    public interface IEmployeeBLL
    {
        public List<EmployeeInfo> GetEmployees(string name);
        public List<EmployeeInfo> GetEmployeesForRole(long roleID);
        public List<EmployeeInfo> GetEmployeesForDep(long depID);
        public int AddEmployee(Employee newEmployee);
        public int DeleteEmployee(long id);
        public EmployeeInfo BestSalary();
        public int UpdateEmploye(Employee update, string role, long id);
        public Employee Login(LoginModel loginModel);

    }
}
