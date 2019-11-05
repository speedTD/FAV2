using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestABC.Models.Data;
using TestABC.Models.Data;
using log4net;
namespace TestABC.Controllers
{
    public class MachineMtnDailyController : Controller
    {
        // GET: MachineMtnDaily
        public ActionResult Index()
        {
            //< !--store userLogin Value -->
            ViewBag.UserLogin = (User)Session["UserLogin"];
            return View();
        }
        MachineMtnDB machineMtnDB = new MachineMtnDB();

        MachineMtnContentListDB machineMtnContentListDB = new MachineMtnContentListDB();
        MachineMtnDetailDB machineMtnDetailDB = new MachineMtnDetailDB();

        public JsonResult InsertMachineMtn(MachineMtn machineMtn)
        {
            return Json(machineMtnDB.Insert(machineMtn), JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateCheckerResult_ByMachineMtnID(MachineMtn machineMtn)
        {
            return Json(machineMtnDB.UpdateCheckerResult_ByMachineMtnID(machineMtn), JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateMachineMtn(MachineMtn machineMtn)
        {
            return Json(machineMtnDB.Insert(machineMtn), JsonRequestBehavior.AllowGet);
        }
        //public JsonResult SelectByCondition(string MachineID, string FrequencyID, string MaintenanceDate, string Shift, string OperatorID, string Month, string Year)
        //{
        //    string where = "1=1";
        //    int count = 0;
        //    if (!string.IsNullOrEmpty(MachineID))
        //    {
        //        count = 1;
        //        where = "tMachineMtn.MachineID = '" + MachineID + "'";
        //    }
        //    if (!string.IsNullOrEmpty(FrequencyID))
        //    {
        //        if (count == 0)
        //        {
        //            where = "tMachineMtn.FrequencyID = '" + FrequencyID + "' ";
        //            count = 1;
        //        }
        //        else
        //        {
        //            where += " AND tMachineMtn.FrequencyID = '" + FrequencyID + "' ";
        //        }
        //    }
        //    if (!string.IsNullOrEmpty(MaintenanceDate))
        //    {
        //        if (count == 0)
        //        {
        //            where = "tMachineMtn.MaintenanceDate = '" + MaintenanceDate + "' ";
        //            count = 1;
        //        }
        //        else
        //        {
        //            where += " AND tMachineMtn.MaintenanceDate = '" + MaintenanceDate + "' ";
        //        }
        //    }
        //    if (!string.IsNullOrEmpty(Shift))
        //    {
        //        if (count == 0)
        //        {
        //            where = "tMachineMtn.Shift = '" + Shift + "' ";
        //            count = 1;
        //        }
        //        else
        //        {
        //            where += " AND tMachineMtn.Shift = '" + Shift + "' ";
        //        }
        //    }
        //    if (!string.IsNullOrEmpty(Month))
        //    {
        //        if (count == 0)
        //        {
        //            where = "tMachineMtn.Month = '" + Month + "' ";
        //            count = 1;
        //        }
        //        else
        //        {
        //            where += " AND tMachineMtn.Month = '" + Month + "' ";
        //        }
        //    }
        //    if (!string.IsNullOrEmpty(Year))
        //    {
        //        if (count == 0)
        //        {
        //            where = "tMachineMtn.Year = '" + Year + "' ";
        //            count = 1;
        //        }
        //        else
        //        {
        //            where += " AND tMachineMtn.Year = '" + Year + "' ";
        //        }
        //    }
        //    return Json(machineMtnDB.SelectByCondition(where), JsonRequestBehavior.AllowGet);   
        //}
        public JsonResult Delete(int ID)
        {
            return Json(machineMtnDB.DeleteByID(ID), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Insert()
        {
            return View();
        }
        public JsonResult InsertMachineMtnDetail(MachineMtnDetail machineMtnDetail)
        {
            return Json(machineMtnDetailDB.Insert(machineMtnDetail), JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateMachineMtnDetail(MachineMtnDetail machineMtnDetail)
        {
            return Json(machineMtnDetailDB.Insert(machineMtnDetail), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMachineMtnDetail(int MachineMtnID)
        {
             return Json(machineMtnDetailDB.SelectByMachineMtnID(MachineMtnID).lstMachineMtnContentDetail, JsonRequestBehavior.AllowGet);  
        }
        public JsonResult CheckMachineMtnData(string MachineID, string FrequencyID, string MaintenanceDate, string Shift, string Month, string Year)
        {
            string where = "1=1";
            int count = 0;
            if (!string.IsNullOrEmpty(MachineID))
            {
                count = 1;
                where = "MachineID = '" + MachineID + "'";
            }
            if (!string.IsNullOrEmpty(FrequencyID))
            {
                if (count == 0)
                {
                    where = "FrequencyID = '" + FrequencyID + "' ";
                }
                else
                {
                    where += " AND FrequencyID = '" + FrequencyID + "' ";
                }
            }
            if (!string.IsNullOrEmpty(MaintenanceDate))
            {
                if (count == 0)
                {
                    where = "MaintenanceDate = '" + MaintenanceDate + "' ";
                }
                else
                {
                    where += " AND MaintenanceDate = '" + MaintenanceDate + "' ";
                }
            }
            if (!string.IsNullOrEmpty(Shift))
            {
                if (count == 0)
                {
                    where = "Shift = '" + Shift + "' ";
                }
                else
                {
                    where += " AND Shift = '" + Shift + "' ";
                }
            }
            if (!string.IsNullOrEmpty(Month))
            {
                if (count == 0)
                {
                    where = "Month = '" + Month + "' ";
                }
                else
                {
                    where += " AND Month = '" + Month + "' ";
                }
            }
            if (!string.IsNullOrEmpty(Year))
            {
                if (count == 0)
                {
                    where = "Year = '" + Year + "' ";
                }
                else
                {
                    where += " AND Year = '" + Year + "' ";
                }
            }
            // JsonResult x = Json(machineMtnDB.SelectByCondition(where).lstMachineMtn, JsonRequestBehavior.AllowGet);
            return Json(machineMtnDB.SelectByCondition(where), JsonRequestBehavior.AllowGet);   
        }
        public ActionResult Edit()
        {
            return View();
        }
        public JsonResult GetMachineMtnbyID(int MachineMtnID)
        {
            return Json((machineMtnDB.GetMachineMtnbyID(MachineMtnID)), JsonRequestBehavior.AllowGet);
        }
        public ActionResult IndexMonth()
        {
            return View();
        }
        public ActionResult InsertMonth()
        {
            return View();
        }
        public ActionResult EditMonth()
        {
            return View();
        }
        public ActionResult IndexYear()
        {
            return View();
        }
        public ActionResult InsertYear()
        {
            return View();
        }
        public ActionResult EditYear()
        {
            return View();
        }
        public ActionResult Index3Month()
        {
            return View();
        }
        public ActionResult Insert3Month()
        {
            return View();
        }
        public ActionResult Edit3Month()
        {
            return View();
        }
        public ActionResult Index6Month()
        {
            return View();
        }
        public ActionResult Insert6Month()
        {
            return View();
        }
        public ActionResult Edit6Month()
        {
            return View();
        }
        public ActionResult IndexWeek()
        {
            return View();
        }
        public ActionResult InsertWeek()
        {
            return View();
        }
        public ActionResult EditWeek()
        {
            return View();
        }
    }
}