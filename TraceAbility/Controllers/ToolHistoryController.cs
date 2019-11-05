using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestABC.Controllers
{
    public class ToolHistoryController : Controller
    {
        // GET: ToolHistory
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult HistorySearch()
        {
            return View();
        }
    }
}