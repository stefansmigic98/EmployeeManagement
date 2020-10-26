using PraksaWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PraksaWebAPI.DAL.Interfaces
{
    public interface IDepartmentDAL
    {
        public List<Department> GetDepartments();
        public int DeleteDepartment(long id);
        public int AddDepartment(Department department);
        public Department WithMostEmployees();
        public int UpdateDepartment(Department department);
    }
}
