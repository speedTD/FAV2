using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestABC.Models.Data
{
    public class Menu
    {
        public int ID { get; set; }
        public string MenuName { get; set; }
        public string DisplayName { get; set; }
        public int ParentMenuID { get; set; }
        public int isActive { get; set; }
        public string UrlLink { get; set; }
    }
    public class ReturnMenu
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public int Total { get; set; }

        public Menu aMenu { get; set; }

        public List<Menu> lstMenu;
    }

    
    public class MenuRole
    {
        public int ID { get; set; }
        public string MenuName { get; set; }
        public string DisplayName { get; set; }
        public int ParentMenuID { get; set; }
        public int isActive { get; set; }
        public string UrlLink { get; set; }

        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string Permission { get; set; }
    }

    public class ReturnMenuRole
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public int Total { get; set; }

        public MenuRole aMenuRole { get; set; }

        public List<MenuRole> lstMenuRole;
    }
}