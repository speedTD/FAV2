using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestABC.Models.ProductionControl
{
    public class ProductionControlDetail
    {
        public int ID { get; set; }
        public int ProductionControlID { get; set; }
        public string ProgramName { get; set; }
        public int ProgramPdtCtrlID { get; set; }
        public string Part { get; set; } = ""; // Part = "" for using in where.
        public string ControlItem { get; set; }
        public string ColumnName { get; set; }
        public string SpecDisplay { get; set; }
        public string Unit { get; set; }
        public string LowerLimit { get; set; }
        public string UpperLimit { get; set; }
        public string ActualValue { get; set; }
        public string Result { get; set; }
        public DateTime ResultDate { get; set; }
        public string ResultContent { get; set; }
    }
    public class ReturnProductionControlDetail
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public int Total { get; set; }
        public ProductionControlDetail aProductionControlDetail { get; set; }

        public List<ProductionControlDetail> lstProductionControlDetail;
    }
}