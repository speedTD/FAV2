using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestABC.Common;
using TestABC.Models.Data;

namespace TestABC.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        UserDB userDB = new UserDB();

        public JsonResult List()
        {
            return Json(userDB.UserAll(), JsonRequestBehavior.AllowGet);   
        }

        public JsonResult GetbyID(int ID)
        {
            return Json((userDB.GetbyID(ID)).lstUser[0], JsonRequestBehavior.AllowGet); 
        }
        public JsonResult GetbyFactoryID(string factoryID)
        {
            return Json((userDB.GetbyFactoryID(factoryID)), JsonRequestBehavior.AllowGet); 
        }
        public JsonResult Insert(User user)
        {
            user.PassWord = SMCommon.MD5Endcoding(user.PassWord).ToLower();
            return Json(userDB.Insert(user), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(User user)
        {
            if (user.PassWordNew != null)
            {
                user.PassWord = SMCommon.MD5Endcoding(user.PassWordNew).ToLower();
            }
            return Json(userDB.Insert(user), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int ID)
        {
            return Json(userDB.DeleteByID(ID), JsonRequestBehavior.AllowGet);

        }
        public JsonResult SelectByUserName(string userName)
        {
            return Json((userDB.SelectByUserName(userName)), JsonRequestBehavior.AllowGet); 
        }
        public JsonResult SelectDistinctUserID()
        {
            return Json(userDB.SelectDistinctUserID().lstUser, JsonRequestBehavior.AllowGet);   
        }

        public string getUserNameByID(int UserID)
        {
            return userDB.getUserNameByID(UserID);   
        }
        public JsonResult UserExceptUserMenu()
        {
            return Json(userDB.UserExceptUserMenu().lstUser, JsonRequestBehavior.AllowGet);   
        }
        public JsonResult SearchByUserName(string where)
        {
            return Json(userDB.SearchByUserName(where).lstUser, JsonRequestBehavior.AllowGet);   
        }
        public JsonResult SearchByUserNameAndPassword(string userName, string password)
        {
            var passwordMd5 = SMCommon.MD5Endcoding(password.Trim()).ToLower();
            return Json(userDB.SearchByUserNameAndPassword(userName, passwordMd5), JsonRequestBehavior.AllowGet);
        }
        public JsonResult CheckPermission_ByUserNameAndPassword(string userName, string password, string permission)
        {
            var passwordMd5 = SMCommon.MD5Endcoding(password.Trim()).ToLower();
            return Json(userDB.CheckPermission_ByUserNameAndPassword(userName, passwordMd5, permission), JsonRequestBehavior.AllowGet);
        }
    }
}