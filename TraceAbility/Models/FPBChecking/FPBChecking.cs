using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestABC.Models.FPBChecking
{
    public class FPBChecking
    {
        public int ID { get; set; }
        public DateTime FPBCheckingDate { get; set; }
        public string IndicationNo { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public string BatchNo { get; set; }
        public string MachineID { get; set; }
        public string MachineName { get; set; }
        public string UserID { get; set; }
        public string SeqNo { get; set; }
        public string BlockID { get; set; }
        public string Result { get; set; }
        public string ResultContent { get; set; }
        public string Images { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class ReturnFPBChecking
    {
        public string Code { get; set; }

        public string Message { get; set; }
        public int Total { get; set; }
        public string UserName { get; set; }
        public int UserID { get; set; }
        public int LastID { get; set; }
        public int TotalPage { get; internal set; }
        public int PageNumber { get; set; }
        public FPBChecking aFPBChecking { get; set; }

        public List<FPBChecking> LstFPBChecking;
    }
}