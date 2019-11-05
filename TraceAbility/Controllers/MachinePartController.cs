using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestABC.Models.Data;


namespace TestABC.Controllers
{
    public class MachinePartController : Controller
    {
        // GET: MachinePart
        public ActionResult Index()
        {
            return View();
        }
        MachinePartDB machinePartDB = new MachinePartDB();

        public JsonResult List()
        {
            return Json(machinePartDB.MachinePartAll().lstMachinePart, JsonRequestBehavior.AllowGet);   
        }

        public JsonResult GetbyID(int ID)
        {
            return Json((machinePartDB.GetbyID(ID)).lstMachinePart[0], JsonRequestBehavior.AllowGet); 
        }
        public JsonResult GetbyMachineType(int machineTypeID)
        {
            return Json((machinePartDB.GetbyMachineType(machineTypeID)), JsonRequestBehavior.AllowGet); 
        }
        public JsonResult Insert(MachinePart machinePart)
        {
            return Json(machinePartDB.Insert(machinePart), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(MachinePart machinePart)
        {
            return Json(machinePartDB.Insert(machinePart), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int ID)
        {
            return Json(machinePartDB.DeleteByID(ID), JsonRequestBehavior.AllowGet);

        }
    }
}