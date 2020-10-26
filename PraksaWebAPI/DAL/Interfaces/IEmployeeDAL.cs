using PraksaWebAPI.Models;
using System.Collections.Generic;

namespace PraksaWebAPI.DAL.Interfaces
{
    public interface IEmployeeDAL
    {
        public List<EmployeeInfo> GetEmployees(string name);
        public List<EmployeeInfo> GetEmployeesForRole(long roleID);
        public List<EmployeeInfo> GetEmployeesForDep(long depID);
        public int AddEmployee(Employee newEmployee);
        public int DeleteEmployee(long id);
        public EmployeeInfo BestSalary();
        public Employee GetEmployeeById(long id);
        public int UpdateEmploye(Employee update);
        public Employee Login(LoginModel loginModel);
    }
}
