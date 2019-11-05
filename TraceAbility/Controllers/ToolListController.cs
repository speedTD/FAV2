using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestABC.Models.Data;

using System.Configuration;
using System.IO;
using Newtonsoft.Json;

namespace TestABC.Controllers
{
    public class ToolListController : Controller
    {
        // GET: ToolList
        public ActionResult Index()
        {
            return View();
        }
        // GET: CustomerUsingJson
        // GET: CustomerJson
        ToolDB toolDB = new ToolDB();
        int pageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"]);

        // GET: Home  

        public JsonResult List()
        {
            return Json(toolDB.ListAll().lstToolList, JsonRequestBehavior.AllowGet);  
        }

        public JsonResult GetbyID(string toolListID)
        {
            ReturnToolList _returnToolList = toolDB.GetbyID(toolListID);
            return Json(_returnToolList, JsonRequestBehavior.AllowGet);  
        }
        public JsonResult Insert()
        {
            if (Request.Form.Count == 0) return Json(new ReturnToolList(), JsonRequestBehavior.AllowGet);
            ToolList tool = Newtonsoft.Json.JsonConvert.DeserializeObject<ToolList>(Request.Form[0]);
            HttpFileCollectionBase file = Request.Files;
            if (file.Count > 0)
            {
                string[] request = new String[file.Count];
                string path = "~/Images/";
                for (int i = 0; i < file.Count; i++)
                {
                    string fileName = (string)(Path.GetFileName(file[i].FileName)).Split('.')[0];
                    string extension = Path.GetExtension(file[i].FileName);

                    fileName = fileName + DateTime.Now.ToString("yyMMddHHmmss") + extension;
                    request[i] = path + fileName;
                    fileName = Path.Combine(Server.MapPath(path), fileName);
                    file[i].SaveAs(fileName);

                }
                tool.ImageUrl = JsonConvert.SerializeObject(request);

            }
            return Json(toolDB.Insert(tool), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update()
        {
            ToolList tool = Newtonsoft.Json.JsonConvert.DeserializeObject<ToolList>(Request.Form[0]);
            HttpFileCollectionBase file = Request.Files;
            if (file.Count > 0)
            {
                string[] request = new String[file.Count];
                string path = "~/Images/";
                for (int i = 0; i < file.Count; i++)
                {
                    string fileName = (string)(Path.GetFileName(file[i].FileName)).Split('.')[0];
                    string extension = Path.GetExtension(file[i].FileName);

                    fileName = fileName + DateTime.Now.ToString("yyMMddHHmmss") + extension;
                    request[i] = path + fileName;
                    fileName = Path.Combine(Server.MapPath(path), fileName);
                    file[i].SaveAs(fileName);

                }
                tool.ImageUrl = JsonConvert.SerializeObject(request);

            }
            return Json(toolDB.Insert(tool), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(string ID)
        {
            //ReturnToolList x = toolDB.DeleteByID(ID);
            //return Json(toolDB.DeleteByID(ID), JsonRequestBehavior.AllowGet);
            return Json(toolDB.DeleteByID(ID), JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListByPage(int pageNumber)
        {
            ReturnToolList _returnToolList = toolDB.ListbyPage(pageNumber, pageSize);
            _returnToolList.TotalPage = (_returnToolList.Total + pageSize - 1) / pageSize;
            _returnToolList.TotalPage = _returnToolList.TotalPage == 0 ? 1 : _returnToolList.TotalPage;
            //JsonResult x = Json(_returnToolList, JsonRequestBehavior.AllowGet);
            //string y = new JavaScriptSerializer().Serialize(jsonResult.Data);
            //string y = Newtonsoft.Json.JsonConvert.SerializeObject(x.Data); 

            return Json(_returnToolList, JsonRequestBehavior.AllowGet);   
        }

        public JsonResult Search(ReturnToolList searchToolList)
        {
            ReturnToolList _returnToolList = toolDB.SearchTools(searchToolList, pageSize);
            _returnToolList.TotalPage = (_returnToolList.Total + pageSize - 1) / pageSize;
            _returnToolList.TotalPage = _returnToolList.TotalPage == 0 ? 1 : _returnToolList.TotalPage;
            //JsonResult x = Json(_returnToolList, JsonRequestBehavior.AllowGet);
            //string y = new JavaScriptSerializer().Serialize(jsonResult.Data);
            //string y = Newtonsoft.Json.JsonConvert.SerializeObject(x.Data); 

            return Json(_returnToolList, JsonRequestBehavior.AllowGet);   
        }
        public JsonResult SelectByCondition(string where)
        {
            return Json(toolDB.SelectByCondition(where).lstToolList, JsonRequestBehavior.AllowGet);
        }

    }
}