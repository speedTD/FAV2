using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Mvc;
using TestABC.Models.FPBChecking;
using TestABC.Models.Data;

namespace TestABC.Controllers
{
    public class FPBCheckingController : Controller
    {
        // GET: FPBChecking
        PermisionControllerVM permisionVM;
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit()
        {
           
            permisionVM = this.getPermisionControllerViewModel();
            if (!(permisionVM.isAllow_View || permisionVM.isAllow_Edit))
                return RedirectToAction("NotifyInfo", "Home", new { Message = "Không được cập quyền/Not permission!" });
            return View(permisionVM);

        }
        public ActionResult Add()
        {
            return View();
        }
        FPBCheckingDB FPBCheckingDB = new FPBCheckingDB();
        FPBCheckingDetailDB FPBCheckingDetailDB = new FPBCheckingDetailDB();
        int pageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"]);
        public JsonResult Insert(FPBChecking fbaChecking)
        {
            return Json(FPBCheckingDB.Insert(fbaChecking), JsonRequestBehavior.AllowGet);
        }
        //public JsonResult InsertFPBCheckingDetail()
        //{
        //    FPBCheckingDetail FPBCheckingDetail = Newtonsoft.Json.JsonConvert.DeserializeObject<FPBCheckingDetail>(Request.Form[0]);
        //    HttpFileCollectionBase httpFiles = Request.Files;
        //    if (httpFiles.Count > 0)
        //    {
        //        string[] request = new String[httpFiles.Count];
        //        string path = "~/Images/FPBCheckingIMG/";
        //        for (int i = 0; i < httpFiles.Count; i++)
        //        {
        //            string fileName = (string)(Path.GetFileName(httpFiles[i].FileName)).Split('.')[0];
        //            string extension = Path.GetExtension(httpFiles[i].FileName);

        //            fileName = fileName + DateTime.Now.ToString("yyMMddHHmmss") + extension;
        //            request[i] = path + fileName;
        //            fileName = Path.Combine(Server.MapPath(path), fileName);
        //            httpFiles[i].SaveAs(fileName);
        //        }
        //        FPBCheckingDetail.Images = JsonConvert.SerializeObject(request);
        //    }
        //    return Json(FPBCheckingDetailDB.Insert(FPBCheckingDetail), JsonRequestBehavior.AllowGet);
        //}
        public JsonResult InsertFPBCheckingDetail()
        {
            FPBCheckingDetail FPBCheckingDetail = Newtonsoft.Json.JsonConvert.DeserializeObject<FPBCheckingDetail>(Request.Form[0]);
            HttpFileCollectionBase httpFiles = Request.Files;
            ReturnFPBCheckingDetail returnFPBCheckingDetail = new ReturnFPBCheckingDetail();
            returnFPBCheckingDetail.Code = "00";
            returnFPBCheckingDetail.Message = "Cập nhật dữ liệu thành công.";
            if (httpFiles.Count > 0)
            {
                string[] request = new String[httpFiles.Count];
                string path = "~/Images/FPBCheckingIMG/";
                for (int i = 0; i < httpFiles.Count; i++)
                {
                    string fileName = (string)(Path.GetFileName(httpFiles[i].FileName)).Split('.')[0];
                    string extension = Path.GetExtension(httpFiles[i].FileName);

                    fileName = fileName + DateTime.Now.ToString("yyMMddHHmmss") + extension;
                    request[i] = path + fileName;
                    fileName = Path.Combine(Server.MapPath(path), fileName);
                    httpFiles[i].SaveAs(fileName);
                    FPBCheckingDetail.Images = request[i];
                    returnFPBCheckingDetail= FPBCheckingDetailDB.Insert(FPBCheckingDetail);                   
                }
                //FPBCheckingDetail.Images = JsonConvert.SerializeObject(request);
            }
            return Json(returnFPBCheckingDetail, JsonRequestBehavior.AllowGet);
        }
        public JsonResult List()
        {
            return Json(FPBCheckingDB.ListAll(), JsonRequestBehavior.AllowGet);   
        }
        public JsonResult Delete(int ID)
        {
            return Json(FPBCheckingDB.DeleteByID(ID), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetbyID(string ID)
        {
            return Json((FPBCheckingDB.GetbyID(ID)), JsonRequestBehavior.AllowGet); 
        }
        //public JsonResult GetByFPBCheckingID(int FPBCheckingID)
        //{
        //    return Json(FPBCheckingDetailDB.SelectByFPBCheckingID(FPBCheckingID), JsonRequestBehavior.AllowGet);   
        //}
        public JsonResult LoadFPBCheckingDetail_ByFPBCheckingID(int FPBCheckingID)
        {

            ReturnFPBCheckingDetail _returnFPBCheckingDetail = FPBCheckingDetailDB.LoadFPBCheckingDetail_ByFPBCheckingID(FPBCheckingID);
            _returnFPBCheckingDetail.permisionControllerVM = this.getPermisionControllerViewModel(); //permission

            return Json(_returnFPBCheckingDetail, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Search(ReturnFPBChecking searchFPBChecking)
        {
            ReturnFPBChecking _returnFPBChecking = FPBCheckingDB.SearchFPBChecking(searchFPBChecking, pageSize);
            _returnFPBChecking.TotalPage = (_returnFPBChecking.Total + pageSize - 1) / pageSize;
            _returnFPBChecking.TotalPage = _returnFPBChecking.TotalPage == 0 ? 1 : _returnFPBChecking.TotalPage;
            return Json(_returnFPBChecking, JsonRequestBehavior.AllowGet);   
        }
        public JsonResult FPBCheckingDetail_DeletebyID(int ID)
        {
            return Json(FPBCheckingDetailDB.DeleteByID(ID), JsonRequestBehavior.AllowGet);
        }
        private PermisionControllerVM getPermisionControllerViewModel()
        {
            return SMCommon.getPermisionControllerViewModel(RouteData.Values["controller"].ToString(), (Session["UserPermission"] as ReturnUserPermission));
        }
    }

}