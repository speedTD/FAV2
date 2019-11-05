using DataTables.Mvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestABC.Models.Data;
using System.Linq.Dynamic;

namespace TestABC.Controllers
{
    public class ProgramPdtCtrlHistoryController : Controller
    {
        // GET: ProgramPdtCtrlHistory
        public ActionResult Index()
        {
            return View();
        }
        
        ProgramPdtCtrlHistoryDB programPdtCtrlHistory = new ProgramPdtCtrlHistoryDB();
        int pageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"]);

        public JsonResult GetbyKey(ProgramPdtCtrlHistory p)
        {
            return Json(programPdtCtrlHistory.GetbyKey(p), JsonRequestBehavior.AllowGet);
        }
        //public JsonResult Search(ReturnProgramPdtCtrlHistory searchProgrampdtCtrl)
        //{
        //    ReturnProgramPdtCtrlHistory _returnProgramPdtCtrlHistory = programPdtCtrlHistory.SearchProgramPdtCtrlHistory(searchProgrampdtCtrl, pageSize);
        //    _returnProgramPdtCtrlHistory.TotalPage = (_returnProgramPdtCtrlHistory.Total + pageSize - 1) / pageSize;
        //    _returnProgramPdtCtrlHistory.TotalPage = _returnProgramPdtCtrlHistory.TotalPage == 0 ? 1 : _returnProgramPdtCtrlHistory.TotalPage;
        //    //JsonResult x = Json(_returnToolList, JsonRequestBehavior.AllowGet);
        //    //string y = new JavaScriptSerializer().Serialize(jsonResult.Data);
        //    //string y = Newtonsoft.Json.JsonConvert.SerializeObject(x.Data); 

        //    return Json(_returnProgramPdtCtrlHistory, JsonRequestBehavior.AllowGet);
        //}
        public JsonResult Search([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, ProgramPdtCtrlHistory searchProgrampdtCtrl)   
        {
            //DataTables.
            int pageSize = requestModel.Length != 0 ? requestModel.Length : 10;
            int pageNumber = requestModel.Start / requestModel.Length + 1;



            //Execute.            
            ReturnProgramPdtCtrlHistory _returnProgramPdtCtrlHistory = programPdtCtrlHistory.SearchProgramPdtCtrlHistory(searchProgrampdtCtrl, pageNumber, pageSize);

            // DataTables.
            // Avoid from datatables.net getting stuck (if data=null).
            if (_returnProgramPdtCtrlHistory.lstProgramPdtCtrlHistory == null)
                _returnProgramPdtCtrlHistory.lstProgramPdtCtrlHistory = new List<ProgramPdtCtrlHistory>();
            //Sorting
            var sortedColumns = requestModel.Columns.GetSortedColumns();
            var orderByString = String.Empty;
            foreach (var column in sortedColumns)
            {
                orderByString += orderByString != String.Empty ? "," : "";
                if (column.Data.Contains("Action")) continue;
                orderByString += (column.Data) + (column.SortDirection == Column.OrderDirection.Ascendant ? " asc" : " desc");
            }

            IEnumerable<ProgramPdtCtrlHistory> IEnum_ProgramPdtCtrlHistory = _returnProgramPdtCtrlHistory.lstProgramPdtCtrlHistory.OrderBy(orderByString ==
                                                                                    string.Empty ? "ProgramName asc" : orderByString);
            _returnProgramPdtCtrlHistory.lstProgramPdtCtrlHistory = IEnum_ProgramPdtCtrlHistory.ToList<ProgramPdtCtrlHistory>();

            //Permission.
            _returnProgramPdtCtrlHistory.permisionControllerVM = this.getPermisionControllerViewModel();

            //DataTables.
            //return View.
            var DataTablesResponse = new
            {
                draw = requestModel.Draw,
                data = _returnProgramPdtCtrlHistory,
                recordsFiltered = _returnProgramPdtCtrlHistory.Total,
                recordsTotal = _returnProgramPdtCtrlHistory.Total
            };
            return Json(DataTablesResponse, JsonRequestBehavior.AllowGet);





            //ReturnProgramPdtCtrlHistory _returnProgramPdtCtrlHistory = programPdtCtrlHistory.SearchProgramPdtCtrlHistory(searchProgrampdtCtrl, pageNumber, pageSize);
            //_returnProgramPdtCtrlHistory.TotalPage = (_returnProgramPdtCtrlHistory.Total + pageSize - 1) / pageSize;
            //_returnProgramPdtCtrlHistory.TotalPage = _returnProgramPdtCtrlHistory.TotalPage == 0 ? 1 : _returnProgramPdtCtrlHistory.TotalPage;
            //JsonResult x = Json(_returnToolList, JsonRequestBehavior.AllowGet);
            //string y = new JavaScriptSerializer().Serialize(jsonResult.Data);
            //string y = Newtonsoft.Json.JsonConvert.SerializeObject(x.Data); 

            return Json(_returnProgramPdtCtrlHistory, JsonRequestBehavior.AllowGet);
        }


        
        public JsonResult Delete(ProgramPdtCtrlHistory p)
        {

            //ReturnToolList x = toolDB.DeleteByID(ID);
            //return Json(toolDB.DeleteByID(ID), JsonRequestBehavior.AllowGet);
            return Json(programPdtCtrlHistory.DeleteByKey(p), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Insert(ProgramPdtCtrlHistory p)
        {
            return Json(programPdtCtrlHistory.Insert(p), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Add(ProgramPdtCtrlHistory p)
        {
            return Json(programPdtCtrlHistory.Insert(p), JsonRequestBehavior.AllowGet);
        }
        public JsonResult CountbyID(ProgramPdtCtrlHistory p)
        {
            return Json(programPdtCtrlHistory.GetbyKey(p).Total, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListByPage(int pageNumber)
        {
            ReturnProgramPdtCtrlHistory _returnProgramPdtCtrlHistory = programPdtCtrlHistory.ListbyPage(pageNumber, pageSize);
            _returnProgramPdtCtrlHistory.TotalPage = (_returnProgramPdtCtrlHistory.Total + pageSize - 1) / pageSize;
            _returnProgramPdtCtrlHistory.TotalPage = _returnProgramPdtCtrlHistory.TotalPage == 0 ? 1 : _returnProgramPdtCtrlHistory.TotalPage;


            return Json(_returnProgramPdtCtrlHistory, JsonRequestBehavior.AllowGet);   
        }
        private PermisionControllerVM getPermisionControllerViewModel()
        {
            return SMCommon.getPermisionControllerViewModel(RouteData.Values["controller"].ToString(), (Session["UserPermission"] as ReturnUserPermission));
        }
    }
}