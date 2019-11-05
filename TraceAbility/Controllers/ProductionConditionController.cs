using DataTables.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using TestABC.Models.Data;
using TestABC.Models.ProductionControl;
using System.Linq.Dynamic;

namespace TestABC.Controllers
{
    public class ProductionConditionController : Controller
    {
        // GET: ProductionCondition
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductionControlList()
        {
            return View();
        }

        #region Add / Edit.
        public ActionResult Add()
        {
            return View();
        }
        
        public ActionResult Edit()
        {
            return View();
        }
        #endregion
        public JsonResult CountBySelection(ProductionControl productionControl) {
            string where = " 1=1 ";
            string where_MachineID = "";
            string where_ProgramName = "";
            string where_PdtCtrlDateTime = "";
            
            if (!string.IsNullOrEmpty(productionControl.MachineID))
                where_MachineID = " and MachineID = '" + productionControl.MachineID + "'";
            if (!string.IsNullOrEmpty(productionControl.ProgramName))
                where_ProgramName = " and ProgramName  = '" + productionControl.ProgramName + "' ";
            if (productionControl.PdtCtrlDateTime != null)
                where_PdtCtrlDateTime = " and PdtCtrlDateTime = '" + productionControl.PdtCtrlDateTime + "' ";
            where += where_MachineID + where_ProgramName + where_PdtCtrlDateTime;

            return Json(productionConTrolDB.CountBySelection(where), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetbyMachinePattern(string lineID, string machineID, string patternCode)
        {
            ConditionSettingDB conditionSettingDB = new ConditionSettingDB();
            ReturnConditionSetting _returnConditionSetting = new ReturnConditionSetting();
            _returnConditionSetting = conditionSettingDB.GetbyMachinePattern(lineID, machineID, patternCode);
            return Json(_returnConditionSetting, JsonRequestBehavior.AllowGet);  
        }
        public JsonResult GetbyMachineTypeID(string MachineID, string BatchNo)
        {
            //ConditionSettingDB conditionSettingDB = new ConditionSettingDB();    check
            ConditionSettingDB conditionSettingDB = new ConditionSettingDB();
            ReturnConditionSetting _returnConditionSetting = new ReturnConditionSetting();
            _returnConditionSetting = conditionSettingDB.GetbyMachineTypeID(MachineID, BatchNo);
            return Json(_returnConditionSetting, JsonRequestBehavior.AllowGet);
        }
        ProductionControlDB productionConTrolDB = new ProductionControlDB();
        ProductionControlDetailDB productionConTrolDetailDB = new ProductionControlDetailDB();
        public JsonResult List()
        {
            JsonResult x = Json(productionConTrolDB.ProductionControlAll().lstProductionControl, JsonRequestBehavior.AllowGet);

            return Json(productionConTrolDB.ProductionControlAll().lstProductionControl, JsonRequestBehavior.AllowGet);   
        }
        //public JsonResult SearchProductionControl(string where)
        //{
        //    return Json(productionConTrolDB.SearchProductionControl(where), JsonRequestBehavior.AllowGet);   
        //}
        public JsonResult SearchProductionControl_UsingDataTables([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, SearchProductionControl searchProductionControl)
        {
            //DataTables.
            int pageSize = requestModel.Length != 0 ? requestModel.Length : 10;
            int pageNumber = requestModel.Start / requestModel.Length + 1;
            

            ReturnProductionControl _returnProductionControl = productionConTrolDB.SearchProductionControl_UsingDataTables(searchProductionControl, pageNumber, pageSize);
            // DataTables.
            // Avoid from datatables.net getting stuck (if data=null).
            if (_returnProductionControl.lstProductionControl == null)
                _returnProductionControl.lstProductionControl = new List<ProductionControl>();
            //Sorting
            var sortedColumns = requestModel.Columns.GetSortedColumns();
            var orderByString = String.Empty;
            foreach (var column in sortedColumns)
            {
                orderByString += orderByString != String.Empty ? "," : "";
                orderByString += (column.Data) + (column.SortDirection == Column.OrderDirection.Ascendant ? " asc" : " desc");
            }
            IEnumerable<ProductionControl> IEnum_ProductionControl = _returnProductionControl.lstProductionControl.OrderBy(orderByString ==
                                                                                    string.Empty ? "MachineID asc" : orderByString);
            _returnProductionControl.lstProductionControl = IEnum_ProductionControl.ToList<ProductionControl>();

            //Permission.
            _returnProductionControl.permisionControllerVM = this.getPermisionControllerViewModel();

            //DataTables.
            //return View.
            var DataTablesResponse = new
            {
                draw = requestModel.Draw,
                data = _returnProductionControl,
                recordsFiltered = _returnProductionControl.Total,
                recordsTotal = _returnProductionControl.Total
            };
            return Json(DataTablesResponse, JsonRequestBehavior.AllowGet);
            //return Json(productionConTrolDB.SearchProductionControl_UsingDataTables(searchProductionControl), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetbyID(int ID)
        {
            return Json((productionConTrolDB.GetbyID(ID)).lstProductionControl[0], JsonRequestBehavior.AllowGet); 
        }
        public JsonResult Insert(ProductionControl productionControl)
        {
            return Json(productionConTrolDB.Insert(productionControl), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(ProductionControl productionControl)
        {
            return Json(productionConTrolDB.Insert(productionControl), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int ID)
        {
            return Json(productionConTrolDB.DeleteByID(ID), JsonRequestBehavior.AllowGet);
        }
        public JsonResult InsertProductionControlDetail(ProductionControlDetail productionControlDetail)
        {
            return Json(productionConTrolDetailDB.InsertProductionControlDetail(productionControlDetail), JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateProductionControlDetail(ProductionControlDetail productionControlDetail)
        {
            return Json(productionConTrolDetailDB.InsertProductionControlDetail(productionControlDetail), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetbyProductionControlID(int ProductionControlID)
        {

            //check
            return Json((productionConTrolDetailDB.GetbyProductionControlID(ProductionControlID)), JsonRequestBehavior.AllowGet); 
        }
        private PermisionControllerVM getPermisionControllerViewModel()
        {
            return SMCommon.getPermisionControllerViewModel(RouteData.Values["controller"].ToString(), (Session["UserPermission"] as ReturnUserPermission));
        }
    }
}

