using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestABC.Models.Data
{
    public class FPB
    {
        public int ID { get; set; }
        public int IDFPBCheckingItem { get; set; }
        public DateTime FPBDate { get; set; }
        public string IndicationNo { get; set; }       
        public string UserID { get; set; }
        public string SeqNo { get; set; }
        public string BlockID { get; set; }
        public string Result { get; set; }
        public string ResultContent { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class ReturnFPB
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public int Total { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public FPB aFPB { get; set; }

        public List<FPB> LstFPB;
    }
}