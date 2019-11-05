using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestABC.Models.Data
{
    public class ProgramPdtCtrl
    {
        public int ID { get; set; }
        public string ProgramName { get; set; }
        public string Part { get; set; }
        public string ControlItem { get; set; }
        public string ColumnName { get; set; }
        public string SpecDisplay { get; set; }
        public string Unit { get; set; }
        public string LowerLimit { get; set; }
        public string UpperLimit { get; set; }
        public int OperatorID { get; set; }
        public string OperatorName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class ReturnProgramPdtCtrl
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public int Total { get; set; }
        public int userID { get; set; }
        public string UserName { get; set; }
        public ProgramPdtCtrl aProgramPdtCtrl { get; set; }
        public int TotalPage { get; internal set; }
        public int PageNumber { get; set; }

        public List<ProgramPdtCtrl> lstProgramPdtCtrl;
        public PermisionControllerVM permisionControllerVM;
    }
    
    public class Import_ProgramPdtCtrl
    {
        public int ID { get; set; }
        public string FactoryID { get; set; }
        public string ProgramType { get; set; }
        public string ProgramName { get; set; }
        public string Part { get; set; }  //gia tri import tu file excel.
        public string ControlItem { get; set; }
        public string ColumnName { get; set; }
        public string SpecDisplay { get; set; }
        public string Unit { get; set; }
        public string LowerLimit { get; set; }
        public string UpperLimit { get; set; }
        public int OperatorID { get; set; }
        public string OperatorName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}