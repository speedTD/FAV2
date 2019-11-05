using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestABC.Models.Data

{
    public class ToolIQC
    {
        public string ID { get; set; }
        public int ToolTypeID { get; set; }
        public string ToolTypeName { get; set; }
        public string PrefixToolID { get; set; }
        public string FromToolID { get; set; }
        public string ToToolID { get; set; }
        public string FileUrl { get; set; }
        public string FactoryID { get; set; }

        public int OperatorID { get; set; }
        public string OperatorName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class ReturnToolIQC
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public int Total { get; set; }
        public int userID { get; set; }
        public string UserName { get; set; }
        public ToolIQC aToolIQC { get; set; }

        public List<ToolIQC> lstToolIQC;
    }
}