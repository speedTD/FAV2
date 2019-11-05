using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestABC.Models.Data;

namespace TestABC.Controllers
{
    public class UserRoleController : Controller
    {
        // GET: UserRole
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListUserRole()
        {
            return View();
        }

        public JsonResult SaveUserWithRoles(UserWithRoles userWithRoles)
        {
            ReturnUserWithRoles returnResult = new ReturnUserWithRoles();
            returnResult.Code = "00";
            //validate
            if (userWithRoles == null)
            {
                returnResult.Code = "99";
                returnResult.Message = "Không có dữ liệu/No data.";
                return Json(returnResult, JsonRequestBehavior.AllowGet);
            }
            UserRoleDB userRoleDB = new UserRoleDB();
            ReturnUserWithRoles returnUserWith_aRole;
            foreach (Role role in userWithRoles.Roles)
            {
                returnUserWith_aRole = userRoleDB.SaveUserWith_aRole(userWithRoles.UserID, role);
                if (returnUserWith_aRole.Code == "99")
                {
                    returnResult.Code = "99";
                    returnResult.Message += "Lỗi phân quyền : " + role.RoleName + "; ";
                }
            }
            return Json(returnResult, JsonRequestBehavior.AllowGet);
        }


    }
}