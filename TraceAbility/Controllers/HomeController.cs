using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestABC.Common;
using TestABC.Models.Data;


namespace TestABC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult Login()
        {
            //#region test nhanh, ko qua dang nhap.

            ////
            //Session["UserID"] = 1;
            //Session["UserName"] = "admin";
            //Session["FactoryID"] = "F4";

            //MenuDB menuDB = new MenuDB();
            //User currentUser = new User() { ID = Convert.ToInt32(Session["UserID"].ToString()) };
            //ReturnMenuRole returnMenuRole = menuDB.GetMenusByUserID(currentUser);
            //var menuViewModel = new MenuViewModel
            //{
            //    returnMenuRole = returnMenuRole,
            //    user = currentUser
            //};
            //Session["MenuPermission"] = menuViewModel;

            ////Permission
            //Session["UserPermission"] = (new UserDB()).ListAllControllerName_PermissionByUserID(1);
            //return RedirectToAction("Report", "MachineMtnReportData", new { area = "" });

            //#endregion

            //---------------------------------------
            return View();
        }
        public ActionResult CheckLogin(string userid, string password)
        {
            //---------------------------------------------------------------------
            if (Request.HttpMethod == "GET")
            {
                return RedirectToAction("Login");
            }
            //check validation.
            string errorNotify = "";
            if (String.IsNullOrEmpty(userid))
                errorNotify = " Nhập tên đăng nhập/Input UserName.";
            if (String.IsNullOrEmpty(password))
                errorNotify += " Nhập mật khẩu/Input password.";
            if (!String.IsNullOrEmpty(errorNotify))
            {
                ViewBag.error = errorNotify;
                return RedirectToAction("Login");
            }

            var passwordMd5 = SMCommon.MD5Endcoding(password.Trim()).ToLower();
           ReturnUser returnUser = (new UserDB()).CheckLogin(userid.Trim(), passwordMd5);
            if (returnUser.Code == "01")
                errorNotify += " Tên đăng nhập hoặc mật khẩu không đúng/UserName or Password is incorrect!";
            if (returnUser.Code == "99")
                errorNotify += " Kiểm tra lại đường truyền/Check connection.";
            if (!String.IsNullOrEmpty(errorNotify))
            {
                ViewBag.error = " Lỗi đăng nhập/Error Login: " + errorNotify;
                return RedirectToAction("Login");
            }

            //Validation is successful.
            if (returnUser.Code == "00") // exist user.
            {
                User user = returnUser.lstUser[0];
                MyShareInfo.ID = user.ID;
                MyShareInfo.UserName = user.UserName;
                MyShareInfo.PassWord = user.PassWord;
                MyShareInfo.FullName = user.FullName;
                MyShareInfo.MobileNumber = user.MobileNumber;
                MyShareInfo.FactoryID = user.FactoryID;
                MyShareInfo.RoleID = user.RoleID;
                //Session["UserLogin"] = user;

                Session["UserID"] = user.ID;
                Session["UserName"] = user.UserName;
                Session["FactoryID"] = user.FactoryID;
                
                #region dynamic menu by userid
                MenuDB menuDB = new MenuDB();
                User currentUser = new User() { ID = Convert.ToInt32(Session["UserID"].ToString()) };
                ReturnMenuRole returnMenuRole = menuDB.GetMenusByUserID(currentUser);
                var menuViewModel = new MenuViewModel
                {
                    returnMenuRole = returnMenuRole,
                    user = currentUser
                };
                Session["MenuPermission"] = menuViewModel;
                #endregion
                //Permission
                Session["UserPermission"] = (new UserDB()).ListAllControllerName_PermissionByUserID(user.ID);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Login");
            
        }
        public ActionResult Logout()
        {
            //Session["UserLogin"] = null;
            Session["UserID"] = null;
            Session["UserName"] = null;
            Session["FactoryID"] = null;
            Session["MenuPermission"] = null;
            return RedirectToAction("Login");
        }
       
        public ActionResult NotifyInfo(Notification notification)
        {
            return View(notification);
        }
    }
}