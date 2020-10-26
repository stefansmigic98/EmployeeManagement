using PraksaWebAPI.BLL.Interfaces;
using PraksaWebAPI.DAL.Interfaces;
using PraksaWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PraksaWebAPI.BLL
{
    
    public class RoleBLL : IRoleBLL
    {
        private readonly IRoleDAL _roleDAL;

        public RoleBLL(IRoleDAL roleDAL)
        {
            this._roleDAL = roleDAL;
        }

       
        public int AddArole(Role newRole)
        {
            return _roleDAL.AddArole(newRole);
        }

        public int DeleteRole(long id)
        {
            return _roleDAL.DeleteRole(id);
        }

        public Role GetRoleByID(long id)
        {
            return _roleDAL.GetRoleByID(id);
        }

        public List<Role> GetRoles(string name)
        {
            if (name == null)
                return _roleDAL.GetRoles("");
            else
                return _roleDAL.GetRoles(name);
        }

        public int UpdateRole(Role update)
        {
            return _roleDAL.UpdateRole(update);
        }
    }
}
