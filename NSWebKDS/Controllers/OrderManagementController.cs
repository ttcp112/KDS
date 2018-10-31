using Newtonsoft.Json;
using NSWebKDS.Shared;
using NSWebKDS.Shared.Factory;
using NSWebKDS.Shared.Models;
using NSWebKDS.Shared.Models.OrderManagement;
using System;
using System.Linq;
using System.Web.Mvc;


namespace NSWebKDS.Controllers
{
    public class OrderManagementController : Controller
    {
        private OrderManagementFactory _factory = null;

        public OrderManagementController()
        {
            _factory = new OrderManagementFactory();
        }

        // GET: OrderManagement
        public ActionResult Index()
        {
            if (!string.IsNullOrEmpty(Commons._StoreId))
            {
                return View();
            }
            else
                return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult GetFilter(OrderManagementClientModels _filterModel)
        {
            OrderManagementApiResponseModels result = new OrderManagementApiResponseModels();
            OrderManagementClientModels filterModel = new OrderManagementClientModels();
            try
            {
                filterModel = _filterModel;
                NSLog.Logger.Info("GetFilter_Request: ", filterModel);
                result = _factory.GetListData(filterModel);
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("GetFilter_Error: ", ex);
            }
            var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;// Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetListFilter(string StoreId)
        {
            FilterResponseModel result = new FilterResponseModel();
            FilterFactory _fac = new FilterFactory();
            try
            {
                result.ListTable = _fac.GetListTable(Commons._StoreId, Commons.AppKey, Commons.AppSecret);
                result.ListZone = _fac.GetListZone(Commons._StoreId, Commons.AppKey, Commons.AppSecret);
                result.ListPrinter = _fac.GetListPrinter(Commons._StoreId, Commons.AppKey, Commons.AppSecret);
                if (result.ListZone != null && result.ListZone.Any())
                {
                    result.ListZone = result.ListZone.OrderBy(x => x.Name).ToList();
                }
                if (result.ListTable != null && result.ListTable.Any())
                {
                    result.ListTable = result.ListTable.OrderBy(x => x.Name).ToList();
                }
                if (result.ListPrinter != null && result.ListPrinter.Any())
                {
                    result.ListPrinter = result.ListPrinter.OrderBy(x => x.PrinterName).ToList();
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("GetListFilter_Error: ", ex);
            }
            var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;// Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}