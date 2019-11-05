using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestABC.Models.Data;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using System.Configuration;

using log4net;
namespace TestABC.Controllers
{
    public class MachineMtnContentListController : Controller
    {
        private static readonly ILog mylog4net = LogManager.GetLogger(typeof(MachineMtnContentListController));
        // GET: MachineMtnContentList
        public ActionResult Index()
        {
            return View();
        }
        MachineMtnContentListDB machineMtnContentListDB = new MachineMtnContentListDB();
        MachineTypeFrequencyDB machineTypeFrequencyDB = new MachineTypeFrequencyDB();
        int pageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"]);

        public JsonResult List()
        {
            return Json(machineMtnContentListDB.MachineMtnContentListAll().lstMachineMtnContentList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetbyID(int ID)
        {
            return Json((machineMtnContentListDB.GetbyID(ID)), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Insert(MachineMtnContentList machineMtnContentList)
        {
            return Json(machineMtnContentListDB.Insert(machineMtnContentList), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(MachineMtnContentList machineMtnContentList)
        {
            return Json(machineMtnContentListDB.Insert(machineMtnContentList), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int ID)
        {
            return Json(machineMtnContentListDB.DeleteByID(ID), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetByMachineID(string MachineID, short FrequencyID)
        {
            return Json(machineMtnContentListDB.GetByMachineID(MachineID, FrequencyID), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetByMachineTypeID(int MachineTypeID)
        {
            return Json(machineMtnContentListDB.GetByMachineTypeID(MachineTypeID), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Search(ReturnMachineMtnContentList machineMtnContentList)
        {
            ReturnMachineMtnContentList _returnMachineMtnContentList = machineMtnContentListDB.SearchMachineMtnContentList(machineMtnContentList, pageSize);
            _returnMachineMtnContentList.TotalPage = (_returnMachineMtnContentList.Total + pageSize - 1) / pageSize;
            _returnMachineMtnContentList.TotalPage = _returnMachineMtnContentList.TotalPage == 0 ? 1 : _returnMachineMtnContentList.TotalPage;
            return Json(_returnMachineMtnContentList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CountbyCondition(MachineMtnContentList machineMtnContentList)
        {
            return Json(machineMtnContentListDB.CountbyCondition(machineMtnContentList), JsonRequestBehavior.AllowGet);
        }
        public JsonResult CountbyMachineMtnContentID(int machineMtnContentID)
        {
            return Json((new MachineMtnDetailDB()).CountbyMachineMtnContentID(machineMtnContentID), JsonRequestBehavior.AllowGet);
        }

        #region Import Excel.
        public JsonResult ImportFileMachineTypeMtnContent()
        {
            ReturnMachineMtnContentList returnTotal = new ReturnMachineMtnContentList();
            returnTotal.Code = "00";  // thanh cong.

            #region validate
            //store Frequency of MachineType. 
            MachineTypeFrequency machineTypeFrequency_Import = Newtonsoft.Json.JsonConvert.DeserializeObject<MachineTypeFrequency>(Request.Form[0]);
            ReturnMachineTypeFrequency _returnMachineMtnFrequency = machineTypeFrequencyDB.Insert(machineTypeFrequency_Import);
            if (_returnMachineMtnFrequency.Code == "99")
            {
                returnTotal.Code = "99";
                returnTotal.Message = _returnMachineMtnFrequency.Message;
                return Json(returnTotal, JsonRequestBehavior.AllowGet);
            }
            //xu ly file nhap vao.
            string tenfile = "";
            string fullPath_filename = "";
            string sheetName = "";

            if (Request.Files.Count == 0)
            {
                returnTotal.Code = "99";
                returnTotal.Message = "Hãy chọn file/Choose file.";
                return Json(returnTotal, JsonRequestBehavior.AllowGet);
            }
            #endregion validate.

            #region Upload file to server.
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
                    fullPath_filename = Path.Combine(Server.MapPath("~/Uploads/MachineMaintenance"), fullName);
                    httpFile.SaveAs(fullPath_filename);
                }
                catch (Exception ex)
                {
                    mylog4net.Error("ImportFileMachineTypeMtnContent", ex);

                    if (Request.Files.Count == 0)
                    {
                        returnTotal.Code = "99";
                        returnTotal.Message = "Lỗi cập nhật file lên server./Failed to upload file to server.";
                        return Json(returnTotal, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            #endregion Upload file to server.

            #region extract excel to datatable.
            // Khởi tạo data table
            DataTable dtFromExcel;
            dtFromExcel = ExtractExcelToDataTable(fullPath_filename, sheetName);
            #endregion

            #region validate
            if (dtFromExcel == null || (dtFromExcel.Rows.Count == 0))
            {
                returnTotal.Code = "99";
                returnTotal.Message = "Không có dữ liệu (hoặc lỗi) trong file. Hãy kiểm tra lại/Not exists data in file. Check again!";
                return Json(returnTotal, JsonRequestBehavior.AllowGet);
            }
            #endregion

            #region import datatable to DB

            MachineMtnContentListDB machineMtnContentListDB = new MachineMtnContentListDB();
            MachineMtnContentList machineMtnContentList = new MachineMtnContentList();

            Import_MachineMtnType_ContentList import_aMachineMtnType_ContentList = new Import_MachineMtnType_ContentList();
            import_aMachineMtnType_ContentList.MachineTypeID = machineTypeFrequency_Import.MachineTypeID;
            import_aMachineMtnType_ContentList.FrequencyID = machineTypeFrequency_Import.FrequencyID;

            ReturnMachineMtnContentList resultImport_aMachineMtnContentList;

            dtFromExcel.Columns.Add(new DataColumn("Status"));
            foreach (DataRow dr in dtFromExcel.Rows)
            {
                try
                {
                    if (dr[0].ToString().Trim() != "" && dr[1].ToString().Trim() != "")
                    {
                        import_aMachineMtnType_ContentList.MachinePart = dr[0].ToString().Trim();   //Item bộ phận bảo dưỡng.
                        import_aMachineMtnType_ContentList.ContentMtn = dr[1].ToString().Trim();    //Content noi dung bao duong.
                        import_aMachineMtnType_ContentList.ToolMtn = dr[2].ToString().Trim();       //Tool.
                        import_aMachineMtnType_ContentList.MethodMtn = dr[3].ToString().Trim();     //Method.
                        import_aMachineMtnType_ContentList.Standard = dr[4].ToString().Trim();      //Standard.
                        import_aMachineMtnType_ContentList.Min = dr[5].ToString().Trim();      //Min.
                        import_aMachineMtnType_ContentList.Max = dr[6].ToString().Trim();      //Max.
                        resultImport_aMachineMtnContentList = Import_aMachineMtnContent_To_DB(import_aMachineMtnType_ContentList);

                        if (resultImport_aMachineMtnContentList.Code == "00")
                            dr["Status"] = "Thành công/Success.";
                        else if (resultImport_aMachineMtnContentList.Code == "99")
                            dr["Status"] = "Thất bại/Fail.";
                    }
                    else
                        dr["Status"] = "Thất bại/Fail.";
                }
                catch (Exception)
                {
                    dr["Status"] = "Thất bại/Fail.";
                }
            }
            #endregion

            //delete file after export.
            if (System.IO.File.Exists(fullPath_filename))
                System.IO.File.Delete(fullPath_filename);
            return Json(returnTotal, JsonRequestBehavior.AllowGet);
        }
        public ReturnMachineMtnContentList Import_aMachineMtnContent_To_DB(Import_MachineMtnType_ContentList import_MachineMtnType_ContentList)
        {
            MachineMtnContentListDB machineMtnContentListDB = new MachineMtnContentListDB();
            return machineMtnContentListDB.ImportExcel(import_MachineMtnType_ContentList);
        }
        public DataTable ExtractExcelToDataTable(string fullPath_FileName, string sheetName = "")
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
                    //count columns 
                    int TotalColumn_Count = 0;
                    // Read all headers (headers in row 4).
                    foreach (var firstRowCell in workSheet.Cells[4, 1, 1, workSheet.Dimension.End.Column])
                    {
                        if (firstRowCell.Text.Trim() != "")
                        {
                            TotalColumn_Count += 1;
                            dt.Columns.Add(firstRowCell.Text);
                        }
                    }
                    
                    //Read all data from row 5.
                    for (var rowNumber = 5; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
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
                throw ex;
            }
            return dt;
        }
        #endregion

        //public void ImportFileMachineMtnContent(MachineMtnFrequency machineMtnFrequency, string filePath)
        //{
        //    Excel.Application xlApp = new Excel.Application();
        //    Excel.Workbook xlWorkbook = xlApp.Workbooks.Open("C:\\MachineMtnContent.xlsx");
        //    Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
        //    Excel.Range xlRange = xlWorksheet.UsedRange;
        //    int rowCount = xlRange.Rows.Count;
        //    int colCount = xlRange.Columns.Count;
        //    MachineMtnContentList machineMtnContentList;
        //    for (int i = 1; i <= rowCount; i++)
        //    {

        //        machineMtnContentList = new MachineMtnContentList();
        //        machineMtnContentList.MachineID = machineMtnFrequency.MachineID;
        //        machineMtnContentList.FrequencyID = machineMtnFrequency.FrequencyID;
        //        machineMtnContentList.MachinePart = xlRange.Cells[i, 1].Value2.ToString();
        //        machineMtnContentList.ContentMtn = xlRange.Cells[i, 2].Value2.ToString();
        //        machineMtnContentList.ToolMtn = xlRange.Cells[i, 3].Value2.ToString();
        //        machineMtnContentList.MethodMtn = xlRange.Cells[i, 4].Value2.ToString();
        //        Insert(machineMtnContentList);
        //    }           
        //}
    }
}
