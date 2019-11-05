using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OfficeOpenXml;
using System.IO;
namespace TestABC.Models.Data
{
    public partial class MachineMtnReportDatas
    {
        private delegate void delegate_BuildExcelHandler(MachineMtnReportDataSearch machineMtnReportDataSearch, IEnumerable<MachineMtnReportData> data, ExcelPackage ep, List<BuildArgs> buildArgs);

        //public void CreateAllExcels(MachineMtnReportDataSearch machineMtnReportDataSearch)
        //{
        //    #region get data.
        //    MachineMtnReportDatasDB machineMtnReportDatasDB = new MachineMtnReportDatasDB();
        //    ReturnMachineMtnReportData reportData = machineMtnReportDatasDB.getData(machineMtnReportDataSearch);
        //    #endregion

        //    IEnumerable<MachineMtnReportData> data = reportData.lstMachineMtnReportData.AsEnumerable<MachineMtnReportData>();
        //    CreateExcel(machineMtnReportDataSearch, data, "MachineMtnReport_onlyGroup.xlsx",
        //        MachineMtnReportDatas_GetAllValues
        //    );
        //}

        //private void CreateExcel(MachineMtnReportDataSearch machineMtnReportDataSearch, IEnumerable<MachineMtnReportData> data, string fileName, params delegate_BuildExcelHandler[] BuildExcels)
        //{
        //    byte[] excel = GetExcelData(machineMtnReportDataSearch, data, BuildExcels);
        //    SaveExcelToFile(fileName, excel);
        //}
        //private byte[] GetExcelData(MachineMtnReportDataSearch machineMtnReportDataSearch, IEnumerable<MachineMtnReportData> data, delegate_BuildExcelHandler[] myDelegate_BuildExcels)
        //{
        //    using (var excelPackage = new ExcelPackage())
        //    {
        //        List<BuildArgs> buildArgs = new List<BuildArgs>();
        //        foreach (var BuildExcel in myDelegate_BuildExcels)
        //            BuildExcel(machineMtnReportDataSearch, data, excelPackage, buildArgs);
        //        return excelPackage.GetAsByteArray();
        //    }
        //}

        public System.Web.Mvc.FileStreamResult CreateAllExcels_2(MachineMtnReportDataSearch machineMtnReportDataSearch)
        {
            #region get data.
            MachineMtnReportDatasDB machineMtnReportDatasDB = new MachineMtnReportDatasDB();
            ReturnMachineMtnReportData reportData = machineMtnReportDatasDB.getData(machineMtnReportDataSearch);
            #endregion
            IEnumerable<MachineMtnReportData> data = reportData.lstMachineMtnReportData.AsEnumerable<MachineMtnReportData>();

            List<delegate_BuildExcelHandler> buildExcels = new List<delegate_BuildExcelHandler>();
            string fileName = "";
            fileName += "BaoTriMay";
            if (machineMtnReportDataSearch.FrequencyID == SMCommon.MachineMtnFrequency_Daily)
            {
                fileName += "_HangNgay";
                buildExcels.Add(MachineMtnReportDatas_GetExcel_Daily);
            }

            fileName += "MachineID_" + machineMtnReportDataSearch.MachineID;
            fileName += "MachineName_" + machineMtnReportDataSearch.MachineName;
            fileName += "_" + DateTime.Now.ToString("MM.dd.yyyy.HH.mm.ss");
            fileName += ".xlsx";
            return CreateExcel_2(machineMtnReportDataSearch, data, fileName, buildExcels.ToArray());
        }
        private System.Web.Mvc.FileStreamResult CreateExcel_2(MachineMtnReportDataSearch machineMtnReportDataSearch, IEnumerable<MachineMtnReportData> data, string fileName, params delegate_BuildExcelHandler[] BuildExcels)
       { 
            var package = GetExcelData_2(machineMtnReportDataSearch, data, BuildExcels);
            var fileDownloadName = fileName;
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            var fileStream = new MemoryStream();
            package.SaveAs(fileStream);
            fileStream.Position = 0;

            var fsr = new System.Web.Mvc.FileStreamResult(fileStream, contentType);
            fsr.FileDownloadName = fileDownloadName;
            return fsr;
        }
        private ExcelPackage GetExcelData_2(MachineMtnReportDataSearch machineMtnReportDataSearch, IEnumerable<MachineMtnReportData> data, delegate_BuildExcelHandler[] myDelegate_BuildExcels)
        {
            try
            {
                var excelPackage = new ExcelPackage();

                    List<BuildArgs> buildArgs = new List<BuildArgs>();
                    foreach (var BuildExcel in myDelegate_BuildExcels)
                        BuildExcel(machineMtnReportDataSearch, data, excelPackage, buildArgs);
                    return excelPackage;
            }
            catch (Exception ex)
            {
                throw ex; }
        }
        #region Save file after filling data.
        private void SaveExcelToFile(string fileName, byte[] excel)
        {
            try
            {
                string outputPath = GetOutputPath(fileName);
                File.WriteAllBytes(outputPath, excel);
                Console.WriteLine("Saved {0}", fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to save {0}. {1}", fileName, ex.Message);
            }
        }
        private string GetOutputPath(string fileName)
        {
            string outputPath = Path.GetFullPath(Path.Combine(
                Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                "..", "..", "..", "Output",
                fileName
            ));
            string directory = Path.GetDirectoryName(outputPath);
            if (Directory.Exists(directory) == false)
                Directory.CreateDirectory(directory);
            return outputPath;
        }
        #endregion

    }
}