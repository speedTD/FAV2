using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestABC.Models.Data;

namespace TestABC.Controllers
{
    public class ToolTypeListController : Controller
    {
        // GET: ToolTypeList
        public ActionResult Index()
        {
            return View();
        }
        ToolTypeListDB toolTypeListDB = new ToolTypeListDB();
        // GET: Home  

        public JsonResult List()
        {
            JsonResult x = Json(toolTypeListDB.ListAll().LstToolTypeList, JsonRequestBehavior.AllowGet);

            return Json(toolTypeListDB.ListAll().LstToolTypeList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetbyID(string toolTypeID)
        {
            return Json((toolTypeListDB.GetbyID(toolTypeID)).LstToolTypeList[0], JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetbyToolTypeName(string toolTypeName)
        {
            var listToolType = toolTypeListDB.GetbyToolTypeName(toolTypeName);
            if (listToolType.LstToolTypeList != null && listToolType.LstToolTypeList.Count > 0)
            {
                return Json((toolTypeListDB.GetbyToolTypeName(toolTypeName)).LstToolTypeList[0], JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(listToolType, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Insert(ToolTypeList toolType)
        {
            return Json(toolTypeListDB.Insert(toolType), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(ToolTypeList toolType)
        {
            return Json(toolTypeListDB.Insert(toolType), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(string ToolTypeID)
        {
            return Json(toolTypeListDB.DeleteByID(ToolTypeID), JsonRequestBehavior.AllowGet);
        }
    }
}