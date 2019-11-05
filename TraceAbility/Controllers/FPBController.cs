using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestABC.Models.Data;

namespace TestABC.Controllers
{
    public class FPBController : Controller
    {
        // GET: FPB
        public ActionResult Index()
        {
            return View();
        }
        FPBDB FPBDB = new FPBDB();
        // GET: Home  

        public JsonResult List()
        {
            JsonResult x = Json(FPBDB.ListAll(), JsonRequestBehavior.AllowGet);

            return Json(FPBDB.ListAll(), JsonRequestBehavior.AllowGet);   
        }

        public JsonResult GetbyID(string ID)
        {
            return Json((FPBDB.GetbyID(ID)).LstFPB[0], JsonRequestBehavior.AllowGet); 
        }
        public JsonResult Insert(FPB FPB)
        {
            return Json(FPBDB.Insert(FPB), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(FPB FPB)
        {
            return Json(FPBDB.Insert(FPB), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int ID)
        {
            return Json(FPBDB.DeleteByID(ID), JsonRequestBehavior.AllowGet);
        }
    }
}