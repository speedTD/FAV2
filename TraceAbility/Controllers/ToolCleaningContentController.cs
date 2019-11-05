using System.Web.Mvc;
using TestABC.Models.Data;

using System.Configuration;

namespace TestABC.Controllers
{
    public class ToolCleaningContentController : Controller
    {
        // GET: ToolList
        public ActionResult ToolCleaningContent()
        {
            return View();
        }
        // GET: CustomerUsingJson
        // GET: CustomerJson
        ToolCleaningContentDB toolCleaningContentDB = new ToolCleaningContentDB();
        int pageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"]);
        // GET: Home  

        public JsonResult List()
        {
            //JsonResult x = Json(toolCleaningContentDB.GetAll().LstToolCleaningContent, JsonRequestBehavior.AllowGet);

            return Json(toolCleaningContentDB.GetAll().LstToolCleaningContent, JsonRequestBehavior.AllowGet);   
        }

        public JsonResult GetbyIDRepairDate(ToolCleaningContent toolCleaningContent)
        {
            ReturnToolCleaningContent _returnToolCleaningContentturnToolList = toolCleaningContentDB.GetbyIDRepairDate(toolCleaningContent);
            return Json(_returnToolCleaningContentturnToolList, JsonRequestBehavior.AllowGet); 
        }

        public JsonResult GetbyID(string ID)
        {
            ReturnToolCleaningContent _returnToolCleaningContentturnToolList = toolCleaningContentDB.GetbyID(ID);
            return Json(_returnToolCleaningContentturnToolList, JsonRequestBehavior.AllowGet); 
        }
        public JsonResult Insert(ToolCleaningContent toolCleaningContent)
        {
            //******* Validate *****
            bool isValid = true;
            //check toolid exist.
            ReturnToolList returnToolList = (new ToolDB().GetbyID(toolCleaningContent.ToolID));
            ReturnToolCleaningContent returnToolCleaningContent = new ReturnToolCleaningContent();
            if (returnToolList.Code == "01") //neu khong ton tai ToolID.
            {
                returnToolCleaningContent.Code = "02";  // loi nghiep vu.
                returnToolCleaningContent.Message = "ToolID doesn't exist.";
                isValid = false;
            }
            //check cleaning info exist.
            ReturnToolCleaningContent _checkToolCleaningExist = toolCleaningContentDB.GetbyIDRepairDate(toolCleaningContent);
            if(_checkToolCleaningExist.Code == "00") // neu da ton tai thong tin ban ghi. Bao loi. Ko insert duoc.
            {
                returnToolCleaningContent.Code = "02";  // loi nghiep vu.
                returnToolCleaningContent.Message = "Cleaning info exists.";
                isValid = false;
            }
            //******* End of Validate *****
            if (isValid) // thuc hien viec insert.
                returnToolCleaningContent = toolCleaningContentDB.Insert(toolCleaningContent);
             return Json(returnToolCleaningContent, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(ToolCleaningContent tool)
        {
            return Json(toolCleaningContentDB.Insert(tool), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(string ID)
        {
            return Json(toolCleaningContentDB.DeleteByID(ID), JsonRequestBehavior.AllowGet);

        }

        public JsonResult ListByPage(int pageNumber)
        {
            ReturnToolCleaningContent _returnToolCleaningContent = toolCleaningContentDB.ListbyPage(pageNumber, pageSize);
            _returnToolCleaningContent.TotalPage = (_returnToolCleaningContent.Total + pageSize - 1) / pageSize;
            _returnToolCleaningContent.TotalPage = _returnToolCleaningContent.TotalPage == 0 ? 1 : _returnToolCleaningContent.TotalPage;
            return Json(_returnToolCleaningContent, JsonRequestBehavior.AllowGet);   
        }
        public JsonResult Search(ReturnToolCleaningContent returnToolCleaningContentObj)
        {
            ReturnToolCleaningContent _returnToolCleaningContent = toolCleaningContentDB.SearchToolCleaning(returnToolCleaningContentObj, pageSize);
            _returnToolCleaningContent.TotalPage = (_returnToolCleaningContent.Total + pageSize - 1) / pageSize;
            _returnToolCleaningContent.TotalPage = _returnToolCleaningContent.TotalPage == 0 ? 1 : _returnToolCleaningContent.TotalPage;
            //JsonResult x = Json(_returnToolList, JsonRequestBehavior.AllowGet);
            //string y = new JavaScriptSerializer().Serialize(jsonResult.Data);
            //string y = Newtonsoft.Json.JsonConvert.SerializeObject(x.Data); 

            return Json(_returnToolCleaningContent, JsonRequestBehavior.AllowGet);   
        }
    }
}