using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestABC.Models.Data
{
    public class ProgramName
    {
        public string programName { get; set; }
        public string ProgramType { get; set; }
        public string FactoryID { get; set; }

        public int OperatorID { get; set; }
        public string OperatorName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class ReturnProgramName
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public int Total { get; set; }
        public int userID { get; set; }
        public string UserName { get; set; }
        public ProgramName aProgramName { get; set; }

        public List<ProgramName> lstProgramName;
    }
}