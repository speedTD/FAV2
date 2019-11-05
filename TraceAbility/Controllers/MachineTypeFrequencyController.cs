using System.Web.Mvc;
using TestABC.Models.Data;
using TestABC.Models.Data;

namespace TestABC.Controllers
{
    public class MachineTypeFrequencyController : Controller
    {
        // GET: MachineTypeFrequency
        public ActionResult Index()
        {
            return View();
        }
        MachineTypeFrequencyDB machineTypeFrequencyDB = new MachineTypeFrequencyDB();

        public JsonResult List()
        {
            JsonResult x = Json(machineTypeFrequencyDB.MachineTypeFrequencyAll().lstMachineTypeFrequencyView, JsonRequestBehavior.AllowGet);

            return Json(machineTypeFrequencyDB.MachineTypeFrequencyAll().lstMachineTypeFrequencyView, JsonRequestBehavior.AllowGet);   
        }
        public JsonResult GetbyMachineTypeID(int MachineTypeID)
        {
            return Json((machineTypeFrequencyDB.GetbyMachineTypeID(MachineTypeID)).lstMachineTypeFrequency, JsonRequestBehavior.AllowGet); 
        }
        public JsonResult CountbyMachineTypeID(int MachineTypeID)
        {
            return Json((machineTypeFrequencyDB.GetbyMachineTypeID(MachineTypeID)).Total, JsonRequestBehavior.AllowGet); 
        }
        public JsonResult Insert(MachineTypeFrequency machineTypeFrequency)
        {
            return Json(machineTypeFrequencyDB.Insert(machineTypeFrequency), JsonRequestBehavior.AllowGet);
        }       
       
        public JsonResult Delete(int ID)
        {
            return Json(machineTypeFrequencyDB.DeleteByMachineTypeID(ID), JsonRequestBehavior.AllowGet);
        }
    }
}