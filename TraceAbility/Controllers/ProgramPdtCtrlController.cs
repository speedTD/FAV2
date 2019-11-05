using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestABC.Models.Data;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using log4net;
using System.IO;
using OfficeOpenXml;
using DataTables.Mvc;
using System.Linq.Dynamic;
namespace TestABC.Controllers
{
    public class ProgramPdtCtrlController : Controller
    {
        private static readonly ILog mylog4net = LogManager.GetLogger(typeof(ProgramPdtCtrlController));
        // GET: ProgramPdtCtrl
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit()
        {
            return View();
        }
        public ActionResult ImportExcel()
        {
            return View();
        }
        ProgramPdtCtrlDB programPdtCtrl = new ProgramPdtCtrlDB();
        int pageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"]);

        public JsonResult GetbyKey(ProgramPdtCtrl p)
        {
            return Json(programPdtCtrl.GetbyKey(p), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetbyID(int ID)
        {
            return Json(programPdtCtrl.GetbyID(ID), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Search([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, ProgramPdtCtrl programPdtCtrl)
        {
            //DataTables.
            int pageSize = requestModel.Length != 0 ? requestModel.Length : 10;
            int pageNumber = requestModel.Start / requestModel.Length + 1;
            //ReturnProgramPdtCtrl searchProgrampdtCtrl = new ReturnProgramPdtCtrl();
            //searchProgrampdtCtrl.aProgramPdtCtrl = programPdtCtrl;
            ReturnProgramPdtCtrl _returnProgramPdtCtrl = (new ProgramPdtCtrlDB()).SearchProgramPdtCtrl(programPdtCtrl, pageNumber, pageSize);
            //_returnProgramPdtCtrl.TotalPage = (_returnProgramPdtCtrl.Total + pageSize - 1) / pageSize;
            //_returnProgramPdtCtrl.TotalPage = _returnProgramPdtCtrl.TotalPage == 0 ? 1 : _returnProgramPdtCtrl.TotalPage;

            // DataTables.
            // Avoid from datatables.net getting stuck (if data=null).
            if (_returnProgramPdtCtrl.lstProgramPdtCtrl == null)
                _returnProgramPdtCtrl.lstProgramPdtCtrl = new List<ProgramPdtCtrl>();
            //Sorting
            var sortedColumns = requestModel.Columns.GetSortedColumns();
            var orderByString = String.Empty;
            foreach (var column in sortedColumns)
            {
                if (column.Data.Contains("Action")) continue;
                orderByString += orderByString != String.Empty ? "," : "";
                orderByString += (column.Data) + (column.SortDirection == Column.OrderDirection.Ascendant ? " asc" : " desc");
            }
            IEnumerable<ProgramPdtCtrl> IEnum_MachineMtn = _returnProgramPdtCtrl.lstProgramPdtCtrl.OrderBy(orderByString ==
                                                                                    string.Empty ? "ID asc" : orderByString);
            _returnProgramPdtCtrl.lstProgramPdtCtrl = IEnum_MachineMtn.ToList<ProgramPdtCtrl>();

            //Permission.
            _returnProgramPdtCtrl.permisionControllerVM = this.getPermisionControllerViewModel();

            //DataTables.
            //return View.
            var DataTablesResponse = new
            {
                draw = requestModel.Draw,
                data = _returnProgramPdtCtrl,
                recordsFiltered = _returnProgramPdtCtrl.Total,
                recordsTotal = _returnProgramPdtCtrl.Total
            };
            return Json(DataTablesResponse, JsonRequestBehavior.AllowGet);



            //return Json(_returnProgramPdtCtrl, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProgramPdtCtrl_ByProgramName(ReturnProgramPdtCtrl searchProgrampdtCtrl)
        {
            ReturnProgramPdtCtrl _returnProgramPdtCtrl = programPdtCtrl.GetProgramPdtCtrl_ByProgramName(searchProgrampdtCtrl);
            //_returnProgramPdtCtrl.TotalPage = (_returnProgramPdtCtrl.Total + pageSize - 1) / pageSize;

            return Json(_returnProgramPdtCtrl, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(ProgramPdtCtrl p)
        {

            //ReturnToolList x = toolDB.DeleteByID(ID);
            //return Json(toolDB.DeleteByID(ID), JsonRequestBehavior.AllowGet);
            return Json(programPdtCtrl.DeleteByKey(p), JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteByID(int ID)
        {
            return Json(programPdtCtrl.DeleteByID(ID, Convert.ToInt32(Session["UserID"].ToString())), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Insert(ProgramPdtCtrl p)
        {
            return Json(programPdtCtrl.Insert(p), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(ProgramPdtCtrl p)
        {
            return Json(programPdtCtrl.Insert(p), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Add(ProgramPdtCtrl p)
        {
            return Json(programPdtCtrl.Insert(p), JsonRequestBehavior.AllowGet);
        }
        public JsonResult CountbyID(ProgramPdtCtrl p)
        {
            return Json(programPdtCtrl.GetbyKey(p).Total, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CountbyKey(ProgramPdtCtrl p)
        {
            return Json(programPdtCtrl.GetbyKey(p), JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListByPage(int pageNumber)
        {
            ReturnProgramPdtCtrl _returnProgramPdtCtrl = programPdtCtrl.ListbyPage(pageNumber, pageSize);
            _returnProgramPdtCtrl.TotalPage = (_returnProgramPdtCtrl.Total + pageSize - 1) / pageSize;
            _returnProgramPdtCtrl.TotalPage = _returnProgramPdtCtrl.TotalPage == 0 ? 1 : _returnProgramPdtCtrl.TotalPage;
            return Json(_returnProgramPdtCtrl, JsonRequestBehavior.AllowGet);
        }
        #region Import Excel to DB.
        public JsonResult ImportExcel_ProgramPdtCtrl()
        {
            //handle imported file..
            string fullPath_filename = "";
            string sheetName = "";
            string statusRowImport = "";
            ReturnProgramPdtCtrl _returnProgramPdtCtrl = new ReturnProgramPdtCtrl();

            if (Request.Files.Count == 0)
            {
                _returnProgramPdtCtrl.Code = "99";
                _returnProgramPdtCtrl.Message = "Không có file import/Not exists file to import!";
                return Json(_returnProgramPdtCtrl, JsonRequestBehavior.AllowGet);

            }
            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpPostedFileBase httpFile = Request.Files[0];

                    //Checking for Internet Explorer
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = httpFile.FileName.Split(new char[] { '\\' });
                        fullPath_filename = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fullPath_filename = httpFile.FileName;
                    }
                    string strExtension = Path.GetExtension(httpFile.FileName);
                    string strPrefixName = Path.GetFileNameWithoutExtension(httpFile.FileName);
                    //rename file.
                    strPrefixName += DateTime.Now.ToString("yyMMddHHmmss");
                    string fullName = strPrefixName + strExtension;

                    // Get the complete folder path and store the file inside it.  
                    fullPath_filename = Path.Combine(Server.MapPath("~/Uploads/ProductionControl"), fullName);
                    httpFile.SaveAs(fullPath_filename);
                }
                catch (Exception ex)
                {
                    mylog4net.Error("ImportFileMachineTypeMtnContent", ex);
                    throw ex;
                }
            }

            #region extract excel to datatable.
            DataTable dtExcelResult = ExtractExcelToDataTable(fullPath_filename, sheetName);
            #endregion

            //#region Check Validate data
            //foreach(DataRow row in dtExcelResult.Rows)
            //{
            //    if (String.IsNullOrEmpty(row[0].ToString()))
            //    {
            //        row[]
            //    }
            //}
            //#endregion

            #region Check validate data.
            string str_viewImportStatusTable_ToHTML = "";
            Import_ProgramPdtCtrl importObject = new Import_ProgramPdtCtrl();
            if (dtExcelResult != null)
            {
                if (dtExcelResult.Rows.Count > 0)
                {
                    //table body.
                    //str_viewImportStatusTable_ToHTML += "<tbody class=\"tbody\">";
                    foreach (DataRow dr in dtExcelResult.Rows)
                    {
                        try
                        {
                            importObject.ID = 0;
                            importObject.FactoryID = dr[0].ToString().Trim();
                            importObject.ProgramType = dr[1].ToString().Trim();
                            importObject.ProgramName = dr[2].ToString().Trim();
                            importObject.Part = dr[3].ToString().Trim();
                            importObject.ControlItem = dr[4].ToString().Trim();
                            importObject.ColumnName = dr[5].ToString().Trim();
                            importObject.SpecDisplay = dr[6].ToString().Trim();
                            importObject.Unit = dr[7].ToString().Trim();
                            importObject.LowerLimit = dr[8].ToString().Trim();
                            importObject.UpperLimit = dr[9].ToString().Trim();
                            importObject.OperatorID = Convert.ToInt32(Session["UserID"].ToString());

                            //check validate.
                            if (String.IsNullOrEmpty(importObject.FactoryID)
                                & String.IsNullOrEmpty(importObject.ProgramType)
                                & String.IsNullOrEmpty(importObject.ProgramName)
                                & String.IsNullOrEmpty(importObject.ControlItem)
                                & String.IsNullOrEmpty(importObject.LowerLimit)
                                & String.IsNullOrEmpty(importObject.UpperLimit))
                            {
                                statusRowImport = "Fail";
                            }
                            else
                            {
                                ReturnProgramPdtCtrl returnImport_ProgramPdtCtrl = ImportEachRowExcel_ToDB(importObject);
                                if (returnImport_ProgramPdtCtrl.Code == "00")
                                    statusRowImport = "Success";
                                else
                                    statusRowImport = "Fail";
                            }
                        }
                        catch (Exception ex)
                        {
                            mylog4net.Error("ImportExcel_ProgramPdtCtrl()", ex);
                            statusRowImport = "Fail";
                        }

                        str_viewImportStatusTable_ToHTML += "<tr>";
                        if (String.IsNullOrEmpty(dr[0].ToString().Trim())) //FactoryID
                            str_viewImportStatusTable_ToHTML += "<td style=\"color:red;\">" + dr[0].ToString().Trim() + "</td>";
                        else
                            str_viewImportStatusTable_ToHTML += "<td>" + dr[0].ToString().Trim() + "</td>";
                        if (String.IsNullOrEmpty(dr[1].ToString().Trim())) //FactoryID
                            str_viewImportStatusTable_ToHTML += "<td style=\"color:red;\">" + dr[1].ToString().Trim() + "</td>";
                        else
                            str_viewImportStatusTable_ToHTML += "<td>" + dr[1].ToString().Trim() + "</td>";
                        //str_viewImportStatusTable_ToHTML += "<td>" + importObject.ProgramType + "</td>"; //1 ProgramType
                        if (String.IsNullOrEmpty(dr[2].ToString().Trim())) //FactoryID
                            str_viewImportStatusTable_ToHTML += "<td style=\"color:red;\">" + dr[2].ToString().Trim() + "</td>";
                        else
                            str_viewImportStatusTable_ToHTML += "<td>" + dr[2].ToString().Trim() + "</td>";

                        //str_viewImportStatusTable_ToHTML += "<td>" + importObject.ProgramName + "</td>"; //2 ProgramName
                        str_viewImportStatusTable_ToHTML += "<td>" + importObject.Part + "</td>";        //3 Part
                        if (String.IsNullOrEmpty(dr[4].ToString().Trim())) //FactoryID
                            str_viewImportStatusTable_ToHTML += "<td style=\"color:red;\">" + dr[4].ToString().Trim() + "</td>";
                        else
                            str_viewImportStatusTable_ToHTML += "<td>" + dr[4].ToString().Trim() + "</td>";

                        //str_viewImportStatusTable_ToHTML += "<td>" + importObject.ControlItem + "</td>"; //4 ControlItem
                        str_viewImportStatusTable_ToHTML += "<td>" + importObject.ColumnName + "</td>";  //5 ColumnName
                        str_viewImportStatusTable_ToHTML += "<td>" + importObject.SpecDisplay + "</td>"; //6 SpecDisplay
                        str_viewImportStatusTable_ToHTML += "<td>" + importObject.Unit + "</td>";        //7 Unit
                        if (String.IsNullOrEmpty(dr[8].ToString().Trim())) //FactoryID
                            str_viewImportStatusTable_ToHTML += "<td style=\"color:red;\">" + dr[8].ToString().Trim() + "</td>";
                        else
                            str_viewImportStatusTable_ToHTML += "<td>" + dr[8].ToString().Trim() + "</td>";

                        //str_viewImportStatusTable_ToHTML += "<td>" + importObject.LowerLimit + "</td>";  //8 LowerLimit
                        if (String.IsNullOrEmpty(dr[9].ToString().Trim())) //FactoryID
                            str_viewImportStatusTable_ToHTML += "<td style=\"color:red;\">" + dr[9].ToString().Trim() + "</td>";
                        else
                            str_viewImportStatusTable_ToHTML += "<td>" + dr[9].ToString().Trim() + "</td>";

                        //str_viewImportStatusTable_ToHTML += "<td>" + importObject.UpperLimit + "</td>";  //9 UpperLimit
                        if (statusRowImport == "Fail")
                            str_viewImportStatusTable_ToHTML += "<td style=\"color:red;\">" + statusRowImport + "</td>";
                        else
                            str_viewImportStatusTable_ToHTML += "<td style=\"color: blue;\">" + statusRowImport + "</td>";
                        str_viewImportStatusTable_ToHTML += "</tr>";
                    }
                    //str_viewImportStatusTable_ToHTML += "</tbody>";
                }
                //str_viewImportStatusTable_ToHTML += "</table>";
            }
            #endregion
            //delete file after export.
            if (System.IO.File.Exists(fullPath_filename))
                System.IO.File.Delete(fullPath_filename);

            //return Json(machineMtnFrequencyDB.Insert(machineMtnFrequency), JsonRequestBehavior.AllowGet);
            _returnProgramPdtCtrl.Code = "00";
            _returnProgramPdtCtrl.Message = str_viewImportStatusTable_ToHTML;
            return Json(_returnProgramPdtCtrl, JsonRequestBehavior.AllowGet);
        }
        private ReturnProgramPdtCtrl ImportEachRowExcel_ToDB(Import_ProgramPdtCtrl importObject)
        {
            ProgramPdtCtrlDB programPdtCtrlDB = new ProgramPdtCtrlDB();
            return programPdtCtrlDB.ImportExcel(importObject);
        }

        private DataTable ExtractExcelToDataTable(string fullPath_FileName, string sheetName = "")
        {
            DataTable dt = new DataTable();
            // Load file excel và các setting ban đầu
            try
            {
                using (ExcelPackage package = new ExcelPackage(new FileInfo(fullPath_FileName)))
                {
                    ExcelWorkbook t1 = package.Workbook;
                    ExcelWorksheets x1 = t1.Worksheets;
                    int y = x1.Count;
                    if (package.Workbook.Worksheets.Count < 1)
                    {
                        // Log.
                        return null;
                    }
                    // Khởi Lấy Sheet đầu tiên trong file Excel để truy vấn, truyền vào name của Sheet để lấy ra sheet cần, nếu name = null thì lấy sheet đầu tiên
                    ExcelWorksheet workSheet = package.Workbook.Worksheets.FirstOrDefault(x => x.Name == sheetName) ?? package.Workbook.Worksheets.FirstOrDefault();

                    // đếm tổng số cột cần import.  (Đọc tất cả các header (header để ở dòng 1).)
                    int TotalColumn_Count = 0;
                    int TotalColumn = 10;
                    foreach (var firstRowCell in workSheet.Cells[1, 1, 1, TotalColumn]) //workSheet.Dimension.End.Column
                    {
                        if (firstRowCell.Text.Trim() != "")
                        {
                            TotalColumn_Count += 1;
                            dt.Columns.Add(firstRowCell.Text);
                        }
                    }
                    // Đọc tất cả data bắt đầu từ row thứ 2.
                    for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
                    {
                        // Lấy 1 row trong excel để truy vấn
                        var excelRow = workSheet.Cells[rowNumber, 1, rowNumber, TotalColumn_Count]; // workSheet.Dimension.End.Column];
                                                                                                    // tạo 1 row trong data table
                        var newRow = dt.NewRow();
                        foreach (var cell in excelRow)
                        {
                            newRow[cell.Start.Column - 1] = cell.Text;
                        }
                        dt.Rows.Add(newRow);
                    }
                }
            }
            catch (Exception ex)
            {
                mylog4net.Error(" ExtractExcelToDataTable ", ex);
                throw ex;
            }
            return dt;
        }
        #endregion

        #region Check data excel file.
        public JsonResult CheckFile_ImportExcel_ProgramPdtCtrl(bool isImportToDB = false)
        {
            bool isCheckingExcelTotalResult = true;
            //handle imported file..
            string fullPath_filename = "";
            string sheetName = "";
            string statusRowImport = "";
            ReturnProgramPdtCtrl _returnProgramPdtCtrl = new ReturnProgramPdtCtrl();
            #region checking file path.
            if (Request.Files.Count == 0)
            {
                _returnProgramPdtCtrl.Code = "99";
                _returnProgramPdtCtrl.Message = "Không có file import/Not exists file to import!";
                return Json(_returnProgramPdtCtrl, JsonRequestBehavior.AllowGet);
            }
            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpPostedFileBase httpFile = Request.Files[0];

                    //Checking for Internet Explorer
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = httpFile.FileName.Split(new char[] { '\\' });
                        fullPath_filename = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fullPath_filename = httpFile.FileName;
                    }
                    string strExtension = Path.GetExtension(httpFile.FileName);
                    string strPrefixName = Path.GetFileNameWithoutExtension(httpFile.FileName);
                    //rename file.
                    strPrefixName += DateTime.Now.ToString("yyMMddHHmmss");
                    string fullName = strPrefixName + strExtension;

                    // Get the complete folder path and store the file inside it.  
                    fullPath_filename = Path.Combine(Server.MapPath("~/Uploads/ProductionControl"), fullName);
                    httpFile.SaveAs(fullPath_filename);
                }
                catch (Exception ex)
                {
                    _returnProgramPdtCtrl.Code = "99";
                    _returnProgramPdtCtrl.Message = "Lỗi file. Hãy kiểm tra lại file/File error: " + ex.ToString();
                    mylog4net.Error("ImportFileMachineTypeMtnContent", ex);
                    return Json(_returnProgramPdtCtrl, JsonRequestBehavior.AllowGet);
                }
            }
            #endregion


            #region extract excel to datatable.
            // Khởi tạo data table
            DataTable dtResult;
            dtResult = ExtractExcelToDataTable(fullPath_filename, sheetName);
            #endregion

            #region check data excel file.
            string str_viewImportStatusTable_ToHTML = "";
            Import_ProgramPdtCtrl importObject = new Import_ProgramPdtCtrl();
            if (dtResult != null)
            {
                if (dtResult.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtResult.Rows)
                    {
                        try
                        {
                            statusRowImport = "Hợp lệ/OK";

                            importObject.ID = 0;
                            importObject.FactoryID = dr[0].ToString().Trim();
                            importObject.ProgramType = dr[1].ToString().Trim();
                            importObject.ProgramName = dr[2].ToString().Trim();
                            importObject.Part = dr[3].ToString().Trim();
                            importObject.ControlItem = dr[4].ToString().Trim();
                            importObject.ColumnName = dr[5].ToString().Trim();
                            importObject.SpecDisplay = dr[6].ToString().Trim();
                            importObject.Unit = dr[7].ToString().Trim();
                            importObject.LowerLimit = dr[8].ToString().Trim();
                            importObject.UpperLimit = dr[9].ToString().Trim();
                            importObject.OperatorID = Convert.ToInt32(Session["UserID"].ToString());

                            //check validate.
                            //check empty value.
                            if (String.IsNullOrEmpty(importObject.FactoryID)
                                || String.IsNullOrEmpty(importObject.ProgramType)
                                || String.IsNullOrEmpty(importObject.ProgramName)
                                || String.IsNullOrEmpty(importObject.ControlItem)
                                || String.IsNullOrEmpty(importObject.LowerLimit)
                                || String.IsNullOrEmpty(importObject.UpperLimit))
                            {
                                statusRowImport = "Lỗi/Fail - Thiếu dữ liệu bắt buộc phải nhập.";
                            }
                            //check Lowwer Upper
                            if (!statusRowImport.Contains("Fail"))
                            {
                                Double IntValue_UpperLimit;
                                Double IntValue_LowerLimit;
                                //if UpperLimit,LowerLimit are double values.
                                if (Double.TryParse(importObject.LowerLimit, out IntValue_LowerLimit) & Double.TryParse(importObject.UpperLimit, out IntValue_UpperLimit))
                                {
                                    if (IntValue_LowerLimit > IntValue_UpperLimit)
                                        statusRowImport = "Lỗi/Fail - UpperLimit nhỏ hơn LowerLimit";
                                }
                                //if just one (UpperLimit or LowerLimit) is double value.
                                else if (Double.TryParse(importObject.LowerLimit, out IntValue_UpperLimit) || Double.TryParse(importObject.LowerLimit, out IntValue_LowerLimit))
                                {
                                    statusRowImport = "Lỗi/Fail - UpperLimit, LowerLimit không đồng nhất dữ liệu kiểu chữ hoặc số.";
                                }
                                else //if UpperLimit,LowerLimit are string values.
                                {
                                    if (importObject.LowerLimit != importObject.UpperLimit)
                                    {
                                        statusRowImport = "Lỗi/Fail - Giá trị kiểu chữ của UpperLimit và LowerLimit không giống nhau.";
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            //mylog4net.Error("ImportExcel_ProgramPdtCtrl()", ex);
                            statusRowImport = "Lỗi/Fail :" +ex.ToString();
                        }

                        str_viewImportStatusTable_ToHTML += "<tr>";
                        str_viewImportStatusTable_ToHTML += "<td>" + importObject.FactoryID + "</td>";
                        str_viewImportStatusTable_ToHTML += "<td>" + importObject.ProgramType + "</td>";
                        str_viewImportStatusTable_ToHTML += "<td>" + importObject.ProgramName + "</td>";
                        str_viewImportStatusTable_ToHTML += "<td>" + importObject.Part + "</td>";
                        str_viewImportStatusTable_ToHTML += "<td>" + importObject.ControlItem + "</td>";
                        str_viewImportStatusTable_ToHTML += "<td>" + importObject.ColumnName + "</td>";
                        str_viewImportStatusTable_ToHTML += "<td>" + importObject.SpecDisplay + "</td>";
                        str_viewImportStatusTable_ToHTML += "<td>" + importObject.Unit + "</td>";
                        str_viewImportStatusTable_ToHTML += "<td>" + importObject.LowerLimit + "</td>";
                        str_viewImportStatusTable_ToHTML += "<td>" + importObject.UpperLimit + "</td>";
                        if (statusRowImport.Contains("Fail"))
                            str_viewImportStatusTable_ToHTML += "<td class=\"statusRowImport\" style=\"color: red;\">" + statusRowImport + "</td>";
                        else
                            str_viewImportStatusTable_ToHTML += "<td style=\"color: blue;\">" + statusRowImport + "</td>";
                        str_viewImportStatusTable_ToHTML += "</tr>";

                        if (statusRowImport.Contains("Fail"))
                            isCheckingExcelTotalResult = false;
                    }
                    //str_viewImportStatusTable_ToHTML += "</tbody>";
                }
                //str_viewImportStatusTable_ToHTML += "</table>";
            }
            #endregion

            #region import to Database.
            if (isCheckingExcelTotalResult & isImportToDB) //if data is validated and isImportToDB = true: allow to import to database.
            {
                str_viewImportStatusTable_ToHTML = "";
                if (dtResult != null & dtResult.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtResult.Rows)
                    {
                        try
                        {
                            importObject.ID = 0;
                            importObject.FactoryID = dr[0].ToString().Trim();
                            importObject.ProgramType = dr[1].ToString().Trim();
                            importObject.ProgramName = dr[2].ToString().Trim();
                            importObject.Part = dr[3].ToString().Trim();
                            importObject.ControlItem = dr[4].ToString().Trim();
                            importObject.ColumnName = dr[5].ToString().Trim();
                            importObject.SpecDisplay = dr[6].ToString().Trim();
                            importObject.Unit = dr[7].ToString().Trim();
                            importObject.LowerLimit = dr[8].ToString().Trim();
                            importObject.UpperLimit = dr[9].ToString().Trim();
                            importObject.OperatorID = Convert.ToInt32(Session["UserID"].ToString());

                            ReturnProgramPdtCtrl returnImport_ProgramPdtCtrl = ImportEachRowExcel_ToDB(importObject);
                            if (returnImport_ProgramPdtCtrl.Code == "00")
                                statusRowImport = "Import Success";
                            else
                                statusRowImport = "Import Fail";
                            
                            str_viewImportStatusTable_ToHTML += "<tr>";
                            str_viewImportStatusTable_ToHTML += "<td>" + importObject.FactoryID + "</td>";
                            str_viewImportStatusTable_ToHTML += "<td>" + importObject.ProgramType + "</td>";
                            str_viewImportStatusTable_ToHTML += "<td>" + importObject.ProgramName + "</td>";
                            str_viewImportStatusTable_ToHTML += "<td>" + importObject.Part + "</td>";
                            str_viewImportStatusTable_ToHTML += "<td>" + importObject.ControlItem + "</td>";
                            str_viewImportStatusTable_ToHTML += "<td>" + importObject.ColumnName + "</td>";
                            str_viewImportStatusTable_ToHTML += "<td>" + importObject.SpecDisplay + "</td>";
                            str_viewImportStatusTable_ToHTML += "<td>" + importObject.Unit + "</td>";
                            str_viewImportStatusTable_ToHTML += "<td>" + importObject.LowerLimit + "</td>";
                            str_viewImportStatusTable_ToHTML += "<td>" + importObject.UpperLimit + "</td>";
                            if (statusRowImport.Contains("Fail"))
                                str_viewImportStatusTable_ToHTML += "<td class=\"statusRowImport\" style=\"color: red;\">" + statusRowImport + "</td>";
                            else
                                str_viewImportStatusTable_ToHTML += "<td style=\"color: blue;\">" + statusRowImport + "</td>";
                            str_viewImportStatusTable_ToHTML += "</tr>";
                        }
                        catch (Exception ex)
                        {
                            mylog4net.Error("ImportExcel_ProgramPdtCtrl()", ex);
                            statusRowImport = "Fail";
                        }
                    }
                }
            }
            #endregion
            
            //delete file after export.                     
            if (System.IO.File.Exists(fullPath_filename))
                System.IO.File.Delete(fullPath_filename);

            //return Json(machineMtnFrequencyDB.Insert(machineMtnFrequency), JsonRequestBehavior.AllowGet);
            _returnProgramPdtCtrl.Code = "00";
            _returnProgramPdtCtrl.Message = str_viewImportStatusTable_ToHTML;
            return Json(_returnProgramPdtCtrl, JsonRequestBehavior.AllowGet);
        }
        #endregion
        private PermisionControllerVM getPermisionControllerViewModel()
        {
            return SMCommon.getPermisionControllerViewModel(RouteData.Values["controller"].ToString(), (Session["UserPermission"] as ReturnUserPermission));
        }
    }

}