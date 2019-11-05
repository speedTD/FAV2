using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestABC.Models.Data;

namespace TestABC.Controllers
{
    public class FPBCheckingItemController : Controller
    {
        // GET: FPBCheckingItem
        public ActionResult Index()
        {
            return View();
        }
        FPBCheckingItemDB FPBCheckingItemDB = new FPBCheckingItemDB();
        // GET: Home  

        public JsonResult List()
        {
            JsonResult x = Json(FPBCheckingItemDB.ListAll().LstFPBCheckingItem, JsonRequestBehavior.AllowGet);

            return Json(FPBCheckingItemDB.ListAll().LstFPBCheckingItem, JsonRequestBehavior.AllowGet);   
        }

        public JsonResult GetbyID(string IDFPBCheckingItem)
        {
            return Json((FPBCheckingItemDB.GetbyID(IDFPBCheckingItem)).LstFPBCheckingItem[0], JsonRequestBehavior.AllowGet); 
        }
        public JsonResult GetbyMachineID(string MachineID, int FrequencyID)
        {
            return Json(FPBCheckingItemDB.GetbyMachineID(MachineID, FrequencyID).LstFPBCheckingItem, JsonRequestBehavior.AllowGet); 
        }
        public JsonResult Insert(FPBCheckingItem FPBCheckingItem)
        {
            return Json(FPBCheckingItemDB.Insert(FPBCheckingItem), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(FPBCheckingItem FPBCheckingItem)
        {
            return Json(FPBCheckingItemDB.Insert(FPBCheckingItem), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int ID)
        {
            return Json(FPBCheckingItemDB.DeleteByID(ID), JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteByMachineID(string MachineID, string FrequencyID)
        {
            return Json(FPBCheckingItemDB.DeleteByMachineID(MachineID, FrequencyID), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetFPBCheckingItemName(string MachineID)
        {
            //JsonResult x = Json(FPBCheckingItemDB.GetFPBCheckingItemName(MachineID).LstFPBCheckingItem, JsonRequestBehavior.AllowGet);
            return Json(FPBCheckingItemDB.GetFPBCheckingItemName(MachineID), JsonRequestBehavior.AllowGet);   
        }
    }
}