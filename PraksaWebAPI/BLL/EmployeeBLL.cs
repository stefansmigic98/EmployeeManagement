using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PraksaWebAPI.BLL.Interfaces;
using PraksaWebAPI.DAL.Interfaces;
using PraksaWebAPI.Models;

namespace PraksaWebAPI.BLL
{
    public class EmployeeBLL : IEmployeeBLL
    {
        private readonly IEmployeeDAL _employeeDAL;

        public EmployeeBLL(IEmployeeDAL employeeDAL)
        {
            this._employeeDAL = employeeDAL;
        }

        public int AddEmployee(Employee newEmployee)
        {
            return _employeeDAL.AddEmployee(newEmployee);
        }

        public EmployeeInfo BestSalary()
        {
            return _employeeDAL.BestSalary();
        }

        public int DeleteEmployee(long id)
        {
            return _employeeDAL.DeleteEmployee(id);
        }

        

        public List<EmployeeInfo> GetEmployees(string name)
        {
            if (name == null)
                return _employeeDAL.GetEmployees("");
            else
                return _employeeDAL.GetEmployees(name);
        }

        public List<EmployeeInfo> GetEmployeesForDep(long depID)
        {
            return _employeeDAL.GetEmployeesForDep(depID);
        }

        public List<EmployeeInfo> GetEmployeesForRole(long roleID)
        {
            return _employeeDAL.GetEmployeesForRole(roleID);
        }

        public Employee Login(LoginModel loginModel)
        {
            return _employeeDAL.Login(loginModel) ;
        }

        public int UpdateEmploye(Employee update, string role, long id)
        {
            Employee employee = new Employee();
            employee = _employeeDAL.GetEmployeeById(update.ID);
            if (employee == null)
                return 1;
            if (role != "Admin" && update.ID != id)
                return 1;
            if (update.Name == null)
                update.Name = employee.Name;
            if (update.BirthDate == null || update.BirthDate == default(DateTime))
                update.BirthDate = employee.BirthDate;
            if (update.Salary == 0  )
                update.Salary = employee.Salary;
            if (update.DepartmentID == 0)
                update.DepartmentID = employee.DepartmentID;
            if (update.RoleID == 0)
                update.RoleID = employee.RoleID;
            if (update.FinishedTasks == 0)
                update.FinishedTasks = employee.FinishedTasks;
            if(update.Email == null)
                update.Email = employee.Email;
            if(update.Password == null)
                update.Password = employee.Password;

            _employeeDAL.UpdateEmploye(update);
            return 0;
        }
    }
}
