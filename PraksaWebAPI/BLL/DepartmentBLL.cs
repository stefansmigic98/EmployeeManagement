using PraksaWebAPI.BLL.Interfaces;
using PraksaWebAPI.DAL.Interfaces;
using PraksaWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PraksaWebAPI.BLL
{
    public class DepartmentBLL: IDepartmentBLL
    {
        private readonly IDepartmentDAL _departmentDAL;
        public DepartmentBLL(IDepartmentDAL departmentDAL)
        {
            this._departmentDAL = departmentDAL;
        }

        public int AddDepartment(Department department)
        {
            return _departmentDAL.AddDepartment(department);
        }

        public int DeleteDepartment(long id)
        {
            return _departmentDAL.DeleteDepartment(id);
        }

        public List<Department> GetDepartments()
        {
            return _departmentDAL.GetDepartments();
        }

        public int UpdateDepartment(Department department)
        {
            return _departmentDAL.UpdateDepartment(department);
        }

        public Department WithMostEmployees()
        {
            return _departmentDAL.WithMostEmployees();
        }
    }
}
