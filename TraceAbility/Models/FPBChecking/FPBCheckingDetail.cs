using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestABC.Models.Data
{
    public class FPBCheckingDetail
    {
        public int ID { get; set; }
        public int FPBCheckingID { get; set; }
        //public int FPBCheckingItemID { get; set; }
        public string Images { get; set; }
        //public int FrequencyID { get; set; }
        public string CheckingItemName { get; set; }
        public string Result { get; set; }
        public string ResultContent { get; set; }
    }
    public class ReturnFPBCheckingDetail
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public int Total { get; set; }
        public FPBCheckingDetail aFPBCheckingDetail { get; set; }

        public List<FPBCheckingDetail> LstFPBCheckingDetail;
        public PermisionControllerVM permisionControllerVM { get; set; }
    }
}