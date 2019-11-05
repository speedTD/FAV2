using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestABC.Models.Data
{
    public class User
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string PassWordNew { get; set; }
        public string MobileNumber { get; set; }
        public string FactoryID { get; set; }
        public int isActive { get; set; }
        public int RoleID { get; set; }
    }
    public class ReturnUser
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public int Total { get; set; }

        public User aUser { get; set; }

        public List<User> lstUser;
    }
    public class UserPermission
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public int MenuID { get; set; }
        public string ControllerName { get; set; }
        public string Permission { get; set; }
    }
    public class ReturnUserPermission
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public int Total { get; set; }

        public UserPermission aUserPermission { get; set; }

        public List<UserPermission> lstUserPermission;
    }

    public class Notification
    {
        public string Message { get; set; }
    }
}