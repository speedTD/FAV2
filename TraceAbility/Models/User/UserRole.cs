using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestABC.Models.Data
{
    public class UserWithRoles
    {
        public int UserID { get; set; }
        public List<Role> Roles { get; set; }
    }
    public class ReturnUserWithRoles
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public int Total { get; set; }

        public Role aUserWithRoles { get; set; }

        public List<Role> lstUserWithRoles;
    }

    
    public class RoleWithPermissionMenus
    {
        public int RoleID { get; set; }
        public List<Menu> Menus { get; set; }
    }
    public class ReturnRoleWithPermissionMenus
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public int Total { get; set; }

        public RoleWithPermissionMenus aRoleWithPermissionMenus { get; set; }

        public List<RoleWithPermissionMenus> lstRoleWithPermissionMenus;
    }
}