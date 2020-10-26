using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PraksaWebAPI.Models
{
    public class Employee
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public long RoleID { get; set; }
        public float Salary { get; set; }
        public long DepartmentID { get; set; }
        public int FinishedTasks { get; set; } = 0;
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
