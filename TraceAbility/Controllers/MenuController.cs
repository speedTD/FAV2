using System;
using log4net;
using System.Web.Mvc;
using TestABC.Models.Data;

namespace TestABC.Controllers
{
    public class MenuController : Controller
    {
        private static readonly ILog mylog4net = LogManager.GetLogger(typeof(ProgramPdtCtrlController));
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }
        MenuDB menuDB = new MenuDB();
        public ActionResult CreateDynamicMenu_ByUserID()
        {
            MenuViewModel menuViewModel;
            try
            {
                if (Session["MenuPermission"] == null)
                    Session["MenuPermission"] = (new MenuDB()).GetMenuViewModelForDynamicMenu_ByUserID(Convert.ToInt32(Session["UserID"].ToString()));
                menuViewModel = Session["MenuPermission"] as MenuViewModel;
            }
            catch (Exception ex)
            {
                mylog4net.Error(" CreateDynamicMenu_ByUserID ", ex);
                menuViewModel = new MenuViewModel();
            }
            return View(menuViewModel);
        }
        
        public JsonResult List()
        {           
            return Json(menuDB.MenuAll().lstMenu, JsonRequestBehavior.AllowGet);   
        }
        public JsonResult SelectTempSorted()
        {
            return Json(menuDB.SelectTempSorted().lstMenu, JsonRequestBehavior.AllowGet);   
        }
        public JsonResult GetbyID(int ID)
        {
            return Json((menuDB.GetbyID(ID)).lstMenu[0], JsonRequestBehavior.AllowGet); 
        }
        public JsonResult GetbyParentMenuID(int ParentMenuID)
        {
            return Json((menuDB.GetbyParentMenuID(ParentMenuID)).lstMenu, JsonRequestBehavior.AllowGet); 
        }
        public JsonResult Insert(Menu menu)
        {

            return Json(menuDB.Insert(menu), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(Menu menu)
        {
            return Json(menuDB.Insert(menu), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int ID)
        {
            return Json(menuDB.DeleteByID(ID), JsonRequestBehavior.AllowGet);

        }
        public JsonResult SelectByParentMenuID(int ParentMenuID)
        {
            //JsonResult x = Json(menuDB.SelectAllMenu().lstMenu, JsonRequestBehavior.AllowGet);

            return Json(menuDB.GetbyParentMenuID(ParentMenuID).lstMenu, JsonRequestBehavior.AllowGet);   
        }
        public JsonResult SelectMenuLevel2()
        {
            return Json((menuDB.SelectMenuLevel2()).lstMenu, JsonRequestBehavior.AllowGet); 
        }
        
    }
}