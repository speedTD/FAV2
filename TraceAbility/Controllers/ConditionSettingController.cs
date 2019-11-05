using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestABC.Models.Data;

namespace TestABC.Controllers
{
    public class ConditionSettingController : Controller
    {
        // GET: ConditionSetting
        public ActionResult Index()
        {
            return View();
        }
        ConditionSettingDB conditionSettingDB = new ConditionSettingDB();

        public JsonResult List()
        {
           
            return Json(conditionSettingDB.ConditionSettingAll().lstConditionSetting, JsonRequestBehavior.AllowGet);   
        }

        public JsonResult GetbyID(int ID)
        {
            return Json((conditionSettingDB.GetbyID(ID)).lstConditionSetting[0], JsonRequestBehavior.AllowGet); 
        }
        public JsonResult Insert(ConditionSetting conditionSetting)
        {
            return Json(conditionSettingDB.Insert(conditionSetting), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(ConditionSetting conditionSetting)
        {
            return Json(conditionSettingDB.Insert(conditionSetting), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int ID)
        {
            return Json(conditionSettingDB.DeleteByID(ID), JsonRequestBehavior.AllowGet);

        }
    }
}