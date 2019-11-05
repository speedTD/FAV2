using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestABC.Models.Data
{
    public class Role
    {
        public int ID { get; set; }
        public string RoleName { get; set; }
        public string Title { get; set; }
        public bool Checked { get; set; }
    }
    public class ReturnRole
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public int Total { get; set; }

        public Role aRole { get; set; }

        public List<Role> lstRole;
    }
    public class RoleMenus
    {
        public List<MenuRole> lstMenuRole { get; set; }
    }

}