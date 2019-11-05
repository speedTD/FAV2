using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestABC.Models.Data
{
    public class ToolList
    {
        public string ToolID { get; set; }
        public string UserID { get; set; }
       
        public string UserName { get; set; }
        public string ToolType { get; set; }
        public string ToolTypeName { get; set; }
        public string ItemCode { get; set; }
        public string Maker { get; set; }
        public string Specification { get; set; }
        public DateTime ReceiveDate { get; set; }
        public DateTime StartUsing { get; set; }

        public string LifeTime { get; set; }

        public DateTime ExpireDate { get; set; }
        // string  ExpireDate { get; set; }
        public string LineID { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool isActive { get; set; }
    }
    public class ReturnToolList
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public int Total { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public int PageNumber { get; set; }
        public int TotalPage { get; set; }
        public ToolList aToolList { get; set; }

        public List<ToolList> lstToolList;
    }

    public class ToolCleaningContent
    {
        public int ID { get; set; }
        public string ToolID { get; set; }
        public string LineUsing { get; set; }
        public int Shift { get; set; }
        public string Result { get; set; }
        public string NGContents { get; set; }
        public DateTime RepairDate { get; set; }
        public string RepairContents { get; set; }
        public string RepairID { get; set; }
        public string CheckBy { get; set; }
        public string ImageLink { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
    public class ReturnToolCleaningContent
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public int Total { get; set; }
        public int TotalPage { get; set; }
        public int PageNumber { get; set; }
        public ToolCleaningContent aToolCleaningContent { get; set; }

        public List<ToolCleaningContent> LstToolCleaningContent;
    }
    public class ToolTypeList
    {
        public string ToolTypeID { get; set; }
        public string ToolTypeName { get; set; }
        public bool isActive { get; set; }
    }
    public class ReturnToolTypeList
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public int Total { get; set; }

        public ToolTypeList aToolTypeList { get; set; }

        public List<ToolTypeList> LstToolTypeList;
    }

}