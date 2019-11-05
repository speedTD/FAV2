using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestABC.Models.Data;
using TestABC.Models.Data;

namespace TestABC.Controllers
{
    public class MachineMtnWeeklyController : Controller
    {
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
        MachineMtnDB machineMtnDB = new MachineMtnDB();
        MachineMtnContentListDB machineMtnContentListDB = new MachineMtnContentListDB();
        MachineMtnDetailDB machineMtnDetailDB = new MachineMtnDetailDB();

        public JsonResult InsertMachineMtn(MachineMtn machineMtn)
        {
            return Json(machineMtnDB.Insert(machineMtn), JsonRequestBehavior.AllowGet);
        }
        public JsonResult SelectByCondition(string MachineID, string FrequencyID, string MaintenanceDate, string Shift, string OperatorID, string Month, string Year)
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
                    count = 1;
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
                    count = 1;
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
                    count = 1;
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
                    count = 1;
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
                    count = 1;
                }
                else
                {
                    where += " AND Year = '" + Year + "' ";
                }
            }
            JsonResult x = Json(machineMtnDB.SelectByCondition(where), JsonRequestBehavior.AllowGet);
            return Json(machineMtnDB.SelectByCondition(where), JsonRequestBehavior.AllowGet);   
        }
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
        public JsonResult GetMachineMtnDetail(int MachineMtnID)
        {
            JsonResult x = Json(machineMtnDetailDB.SelectByMachineMtnID(MachineMtnID).lstMachineMtnContentDetail, JsonRequestBehavior.AllowGet);
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
    }
}