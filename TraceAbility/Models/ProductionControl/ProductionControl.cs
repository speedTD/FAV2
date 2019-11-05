using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestABC.Models.Data
{
    public class ProductionControl
    {
        public int ID { get; set; }
        public string IndicationID { get; set; }
        public string MachineID { get; set; }
        public string MachineName { get; set; }
        public DateTime PdtCtrlDateTime { get; set; }
        public string UserID { get; set; }  // = operatorID
        public string UserName { get; set; } // = operatorName
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public string BatchNo { get; set; }
        public string SeqNo { get; set; }
        public string Result { get; set; }
        public string ProgramName { get; set; }
    }
    public class ReturnProductionControl
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public int Total { get; set; }
        public int LastID { get; set; }
        public int userID { get; set; }
        public string UserName { get; set; }
        public ProductionControl aProductionControl { get; set; }

        public List<ProductionControl> lstProductionControl;
        public PermisionControllerVM permisionControllerVM;
    }
    public class SearchProductionControl
    {

        public int ID { get; set; }
        public string IndicationID { get; set; }
        public string MachineID { get; set; }
        public string MachineName { get; set; }
        public DateTime PdtCtrlDateTime { get; set; }
        public string UserID { get; set; }  // = operatorID
        public string UserName { get; set; } // = operatorName
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public string BatchNo { get; set; }
        public string SeqNo { get; set; }
        public string Result { get; set; }
        public string ProgramName { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

    }
}