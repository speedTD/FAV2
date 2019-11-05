using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestABC.Models.Data
{
    public class ProgramPdtCtrlHistory
    {
        public string ProgramName { get; set; }
        public string Parameter { get; set; }
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
        public string ItemName { get; set; }
        public DateTime HistoryDate { get; set; }
        public int HistoryOperatorID { get; set; }
        public string HistoryOperatorName { get; set; }
        public string StatusCRUD { get; set; }

    }
    public class ReturnProgramPdtCtrlHistory
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public int Total { get; set; }
        public int userID { get; set; }
        public string UserName { get; set; }
        public ProgramPdtCtrlHistory aProgramPdtCtrlHistory { get; set; }
        public int TotalPage { get; internal set; }
        public int PageNumber { get; set; }

        public List<ProgramPdtCtrlHistory> lstProgramPdtCtrlHistory { get; set; }
        public PermisionControllerVM permisionControllerVM { get; set; }


    }
}