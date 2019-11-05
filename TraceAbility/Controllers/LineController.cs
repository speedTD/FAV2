using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestABC.Models.Data;
using System.Configuration;
using TestABC.Models.Data;
using TestABC.Common;
using System.Net;

namespace TestABC.Controllers
{
    public class LineController : Controller
    {
        // GET: Line
        public ActionResult Index()
        {
            return View();
        }
        LineDB lineDB = new LineDB();
        // GET: Home 
        public JsonResult ListAll()
        {
            
            var x = Json(lineDB.ListAll().LstLine, JsonRequestBehavior.AllowGet);
            
            return x;
        }
        public JsonResult List()
        {
            return Json(lineDB.ListAll(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetbyID(string LineID)
        {
            return Json(lineDB.GetbyID(LineID).LstLine[0], JsonRequestBehavior.AllowGet);
        }
        public JsonResult CountbyID(string LineID)
        {
            return Json(lineDB.GetbyID(LineID).Total, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Insert(Line line)
        {
            //if (ClassCommon.CheckPermission(MyShareInfo.ID, 5, "Add").Equals("00") )
            //{
                return Json(lineDB.Insert(line), JsonRequestBehavior.AllowGet);                
            //}
            //else
            //{
            //    return new JsonHttpStatusResult("Not permission!", HttpStatusCode.InternalServerError);
            //}            
        }
        public JsonResult Update(Line line)
        {
            //if (ClassCommon.CheckPermission(MyShareInfo.ID, 5, "Edit").Equals("00"))
            //{
                return Json(lineDB.Insert(line), JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    return new JsonHttpStatusResult("Not permission!", HttpStatusCode.InternalServerError);
            //}
        }
        public JsonResult Delete(string ID)
        {
            //if (ClassCommon.CheckPermission(MyShareInfo.ID, 5, "Delete").Equals("00"))
            //{
                return Json(lineDB.DeleteByID(ID), JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    return new JsonHttpStatusResult("Not permission!", HttpStatusCode.InternalServerError);
            //}
        }
       
    }
}