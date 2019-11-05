using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestABC.Models.Data
{

    public class Line
    {
        public string LineID { get; set; }
        public string LineName { get; set; }
        public string FactoryID { get; set; }
        public bool isActive { get; set; }
    }
    public class ReturnLine
    {
        public string Code { get; set; }

        public string Message { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public int Total { get; set; }
        public int TotalPage { get; set; }
        public Line aLine { get; set; }

        public List<Line> LstLine;
    }

}