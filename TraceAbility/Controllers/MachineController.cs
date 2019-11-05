using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TestABC.Models.Data;

namespace TestABC.Controllers
{
    public class MachineController : Controller
    {
        public ActionResult Machine()
        {

            //IEnumerable<Machine> lst = new List<Machine>() {
            //        new Machine() { MachineID = "01", MachineName = "parent01", Area = "" },
            //        new Machine() { MachineID = "02", MachineName = "parent02", Area = ""  },
            //        new Machine() { MachineID = "03", MachineName = "child01.1", Area = "01"  },
            //        new Machine() { MachineID = "04", MachineName = "child01.2", Area = "01"  },
            //        new Machine() { MachineID = "05", MachineName = "child02.1", Area = "02"  },
            //        new Machine() { MachineID = "06", MachineName = "child03", Area = "03"  }
            //};

            //string strKetQua = "";

            //IEnumerable<Machine> parentMenus = lst.Where(s => s.Area == "");
            //foreach (var parentMenu in parentMenus)
            //{
            //    strKetQua += parentMenu.MachineName + "\n";
            //    var childMenus = lst.Where(s => s.Area == parentMenu.MachineID);
            //    foreach (var oneChildMenu in childMenus)
            //    {
            //        strKetQua += oneChildMenu.MachineName + "\n";
            //    }
            //}

            return View();
        }

        MachineDB machineDB = new MachineDB();

        public JsonResult List()
        {
            return Json(machineDB.MachineAll().lstMachine, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetbyID(string machineID)
        {
            var obj = (machineDB.GetbyID(machineID));
            if (obj.lstMachine != null && obj.lstMachine.Count > 0)
                return Json((machineDB.GetbyID(machineID)).lstMachine[0], JsonRequestBehavior.AllowGet);
            else
            {
                obj.lstMachine = new List<Machine>();
                return Json(obj.lstMachine, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetbyID_ReturnMachineResult(string machineID)
        {
            return Json((machineDB.GetbyID(machineID)), JsonRequestBehavior.AllowGet);
        }
        public JsonResult CountbyID(string machineID)
        {
            return Json((machineDB.GetbyID(machineID)).Total, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CountbyLineID(string LineID)
        {
            return Json(machineDB.CountbyLineID(LineID), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetbyMachineTypeID(int machineTypeID)
        {
            return Json((machineDB.GetbyMachineTypeID(machineTypeID)), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Insert(Machine machine)
        {
            return Json(machineDB.Insert(machine), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(Machine machine)
        {
            return Json(machineDB.Insert(machine), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(string ID)
        {
            return Json(machineDB.DeleteByID(ID), JsonRequestBehavior.AllowGet);
        }

    }
}