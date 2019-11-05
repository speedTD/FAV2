using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestABC.Models.Data;

namespace TestABC.Controllers
{
    public class MachineTypeController : Controller
    {
        // GET: MachineType
        public ActionResult Index()
        {
            return View();
        }
        MachineTypeDB machineTypeDB = new MachineTypeDB();

        public JsonResult List()
        {
            return Json(machineTypeDB.MachineTypeAll().lstMachineType, JsonRequestBehavior.AllowGet);   
        }

        public JsonResult GetbyID(int ID)
        {
            return Json((machineTypeDB.GetbyID(ID)).lstMachineType[0], JsonRequestBehavior.AllowGet); 
        }
        public JsonResult CountbyTypeName(string TypeName)
        {
            return Json((machineTypeDB.GetbyTypeName(TypeName)).Total, JsonRequestBehavior.AllowGet); 
        }
        public JsonResult Insert(MachineType machineType)
        {
            return Json(machineTypeDB.Insert(machineType), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(MachineType machineType)
        {
            return Json(machineTypeDB.Insert(machineType), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int ID)
        {
            return Json(machineTypeDB.DeleteByID(ID), JsonRequestBehavior.AllowGet);

        }
    }
}