using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestABC.Models.Data;

namespace TestABC.Controllers
{
    public class RoleController : Controller
    {
        // GET: Role
        public ActionResult Index()
        {
            return View();
        }
        RoleDB roleDB = new RoleDB();

        public JsonResult List()
        {
            return Json(roleDB.RoleAll(), JsonRequestBehavior.AllowGet);   
        }
        
        public JsonResult ListByUserID(int userID)
        {

            return Json(roleDB.ListByUserID(userID), JsonRequestBehavior.AllowGet);   
        }
        public JsonResult GetbyID(int ID)
        {
            return Json((roleDB.GetbyID(ID)).lstRole[0], JsonRequestBehavior.AllowGet); 
        }
        public JsonResult Insert(Role role)
        {
            return Json(roleDB.Insert(role), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(Role role)
        {
            return Json(roleDB.Insert(role), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int ID)
        {
            return Json(roleDB.DeleteByID(ID), JsonRequestBehavior.AllowGet);
        }

        #region Role Menu (permission)
        public ActionResult ListAllMenuWithRolePermission()
        {
            return View();
        }
        //Get all menus (include Role's permission)
        public JsonResult GetFullMenuIncludeRolePermission(int roleID)
        {
            MenuDB menuDB = new MenuDB();
            //ReturnMenuRole x = menuDB.GetListAllMenuWithRolePermission(roleID);
            return Json(menuDB.GetListAllMenuWithRolePermission(roleID), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMenusByUserID(User user)
        {
            MenuDB menuDB = new MenuDB();
            return Json(menuDB.GetMenusByUserID(user), JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveRoleWithPermissionMenus(RoleMenus roleWithPermissionMenus)
        {
            ReturnMenuRole returnResult = new ReturnMenuRole();
            returnResult.Code = "00";
            //validate
            if (roleWithPermissionMenus.lstMenuRole == null)
            {
                returnResult.Code = "99";
                returnResult.Message = "Không có dữ liệu/No data.";
                return Json(returnResult, JsonRequestBehavior.AllowGet);
            }
            RoleDB roleDB = new RoleDB();
            ReturnMenuRole returnRoleWithPermissionMenus;
            foreach (MenuRole menuRole in roleWithPermissionMenus.lstMenuRole)
            {
                returnRoleWithPermissionMenus = roleDB.SaveRoleWithPermissionMenus(menuRole);
                if (returnRoleWithPermissionMenus.Code == "99")
                {
                    returnResult.Code = "99";
                    returnResult.Message += "Lỗi phân quyền : " + menuRole.MenuName + "; ";
                }
            }
            return Json(returnResult, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}