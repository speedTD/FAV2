using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestABC.Models.Data
{
    public class FPBCheckingItem
    {
        public int IDFPBCheckingItem { get; set; }
        public string MachineID { get; set; }
        public string CheckingItemName { get; set; }
        public int FrequencyID { get; set; }
        public bool isActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class ReturnFPBCheckingItem
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public int Total { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public FPBCheckingItem aFPBCheckingItem { get; set; }

        public List<FPBCheckingItem> LstFPBCheckingItem;
    }
    public class FPBCheckingItemView
    {
        public string MachineID { get; set; }
        public string CheckingItemName { get; set; }
        public int Frequency1 { get; set; }
        public int Frequency2 { get; set; }
        public int Frequency3 { get; set; }
    }
}