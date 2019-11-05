using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OfficeOpenXml;


namespace TestABC.Models.Data
{
    //public class MachineMtnReportData_DataRow
    //{
    //    //step 1. danh sach cot. 
    //}
    
    public enum BuildAction
    {
        None = 0,
        Merge,
        AutoFilter,
        FreezePanes,
        ColumnClustered,
        PieExploded3D
    }
    public enum Frequency
    {
        //MachinetMtnReport step 2. gan tan xuat.
        Daily = 1,
        Monthly = 2,
        AutoFilter,
        FreezePanes,
        ColumnClustered,
        PieExploded3D
    }
    public class BuildArgs
    {
        public BuildAction Action { get; set; }
        public ExcelWorksheet Worksheet { get; set; }
        public int FromRow { get; set; }
        public int FromColumn { get; set; }
        public int ToRow { get; set; }
        public int ToColumn { get; set; }
        public BuildChart Chart { get; set; }
        public string Note { get; set; }
    }
    public class BuildChart
    {
        public string Title { get; set; }
        public List<BuildChartSerie> Series { get; set; }
        public int Row { get; set; }
        public int RowOffsetPixels { get; set; }
        public int Column { get; set; }
        public int ColumnOffsetPixels { get; set; }
    }
    public class BuildChartSerie
    {
        public string Header { get; set; }
        public string SerieAddress { get; set; }
        public string XSerieAddress { get; set; }
    }

    public class MachineMtnReportDataSearch {
        public string MachineID { get; set; }
        public string MachineName { get; set; }
        public int OperatorID { get; set; }
        public int Shift { get; set; }
        public int FrequencyID { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }
        public int FromInt { get; set; } // for : frequency Year, Month, Week.
        public int ToInt { get; set; }
    }
    public class MachineMtnReportData
    {
        //public int ID { get; set; }
        public string MachineID { get; set; }
        public string MachineName { get; set; }
        public string OperatorID { get; set; }
        public string OperatorName { get; set; }
        public short Shift { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public short FrequencyID { get; set; }
        public string TotalResult { get; set; }
        public string TotalResultContents { get; set; }
        public int Week { get; set; }
        public short Month { get; set; }
        public int Year { get; set; }
        public int MachinePartID { get; set; }
        public string MachinePartName { get; set; }
        public string ContentMtn { get; set; }
        public string ToolMtn { get; set; }
        public string MethodMtn { get; set; }

        public string Standard { get; set; }
        public string MtnDetailResult { get; set; }
        public string MtnDetailResultContents { get; set; }
        
        public int CheckerID { get; set; }
        public string CheckerName { get; set; }
        public string CheckerResult { get; set; }
    }
    public class ReturnMachineMtnReportData
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public int Total { get; set; }
        public int LastID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public MachineMtnReportData aMachineMtnReportData { get; set; }

        public List<MachineMtnReportData> lstMachineMtnReportData;
        public int TotalPage { get; set; }
        public PermisionControllerVM permisionControllerVM { get; set; }
       
    }

    
}