using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestABC.Models.Data;

namespace TestABC.Controllers
{
    public class ProgramNameController : Controller
    {
        // GET: ProgramName
        public ActionResult Index()
        {
            return View();
        }
        ProgramNameDB programName = new ProgramNameDB();
        // GET: Home 
        public JsonResult ListAll()
        {
            return Json(programName.ListAll(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult List()
        {
            return Json(programName.ListAll(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetbyID(string nameProgram)
        {
            return Json(programName.GetbyID(nameProgram).lstProgramName[0], JsonRequestBehavior.AllowGet);
        }
        public JsonResult CountbyID(string nameProgram)
        {
            return Json(programName.GetbyID(nameProgram).Total, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Insert(ProgramName p)
        {
            return Json(programName.Insert(p), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(ProgramName p)
        {
            return Json(programName.Insert(p), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(string id)
        {
            return Json(programName.DeleteByID(id), JsonRequestBehavior.AllowGet);

        }
    }
}