using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestABC.Models.Data;

namespace TestABC.Controllers
{
    public class ProductionListController : Controller
    {
        // GET: ProductionList
        public ActionResult Index()
        {
            return View();
        }
        ProductionListDB productionListDB = new ProductionListDB();

        public JsonResult List()
        {
            JsonResult x = Json(productionListDB.ProductionListAll().lstProductionList, JsonRequestBehavior.AllowGet);

            return Json(productionListDB.ProductionListAll().lstProductionList, JsonRequestBehavior.AllowGet);   
        }

        public JsonResult GetbyID(string ID)
        {
            return Json((productionListDB.GetbyID(ID)).lstProductionList[0], JsonRequestBehavior.AllowGet); 
        }
        public JsonResult GetbyItemCode(string ItemCode)
        {
            return Json((productionListDB.GetbyItemCode(ItemCode)).lstProductionList[0], JsonRequestBehavior.AllowGet); 
        }
        public JsonResult CountbyID(string ID)
        {
            return Json((productionListDB.GetbyID(ID)).Total, JsonRequestBehavior.AllowGet); 
        }
        public JsonResult Insert(ProductionList productionList)
        {
            return Json(productionListDB.Insert(productionList), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(ProductionList productionList)
        {
            return Json(productionListDB.Insert(productionList), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(string ID)
        {
            return Json(productionListDB.DeleteByID(ID), JsonRequestBehavior.AllowGet);

        }
    }
}