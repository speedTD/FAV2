using System;
using System.Web.Mvc;
using TestABC.Models.Data;
using System.Collections.Generic;
using DataTables.Mvc;
using System.Linq;
using System.Linq.Dynamic;
namespace TestABC.Controllers
{
    public class MachineMtnMonthYearController : Controller
    {
        MachineMtnDB machineMtnDB = new MachineMtnDB();
        MachineMtnContentListDB machineMtnContentListDB = new MachineMtnContentListDB();
        MachineMtnDetailDB machineMtnDetailDB = new MachineMtnDetailDB();
        int pageSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
        PermisionControllerVM permisionVM;
        #region Action
        // GET: MachineMtnMonthYear
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MachineMtnMonthYearList()
        {
            //permisionVM
            permisionVM = this.getPermisionControllerViewModel();
            if (!permisionVM.isAllow_View)
                return RedirectToAction("NotifyInfo", "Home", new { Message = "Không được cập quyền/Not permission!" });
            return View(permisionVM);
        }
        public ActionResult Add()
        {
            permisionVM = this.getPermisionControllerViewModel();
            if (!permisionVM.isAllow_Add)
                return RedirectToAction("NotifyInfo", "Home", new Notification() { Message = "Không được cập quyền/Not permission!" });
            return View();
        }
        public ActionResult Edit()
        {
            permisionVM = this.getPermisionControllerViewModel();
            if (!(permisionVM.isAllow_View || permisionVM.isAllow_Edit))
                return RedirectToAction("NotifyInfo", "Home", new { Message = "Không được cập quyền/Not permission!" });
            return View(permisionVM);

        }
        #endregion
        public JsonResult CheckMachineMtnData(string MachineID, string FrequencyID, string MaintenanceDate, string Shift, string Week, string Month, string Year, string CheckerResult, string Result)
        {
            string where = " 1=1 ";
            string where_MachineID = "";
            string where_FrequencyID = "";
            string where_MaintenanceDate = "";
            string where_Shift = "";
            string where_Week = "";
            string where_Month = "";
            string where_Year = "";
            string where_CheckerResult = "";
            string where_Result = ""; //total Result.

            if (!string.IsNullOrEmpty(MachineID))
                where_MachineID = " and tMachineMtn.MachineID = '" + MachineID + "'";
            if (!string.IsNullOrEmpty(FrequencyID))
                where_FrequencyID = " and tMachineMtn.FrequencyID = '" + FrequencyID + "' ";
            if (!string.IsNullOrEmpty(MaintenanceDate))
                where_MaintenanceDate = " and tMachineMtn.MaintenanceDate = '" + MaintenanceDate + "' ";
            if (!string.IsNullOrEmpty(Shift))
                where_Shift = " and Shift = '" + Shift + "' ";
            if (!string.IsNullOrEmpty(Week))
                where_Week += " AND Week = '" + Week + "' ";
            if (!string.IsNullOrEmpty(Month))
                where_Month += " AND Month = '" + Month + "' ";
            if (!string.IsNullOrEmpty(Year))
                where_Year += " AND Year = '" + Year + "' ";
            if (FrequencyID == "1")
                where += where_MachineID + where_FrequencyID + where_MaintenanceDate + where_Shift;
            if (FrequencyID == "2")
                where += where_MachineID + where_FrequencyID + where_Week + where_Year;
            if (FrequencyID == "3" || FrequencyID == "4" || FrequencyID == "5")
                where += where_MachineID + where_FrequencyID + where_Month + where_Year;
            if (FrequencyID == "6")
                where += where_MachineID + where_FrequencyID + where_Year;
            if (!string.IsNullOrEmpty(CheckerResult))
                where += " AND CheckerResult = '" + CheckerResult + "' ";
            if (!string.IsNullOrEmpty(Result))
                where += " AND Result = '" + Result + "' ";

            return Json(machineMtnDB.SelectByCondition(where), JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertMachineMtn(MachineMtn machineMtn)
        {
            return Json(machineMtnDB.Insert(machineMtn), JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateCheckerResult_ByMachineMtnID(MachineMtn machineMtn)
        {
            return Json(machineMtnDB.UpdateCheckerResult_ByMachineMtnID(machineMtn), JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateMachineMtn(MachineMtn machineMtn)
        {
            return Json(machineMtnDB.Insert(machineMtn), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int ID)
        {
            permisionVM = this.getPermisionControllerViewModel();
            if (!(permisionVM.isAllow_Delete))
            {
                ReturnMachineMtn returnMachineMtn = new ReturnMachineMtn();
                returnMachineMtn.Code = "99";
                returnMachineMtn.Message = "Không được cấp quyền/ Not permission!";
                return Json(returnMachineMtn, JsonRequestBehavior.AllowGet);
            }
            return Json(machineMtnDB.DeleteByID(ID), JsonRequestBehavior.AllowGet);
        }
        //public JsonResult SelectByCondition(string MachineID, string FrequencyID, string MaintenanceDate, string Shift, string Week, string Month, string Year, string CheckerResult, string Result)
        //{
        //    string where = "1=1 ";
        //    string where_MachineID = "";
        //    string where_FrequencyID = "";
        //    string where_MaintenanceDate = "";
        //    string where_Shift = "";
        //    string where_Week = "";
        //    string where_Month = "";
        //    string where_Year = "";

        //    if (!string.IsNullOrEmpty(MachineID))
        //        where_MachineID = " and tMachineMtn.MachineID = '" + MachineID + "'";
        //    if (!string.IsNullOrEmpty(FrequencyID))
        //        where_FrequencyID = " and tMachineMtn.FrequencyID = '" + FrequencyID + "' ";

        //    if (!string.IsNullOrEmpty(MaintenanceDate))
        //        where_MaintenanceDate = " and tMachineMtn.MaintenanceDate = '" + MaintenanceDate + "' ";
        //    if (!string.IsNullOrEmpty(Shift))
        //        where_Shift = " and Shift = '" + Shift + "' ";
        //    if (!string.IsNullOrEmpty(Week))
        //        where_Week += " AND Week = '" + Week + "' ";
        //    if (!string.IsNullOrEmpty(Month))
        //        where_Month += " AND Month = '" + Month + "' ";
        //    if (!string.IsNullOrEmpty(Year))
        //        where_Year += " AND Year = '" + Year + "' ";

        //    //tong hop cau lenh where.
        //    if (string.IsNullOrEmpty(FrequencyID))  // lay tat ca.
        //    {
        //        where += where_MachineID + where_FrequencyID + where_MaintenanceDate + where_Shift;
        //        where += where_Week + where_Month + where_Year;
        //    }
        //    if (FrequencyID == "1")
        //        where += where_MachineID + where_FrequencyID + where_MaintenanceDate + where_Shift;
        //    if (FrequencyID == "2")
        //        where += where_MachineID + where_FrequencyID + where_Week + where_Year;
        //    if (FrequencyID == "3" || FrequencyID == "4" || FrequencyID == "5")
        //        where += where_MachineID + where_FrequencyID + where_Month + where_Year;
        //    if (FrequencyID == "6")
        //        where += where_MachineID + where_FrequencyID + where_Year;

        //    if (!string.IsNullOrEmpty(CheckerResult))
        //        where += " AND CheckerResult = '" + CheckerResult + "' ";
        //    if (!string.IsNullOrEmpty(Result))
        //        where += " AND Result = '" + Result + "' ";

        //    ReturnMachineMtn _returnMachineMtn = machineMtnDB.SelectByCondition(where);
        //    _returnMachineMtn.permisionControllerVM = ClassCommon.getPermisionControllerViewModel(RouteData.Values["controller"].ToString(), (Session["UserPermission"] as ReturnUserPermission));
        //    _returnMachineMtn.TotalPage = (_returnMachineMtn.Total + pageSize - 1) / pageSize;
        //    _returnMachineMtn.TotalPage = _returnMachineMtn.TotalPage == 0 ? 1 : _returnMachineMtn.TotalPage;

        //    return Json(_returnMachineMtn, JsonRequestBehavior.AllowGet);  
        //}
        public JsonResult SelectByPage(string MachineID, string FrequencyID, string MaintenanceDate, string Shift, string Week, string Month, string Year, string CheckerResult, string Result, int pageNumber)
        {
            string where = " 1=1 ";
            string where_MachineID = "";
            string where_FrequencyID = "";
            string where_MaintenanceDate = "";
            string where_Shift = "";
            string where_Week = "";
            string where_Month = "";
            string where_Year = "";

            if (!string.IsNullOrEmpty(MachineID))
                where_MachineID = " and tMachineMtn.MachineID = '" + MachineID + "'";
            if (!string.IsNullOrEmpty(FrequencyID))
                where_FrequencyID = " and tMachineMtn.FrequencyID = '" + FrequencyID + "' ";

            if (!string.IsNullOrEmpty(MaintenanceDate))
                where_MaintenanceDate = " and tMachineMtn.MaintenanceDate = '" + MaintenanceDate + "' ";
            if (!string.IsNullOrEmpty(Shift))
                where_Shift = " and Shift = '" + Shift + "' ";
            if (!string.IsNullOrEmpty(Week))
                where_Week += " AND Week = '" + Week + "' ";
            if (!string.IsNullOrEmpty(Month))
                where_Month += " AND Month = '" + Month + "' ";
            if (!string.IsNullOrEmpty(Year))
                where_Year += " AND Year = '" + Year + "' ";

            //tong hop cau lenh where.
            if (string.IsNullOrEmpty(FrequencyID))  // lay tat ca.
            {
                where += where_MachineID + where_FrequencyID + where_MaintenanceDate + where_Shift;
                where += where_Week + where_Month + where_Year;
            }
            if (FrequencyID == "1")
                where += where_MachineID + where_FrequencyID + where_MaintenanceDate + where_Shift;
            if (FrequencyID == "2")
                where += where_MachineID + where_FrequencyID + where_Week + where_Year;
            if (FrequencyID == "3" || FrequencyID == "4" || FrequencyID == "5")
                where += where_MachineID + where_FrequencyID + where_Month + where_Year;
            if (FrequencyID == "6")
                where += where_MachineID + where_FrequencyID + where_Year;

            if (!string.IsNullOrEmpty(CheckerResult))
                where += " AND CheckerResult = '" + CheckerResult + "' ";
            if (!string.IsNullOrEmpty(Result))
                where += " AND Result = '" + Result + "' ";

            ReturnMachineMtn _returnMachineMtn = machineMtnDB.SelectByPage(where, pageNumber, pageSize);
            _returnMachineMtn.permisionControllerVM = this.getPermisionControllerViewModel(); //permission
            _returnMachineMtn.TotalPage = (_returnMachineMtn.Total + pageSize - 1) / pageSize;
            _returnMachineMtn.TotalPage = _returnMachineMtn.TotalPage == 0 ? 1 : _returnMachineMtn.TotalPage;

            return Json(_returnMachineMtn, JsonRequestBehavior.AllowGet);  //Trả về dạng List<>   
        }
        public JsonResult SelectByPageDataTable([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, MachineMtn machineMtn)
        {
            //DataTables.
            int pageSize = requestModel.Length != 0 ? requestModel.Length : 10;
            int pageNumber = requestModel.Start / requestModel.Length + 1;

            string where = " where 1=1 ";
            string where_MachineID = "";
            string where_FrequencyID = "";
            string where_MaintenanceDate = "";
            string where_Shift = "";
            string where_Week = "";
            string where_Month = "";
            string where_Year = "";

            if (!string.IsNullOrEmpty(machineMtn.MachineID))
                where_MachineID = " and tMachineMtn.MachineID = '" + machineMtn.MachineID + "'";
            if (machineMtn.FrequencyID != 0)
                where_FrequencyID = " and tMachineMtn.FrequencyID = '" + machineMtn.FrequencyID + "' ";

            if (machineMtn.MaintenanceDate != DateTime.MinValue)
                where_MaintenanceDate = " and tMachineMtn.MaintenanceDate = '" + machineMtn.MaintenanceDate + "' ";
            if (machineMtn.Shift != 0)
                where_Shift = " and Shift = '" + machineMtn.Shift + "' ";
            if (machineMtn.Week != 0)
                where_Week += " AND Week = '" + machineMtn.Week + "' ";
            if (machineMtn.Month != 0)
                where_Month += " AND Month = '" + machineMtn.Month + "' ";
            if (machineMtn.Year != 0)
                where_Year += " AND Year = '" + machineMtn.Year + "' ";

            //tong hop cau lenh where.
            if (machineMtn.FrequencyID == 0)  // lay tat ca.
            {
                where += where_MachineID + where_FrequencyID + where_MaintenanceDate + where_Shift;
                where += where_Week + where_Month + where_Year;
            }
            if (machineMtn.FrequencyID == 1)
                where += where_MachineID + where_FrequencyID + where_MaintenanceDate + where_Shift;
            if (machineMtn.FrequencyID == 2)
                where += where_MachineID + where_FrequencyID + where_Week + where_Year;
            if (machineMtn.FrequencyID == 3 || machineMtn.FrequencyID == 4 || machineMtn.FrequencyID == 5)
                where += where_MachineID + where_FrequencyID + where_Month + where_Year;
            if (machineMtn.FrequencyID == 6)
                where += where_MachineID + where_FrequencyID + where_Year;

            if (!string.IsNullOrEmpty(machineMtn.CheckerResult))
                where += " AND CheckerResult = '" + machineMtn.CheckerResult + "' ";
            if (!string.IsNullOrEmpty(machineMtn.Result))
                where += " AND Result = '" + machineMtn.Result + "' ";
            //Execute.            
            ReturnMachineMtn _returnMachineMtn = machineMtnDB.SelectByPage(where, pageNumber, pageSize);

            // DataTables.
            // Avoid from datatables.net getting stuck (if data=null).
            if (_returnMachineMtn.lstMachineMtn == null)
                _returnMachineMtn.lstMachineMtn = new List<MachineMtn>();
            //Sorting
            var sortedColumns = requestModel.Columns.GetSortedColumns();
            var orderByString = String.Empty;
            foreach (var column in sortedColumns)
            {
                orderByString += orderByString != String.Empty ? "," : "";
                orderByString += (column.Data) + (column.SortDirection == Column.OrderDirection.Ascendant ? " asc" : " desc");
            }
            IEnumerable<MachineMtn> IEnum_MachineMtn = _returnMachineMtn.lstMachineMtn.OrderBy(orderByString ==
                                                                                    string.Empty ? "MachineID asc" : orderByString);
            _returnMachineMtn.lstMachineMtn = IEnum_MachineMtn.ToList<MachineMtn>();

            //Permission.
            _returnMachineMtn.permisionControllerVM = this.getPermisionControllerViewModel();

            //DataTables.
            //return View.
            var DataTablesResponse = new
            {
                draw = requestModel.Draw,
                data = _returnMachineMtn,
                recordsFiltered = _returnMachineMtn.Total,
                recordsTotal = _returnMachineMtn.Total
            };
            return Json(DataTablesResponse, JsonRequestBehavior.AllowGet);
        }
        public JsonResult InsertMachineMtnDetail(MachineMtnDetail machineMtnDetail)
        {
            return Json(machineMtnDetailDB.Insert(machineMtnDetail), JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateMachineMtnDetail(MachineMtnDetail machineMtnDetail)
        {
            return Json(machineMtnDetailDB.Insert(machineMtnDetail), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMachineMtnDetail(int MachineMtnID)
        {
            //checkpermissionVM
            ReturnMachineMtnContentDetail returnMachineMtnContentDetail = machineMtnDetailDB.SelectByMachineMtnID(MachineMtnID);
            returnMachineMtnContentDetail.permisionControllerVM = this.getPermisionControllerViewModel();

            return Json(returnMachineMtnContentDetail, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMachineMtnbyID(int MachineMtnID)
        {
            return Json((machineMtnDB.GetMachineMtnbyID(MachineMtnID)), JsonRequestBehavior.AllowGet);
        }
        public JsonResult CountMachineMtByMachineID(string MachineID)
        {
            return Json((machineMtnDB.CountMachineMtByMachineID(MachineID)), JsonRequestBehavior.AllowGet);
        }
        public JsonResult SelectByMachineMtnID(string MachineID)
        {
            return Json((machineMtnDB.CountMachineMtByMachineID(MachineID)), JsonRequestBehavior.AllowGet);
        }
        private PermisionControllerVM getPermisionControllerViewModel()
        {
            return SMCommon.getPermisionControllerViewModel(RouteData.Values["controller"].ToString(), (Session["UserPermission"] as ReturnUserPermission));
        }

    }
}