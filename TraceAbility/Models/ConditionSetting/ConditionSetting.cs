using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestABC.Models.Data
{
    public class ConditionSetting
    {
        public int ID { get; set; }
        public string LineID { get; set; }
        public int MachineTypeID { get; set; }
        public string MachineTypeName { get; set; }
        public string PatternCode { get; set; }
        public string ControlItem { get; set; }
        public string SpecDisplay { get; set; }
        public string Unit { get; set; }
        public float LowerLimit { get; set; }
        public float UpperLimit { get; set; }
    }
    public class ReturnConditionSetting
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public int Total { get; set; }

        public ConditionSetting aConditionSetting { get; set; }

        public List<ConditionSetting> lstConditionSetting;
    }
}