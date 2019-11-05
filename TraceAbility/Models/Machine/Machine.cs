using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestABC.Models.Data
{
    #region Machine 
    public class Machine
    {
        public string MachineID { get; set; }
        public string MachineName { get; set; }
        public string MachineNumber { get; set; }
        public string Area { get; set; }
        public string Section { get; set; }
        public DateTime ReceiveDate { get; set; }
        public string Maker { get; set; }
        public string SerialNumber { get; set; }
        public int MachineTypeID { get; set; }
        public string MachineTypeName { get; set; }
        public string LineID { get; set; }
        public bool isActive { get; set; }

    }

    public class ReturnMachine
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public int Total { get; set; }

        public Machine aMachine { get; set; }

        public List<Machine> lstMachine;
    }

    public class MachineMtnFrequency
    {
        public int ID { get; set; }
        public string MachineID { get; set; }
        public short FrequencyID { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class MachineTypeMtnFrequency
    {
        public int ID { get; set; }
        public string MachineTypeID { get; set; }
        public short FrequencyID { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    #endregion Machine 

    public class MachineMtnContentList
    {
        public int ID { get; set; }
        public int MachineTypeID { get; set; }
        public int MachinePartID { get; set; }
        public string MachineTypeName { get; set; }
        public string MachinePartName { get; set; }
        public string ContentMtn { get; set; }
        public string ToolMtn { get; set; }
        public string MethodMtn { get; set; }
        public string Standard { get; set; }
        public short FrequencyID { get; set; }
        public bool IsActive { get; set; }

        public string Min { get; set; }
        public string Max { get; set; }
    }
    public class ReturnMachineMtnContentList
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public int Total { get; set; }
        public int TotalPage { get; set; }
        public int PageNumber { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public MachineMtnContentList aMachineTypeMtnContentList { get; set; }
        public List<MachineMtnContentList> lstMachineMtnContentList;
    }
    public class Import_MachineMtnType_ContentList
    {
        public int ID { get; set; }
        public int MachineTypeID { get; set; }
        public string MachinePart { get; set; }  //gia tri import tu file excel.
        public string ContentMtn { get; set; }
        public string ToolMtn { get; set; }
        public string MethodMtn { get; set; }
        public string Standard { get; set; }
        public short FrequencyID { get; set; }
        public string Min { get; set; }
        public string Max { get; set; }
    }
    //public class MachineMtnContentList
    //{
    //    public string MachineID { get; set; }
    //    public string MachinePart { get; set; }
    //    public string ContentMtn { get; set; }
    //    public string ToolMtn { get; set; }
    //    public string MethodMtn { get; set; }
    //    public short FrequencyID { get; set; }
    //    public int ID { get; set; }
    //}
    //public class ReturnMachineMtnContentList
    //{
    //    public string Code { get; set; }

    //    public string Message { get; set; }

    //    public int Total { get; set; }

    //    public MachineMtnContentList aMachineMtnContentList { get; set; }

    //    public List<MachineMtnContentList> lstMachineMtnContentList;
    //}


    public class MachineMtnFrequencyImport
    {
        public int ID { get; set; }
        public string MachineID { get; set; }
        public short FrequencyID { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class ReturnMachineMtnFrequency
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public int Total { get; set; }

        public MachineMtnFrequency aMachineMtnFrequency { get; set; }

        public List<MachineMtnFrequency> lstMachineMtnFrequency;
    }
    public class MachineMtnFrequencyView
    {
        public int ID { get; set; }
        public string MachineID { get; set; }
        public string MachineName { get; set; }
        public int Daily { get; set; }
        public int Weekly { get; set; }
        public int Monthly { get; set; }
        public int ThreeMonths { get; set; }
        public int SixMonths { get; set; }
        public int Yearly { get; set; }
    }
    public class ReturnMachineMtnFrequencyView
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public int Total { get; set; }

        public MachineMtnFrequencyView aMachineMtnFrequencyView { get; set; }

        public List<MachineMtnFrequencyView> lstMachineMtnFrequencyView;
    }
    public class MachineMtn
    {
        public int ID { get; set; }
        public string MachineID { get; set; }
        public string MachineName { get; set; }
        public string OperatorID { get; set; }
        public string OperatorName { get; set; }
        public short Shift { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public short FrequencyID { get; set; }
        public string Result { get; set; }
        public string ResultContents { get; set; }
        public int Week { get; set; }
        public short Month { get; set; }
        public int Year { get; set; }
        public int CheckerID { get; set; }
        public string CheckerName { get; set; }
        public string CheckerResult { get; set; }
    }
    public class ReturnMachineMtn
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public int Total { get; set; }
        public int LastID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public MachineMtn aMachineMtn { get; set; }

        public List<MachineMtn> lstMachineMtn;
        public int TotalPage { get; set; }
        public PermisionControllerVM permisionControllerVM { get; set; }
    }
    
    public class MachineMtnDetail
    {
        public int ID { get; set; }
        public int MachineMtnID { get; set; }
        public int MachineMtnContentID { get; set; }
        public string Result { get; set; }
        public string ResultContents { get; set; }
        public string ContentMtn { get; set; }
        public string ToolMtn { get; set; }
        public string MethodMtn { get; set; }
        public string Standard { get; set; }

        public string Min { get; set; }
        public string Max { get; set; }
        public string ActualValue { get; set; }

    }
    public class ReturnMachineMtnDetail
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public int Total { get; set; }

        public MachineMtnDetail aMachineMtnDetail { get; set; }

        public List<MachineMtnDetail> lstMachineMtnDetail;
        public PermisionControllerVM permisionControllerVM { get; set; }
    }
    public class MachineMtnContentDetail
    {
        public int ID { get; set; }
        public int MachineMtnID { get; set; }
        public int MachineMtnContentID { get; set; }
        public string Result { get; set; }
        public string ResultContents { get; set; }
        public string MachinePart { get; set; }
        public string MachinePartName { get; set; }
        public string ContentMtn { get; set; }
        public string ToolMtn { get; set; }
        public string MethodMtn { get; set; }
        public string Standard { get; set; }
        public string Min { get; set; }
        public string Max { get; set; }
        public string ActualValue { get; set; }
    }
    public class ReturnMachineMtnContentDetail
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public int Total { get; set; }
        public MachineMtnContentDetail aMachineMtnContentDetail { get; set; }
        public List<MachineMtnContentDetail> lstMachineMtnContentDetail;
        public PermisionControllerVM permisionControllerVM { get; set; }
    }

    
    #region MachineType
    public class MachineTypeFrequency
    {
        public int ID { get; set; }
        public int MachineTypeID { get; set; }
        public short FrequencyID { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class ReturnMachineTypeFrequency
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public int Total { get; set; }

        public MachineTypeFrequency aMachineTypeFrequency { get; set; }

        public List<MachineTypeFrequency> lstMachineTypeFrequency;
    }
    public class MachineTypeFrequencyView
    {
        public int ID { get; set; }
        public string MachineTypeID { get; set; }
        public string MachineTypeName { get; set; }
        public int Daily { get; set; }
        public int Weekly { get; set; }
        public int Monthly { get; set; }
        public int ThreeMonths { get; set; }
        public int SixMonths { get; set; }
        public int Yearly { get; set; }
    }
    public class ReturnMachineTypeFrequencyView
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public int Total { get; set; }

        public MachineTypeFrequencyView aMachineTypeFrequencyView { get; set; }

        public List<MachineTypeFrequencyView> lstMachineTypeFrequencyView;
    }
    #endregion MachineType

    public class DataTablesRequest
    {
        //ColumnCollection Columns { get; set; }
        int Draw { get; set; }
        int Length { get; set; }
        //Search Search { get; set; }
        int Start { get; set; }

    }
    
}