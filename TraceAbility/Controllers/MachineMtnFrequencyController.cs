using System.Web;
using System.IO;
using System.Web.Mvc;
using TestABC.Models.Data;
using System;
using System.Data;
using OfficeOpenXml;
using System.Linq;

namespace TestABC.Controllers
{
    public class MachineMtnFrequencyController : Controller
    {
        // GET: MachineFrequence
        public ActionResult Index()
        {
            return View();
        }
        MachineMtnFrequencyDB machineMtnFrequencyDB = new MachineMtnFrequencyDB();

        public JsonResult List()
        {
            JsonResult x = Json(machineMtnFrequencyDB.MachineMtnFrequencyAll().lstMachineMtnFrequencyView, JsonRequestBehavior.AllowGet);

            return Json(machineMtnFrequencyDB.MachineMtnFrequencyAll().lstMachineMtnFrequencyView, JsonRequestBehavior.AllowGet);   
        }
        public JsonResult GetbyMachineID(string MachineID)
        {
            return Json((machineMtnFrequencyDB.GetbyMachineID(MachineID)).lstMachineMtnFrequency[0], JsonRequestBehavior.AllowGet); 
        }
        public JsonResult Insert(MachineMtnFrequency machineMtnFrequency)
        {
            return Json(machineMtnFrequencyDB.Insert(machineMtnFrequency), JsonRequestBehavior.AllowGet);
        }
        //public JsonResult InsertWithFileImport()
        //{
        //    string tenfile = "";
        //    if (Request.Files.Count > 0)
        //    {
        //        try
        //        {
        //            HttpPostedFileBase file = Request.Files[0];
        //            string fname;

        //            // Checking for Internet Explorer  
        //            //if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
        //            //{
        //            //    string[] testfiles = file.FileName.Split(new char[] { '\\' });
        //            //    fname = testfiles[testfiles.Length - 1];
        //            //}
        //            //else
        //            //{
        //            //    fname = file.FileName;
        //            //}
        //            string strExtension = Path.GetExtension(file.FileName);
        //            string strPrefixName = Path.GetFileNameWithoutExtension(file.FileName);
        //            strPrefixName += DateTime.Now.ToString("yyyyMMdd_HHmm");
        //            string fullName = strPrefixName + strExtension;

        //            // Get the complete folder path and store the file inside it.  
        //            fname = Path.Combine(Server.MapPath("~/Uploads/"), fullName);
        //            file.SaveAs(fname);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }


        //    //    if (imageFile.ContentLength > 0)
        //    //{
        //    //    var fileName = Path.GetFileName(imageFile.FileName);
        //    //    var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
        //    //    imageFile.SaveAs(path);
        //    //}
        //    //return Json(machineMtnFrequencyDB.Insert(machineMtnFrequency), JsonRequestBehavior.AllowGet);
        //    return Json("");
        //}
        public JsonResult InsertWithFileImport()
        {
            //MachineTypeFrequency machineTypeFrequency = Newtonsoft.Json.JsonConvert.DeserializeObject<MachineTypeFrequency>(Request.Form[0]);
            //ReturnMachineTypeFrequency _returnMachineMtnFrequency = machineMtnFrequencyDB.Insert(machineTypeFrequency);
            //if(_returnMachineMtnFrequency.Code == "99")
            //    return Json(_returnMachineMtnFrequency, JsonRequestBehavior.AllowGet);
            
            ////xu ly file nhap vao.
            //string tenfile = "";
            //string fullPath_filename  = "";
            //string sheetName = "";
            //if (Request.Files.Count > 0)
            //{
            //    try
            //    {
            //        HttpPostedFileBase httpFile = Request.Files[0];

            //        //Checking for Internet Explorer
            //        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
            //            {
            //                string[] testfiles = httpFile.FileName.Split(new char[] { '\\' });
            //                fullPath_filename = testfiles[testfiles.Length - 1];
            //            }
            //            else
            //            {
            //                fullPath_filename = httpFile.FileName;
            //            }
            //        string strExtension = Path.GetExtension(httpFile.FileName);
            //        string strPrefixName = Path.GetFileNameWithoutExtension(httpFile.FileName);
            //        //rename file.
            //        strPrefixName += DateTime.Now.ToString("yyMMddHHmmss");
            //        string fullName = strPrefixName + strExtension;

            //        // Get the complete folder path and store the file inside it.  
            //        fullPath_filename = Path.Combine(Server.MapPath("~/Uploads/MachineMaintanance"), fullName);
            //        httpFile.SaveAs(fullPath_filename);
            //    }
            //    catch (Exception ex)
            //    {
            //        throw ex;
            //    }
            //}

            //#region extract excel to datatable.
            //// Khởi tạo data table
            //DataTable dtResult;
            //dtResult = ExtractExcelToDataTable(fullPath_filename, sheetName);
            //#endregion

            //#region import datatable to DB
            //MachineMtnContentListDB machineMtnContentListDB = new MachineMtnContentListDB();
            //MachineMtnContentList machineMtnContentList = new MachineMtnContentList();
            //machineMtnContentList.MachineID = machineMtnFrequency.MachineID;
            //machineMtnContentList.FrequencyID = machineMtnFrequency.FrequencyID;

            //if (dtResult != null)
            //{
            //    if (dtResult.Rows.Count > 0)
            //    {
            //        foreach (DataRow dr in dtResult.Rows)
            //        {
            //            try
            //            {
            //                machineMtnContentList.MachinePart = dr[0].ToString().Trim(); //Item bộ phận bảo dưỡng.
            //                machineMtnContentList.ContentMtn = dr[1].ToString().Trim(); //Checking_Content noi dung bao duong.
            //                machineMtnContentList.ToolMtn = dr[2].ToString().Trim();    //Component
            //                machineMtnContentList.MethodMtn = dr[3].ToString().Trim(); //Standard
            //                machineMtnContentListDB.Insert(machineMtnContentList);
            //            }
            //            catch (Exception ex)
            //            {
            //                throw ex;
            //            }
            //        }    
            //    }
            //}
            //#endregion

            ////delete file after export.
            //if (System.IO.File.Exists(fullPath_filename))
            //    System.IO.File.Delete(fullPath_filename);

            //return Json(machineMtnFrequencyDB.Insert(machineMtnFrequency), JsonRequestBehavior.AllowGet);
            return Json("");
        }

        //public DataTable ExtractExcelToDataTable(string fullPath_FileName, string sheetName = "")
        //{
        //    DataTable dt = new DataTable();
        //    // Load file excel và các setting ban đầu
        //    try
        //    {
        //        using (ExcelPackage package = new ExcelPackage(new FileInfo(fullPath_FileName)))
        //        {
        //            ExcelWorkbook t1 = package.Workbook;
        //            ExcelWorksheets x1 = t1.Worksheets;
        //            int y = x1.Count;

        //            if (package.Workbook.Worksheets.Count < 1)
        //            {
        //                // Log.
        //                return null;
        //            }
        //            // Khởi Lấy Sheet đầu tiên trong file Excel để truy vấn, truyền vào name của Sheet để lấy ra sheet cần, nếu name = null thì lấy sheet đầu tiên
        //            ExcelWorksheet workSheet = package.Workbook.Worksheets.FirstOrDefault(x => x.Name == sheetName) ?? package.Workbook.Worksheets.FirstOrDefault();
        //            //đếm số cột. 
        //            int TotalColumn_Count = 0;
        //            // Đọc tất cả các header (header row 4).
        //            foreach (var firstRowCell in workSheet.Cells[4, 1, 1, workSheet.Dimension.End.Column])
        //            {
        //                if (firstRowCell.Text.Trim() != "")
        //                {
        //                    TotalColumn_Count += 1;
        //                    dt.Columns.Add(firstRowCell.Text);
        //                }
        //            }
        //            // Đọc tất cả data bắt đầu từ row thứ 5.
        //            for (var rowNumber = 5; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
        //            {
        //                // Lấy 1 row trong excel để truy vấn
        //                var excelRow = workSheet.Cells[rowNumber, 1, rowNumber, TotalColumn_Count]; // workSheet.Dimension.End.Column];
        //                                                                                            // tạo 1 row trong data table
        //                var newRow = dt.NewRow();
        //                foreach (var cell in excelRow)
        //                {
        //                    newRow[cell.Start.Column - 1] = cell.Text;

        //                }
        //                dt.Rows.Add(newRow);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return dt;
        //}

        public JsonResult Delete(string MachineID)
        {
            return Json(machineMtnFrequencyDB.DeleteByMachineID(MachineID), JsonRequestBehavior.AllowGet);
        }
        
    }
}