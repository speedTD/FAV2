//using System;
//using System.IO;
//using System.Web.Mvc;

//namespace TraceLibs
//{
//    public static class SMHelper
//    {
//        public static String RenderViewToString(ControllerContext context, String viewPath, object model = null)
//        {
//            if(model !=null)
//            context.Controller.ViewData.Model = model;
//            using (var sw = new StringWriter())
//            {
//                var viewResult = ViewEngines.Engines.FindView(context, viewPath, null);
//                var viewContext = new ViewContext(context, viewResult.View, context.Controller.ViewData, context.Controller.TempData, sw);
//                viewResult.View.Render(viewContext, sw);
//                viewResult.ViewEngine.ReleaseView(context, viewResult.View);
//                return sw.GetStringBuilder().ToString();
//            }
//        }
//    }
//}
