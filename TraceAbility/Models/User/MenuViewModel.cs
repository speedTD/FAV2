using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestABC.Models.Data
{
    public class MenuViewModel
    {
        public ReturnMenuRole returnMenuRole { get; set; }
        public User user { get; set; }
    }
    public class PermisionControllerVM
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public bool isAllow_View { get; set; }
        public bool isAllow_Add { get; set; }
        public bool isAllow_Edit { get; set; }
        public bool isAllow_Delete { get; set; }
        public bool isAllow_Check { get; set; }
    }
    
}