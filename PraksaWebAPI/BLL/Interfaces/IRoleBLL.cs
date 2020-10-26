using PraksaWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PraksaWebAPI.BLL.Interfaces
{
    public interface IRoleBLL
    {
        public List<Role> GetRoles(string name);
        public int DeleteRole(long id);
        public int AddArole(Role newRole);
        public Role GetRoleByID(long id);
        public int UpdateRole(Role update);

    }
}
