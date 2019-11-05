using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestABC.Models.Data
{
    public class UserMenu
    {
        public int ID { get; set; }
        public int MenuID { get; set; }
        public int UserID { get; set; }
        public string Pemission { get; set; }
        public string MenuName { get; set; }
    }
    public class ReturnUserMenu
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public int Total { get; set; }

        public UserMenu aUserMenu { get; set; }

        public List<UserMenu> lstUserMenu;
    }
}