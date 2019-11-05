using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestABC.Models.Data;

namespace TestABC.Controllers
{
    public class UserMenuController : Controller
    {
        // GET: UserMenu
        public ActionResult Index()
        {
            return View();
        }
        UserMenuDB userMenuDB = new UserMenuDB();

        public JsonResult List()
        {
            JsonResult x = Json(userMenuDB.UserAll().lstUserMenu, JsonRequestBehavior.AllowGet);

            return Json(userMenuDB.UserAll().lstUserMenu, JsonRequestBehavior.AllowGet);   
        }
        public JsonResult GetByUserID(int UserID)
        {
            //JsonResult x = Json(userMenuDB.GetByUserID(UserID).lstUserMenu, JsonRequestBehavior.AllowGet);

            return Json(userMenuDB.GetByUserID(UserID).lstUserMenu, JsonRequestBehavior.AllowGet);   
        }
        public JsonResult GetbyID(int ID)
        {
            return Json((userMenuDB.GetbyID(ID)).lstUserMenu[0], JsonRequestBehavior.AllowGet); 
        }
        public JsonResult Insert(UserMenu userMenu)
        {

            return Json(userMenuDB.Insert(userMenu), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(UserMenu userMenu)
        {
            return Json(userMenuDB.Insert(userMenu), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int ID)
        {
            return Json(userMenuDB.DeleteByID(ID), JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteByUserID(int ID)
        {
            return Json(userMenuDB.DeleteByUserID(ID), JsonRequestBehavior.AllowGet);
        }        
    }
}