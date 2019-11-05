using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestABC.Models.Data;
using System.IO;

namespace TestABC.Controllers
{
    public class ToolIQCController : Controller
    {
        // GET: ToolIQC
        public ActionResult Index()
        {
            //< !--store userLogin Value -->
            //ViewBag.UserLogin = (User)Session["UserLogin"];
            return View();
        }
        ToolIQCDB toolIQCDB = new ToolIQCDB();
        // GET: Home 
        public JsonResult ListAll()
        {
            return Json(toolIQCDB.ListAll().lstToolIQC, JsonRequestBehavior.AllowGet);
        }
        public JsonResult List()
        {
            return Json(toolIQCDB.ListAll(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetbyID(string id)
        {
            return Json(toolIQCDB.GetbyID(id).lstToolIQC[0], JsonRequestBehavior.AllowGet);
        }
        public JsonResult CountbyFileName(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            return Json(toolIQCDB.CountByFileName(filename).Total, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Insert()
        {
            if (Request.Form.Count == 0) return Json(new ToolIQC(), JsonRequestBehavior.AllowGet);
            ToolIQC toolIQC = Newtonsoft.Json.JsonConvert.DeserializeObject<ToolIQC>(Request.Form[0]);

            HttpFileCollectionBase file = Request.Files;
            if (file.Count > 0)
            {
                string[] lstFileUrl = new String[file.Count];
                string path = "~/Images/ToolIQC/";
                for (int i = 0; i < file.Count; i++)
                {
                    string fileName = (string)(Path.GetFileName(file[i].FileName)).Split('.')[0];
                    string extension = Path.GetExtension(file[i].FileName);

                    fileName = fileName + extension;
                    lstFileUrl[i] = path + fileName;
                    fileName = Path.Combine(Server.MapPath(path), fileName);
                    file[i].SaveAs(fileName);
                }
                toolIQC.FileUrl = lstFileUrl[0];
            }

            return Json(toolIQCDB.Insert(toolIQC), JsonRequestBehavior.AllowGet);

        }
        public JsonResult Update(ToolIQC toolIQC)
        {
            HttpFileCollectionBase file = Request.Files;
            if (file.Count > 0)
            {
                string[] lstFileUrl = new String[file.Count];
                string path = "~/Images/ToolIQC/";
                for (int i = 0; i < file.Count; i++)
                {
                    string fileName = (string)(Path.GetFileName(file[i].FileName)).Split('.')[0];
                    string extension = Path.GetExtension(file[i].FileName);

                    fileName = fileName + extension;
                    lstFileUrl[i] = path + fileName;
                    fileName = Path.Combine(Server.MapPath(path), fileName);
                    file[i].SaveAs(fileName);
                }
                toolIQC.FileUrl = lstFileUrl[0];
            }
            return Json(toolIQCDB.Insert(toolIQC), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int id)
        {
            return Json(toolIQCDB.DeleteByID(id), JsonRequestBehavior.AllowGet);

        }
    }
}