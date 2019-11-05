using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestABC.Models.Data
{
    public class MachinePart
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MachineType { get; set; }    // is MachineTypeID
        public string TypeName { get; set; }
        public DateTime DateCreated { get; set; }
    }
    public class ReturnMachinePart
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public int Total { get; set; }

        public MachinePart aMachinePart { get; set; }

        public List<MachinePart> lstMachinePart;
    }
}