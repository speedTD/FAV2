using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestABC.Models.Data;
namespace TestABC.Controllers
{
    public class MachineMtnReportDataController : Controller
    {
        MachineMtnReportDatasDB machineMtnReportDatasDB = new MachineMtnReportDatasDB();
        public ActionResult Report()
        {
            return View();
        }
        //public JsonResult CreateReport(MachineMtnReportDataSearch machineMtnReportDataSearch) {
        //    //MachineMtnReportDataSearch machineMtnReportDataSearch = new MachineMtnReportDataSearch();
        //    //machineMtnReportDataSearch.FrequencyID = 1;
        //    //machineMtnReportDataSearch.FromDate = new DateTime(2019,7,1);
        //    //machineMtnReportDataSearch.ToDate = new DateTime(2019, 8, 30);
        //    //machineMtnReportDataSearch.MachineID = "MOMT007";
        //    ReturnMachineMtnReportData returnMachineMtnReportData = machineMtnReportDatasDB.getData(machineMtnReportDataSearch);
        //    //test createreport.
        //    MachineMtnReportDatas x = new MachineMtnReportDatas();
        //    x.CreateAllExcels(machineMtnReportDataSearch);
        //    return Json(returnMachineMtnReportData, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult CreateReport_2(MachineMtnReportDataSearch machineMtnReportDataSearch)
        {
            MachineMtnReportDatas x = new MachineMtnReportDatas();
            return x.CreateAllExcels_2(machineMtnReportDataSearch);
        }
    }
}