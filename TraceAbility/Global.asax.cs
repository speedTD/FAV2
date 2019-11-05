using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using log4net;
using StackExchange.Profiling;

namespace TestABC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //ViewEngines.Engines.Clear();
            //ViewEngines.Engines.Add(new RazorViewEngine());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            log4net.Config.XmlConfigurator.Configure();

        }
        protected void Session_Start()
        {
            //Session["UserLogin"] = null;
            Session["UserID"] = null;
            Session["UserName"] = null;
            Session["FactoryID"] = null;
            Session["MenuPermission"] = null;
            string sessionID = Session.SessionID;
        }
    }
}
