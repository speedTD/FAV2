using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestABC.Models.Data
{
    public class ProductionList
    {
        public string IndicatorID { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public string BatchNo { get; set; }
        public string ProgramName { get; set; }
        public string LineID { get; set; }
        public int MachineTypeID { get; set; }
        public string MachineTypeName { get; set; }
        public string MachineName { get; set; }
        public string PatternCode { get; set; }
    }
    public class ReturnProductionList
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public int Total { get; set; }

        public ProductionList aProductionList { get; set; }

        public List<ProductionList> lstProductionList;
    }
}