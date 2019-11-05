using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestABC.Models.Data
{
    public class MachineType
    {
        public int ID { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }        
        public bool isActive { get; set; }
    }
    public class ReturnMachineType
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public int Total { get; set; }

        public MachineType aMachineType { get; set; }

        public List<MachineType> lstMachineType;
    }
}