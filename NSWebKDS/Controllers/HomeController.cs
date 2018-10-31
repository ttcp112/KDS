
using Newtonsoft.Json;
using NSWebKDS.Shared;
using NSWebKDS.Shared.Factory;
using NSWebKDS.Shared.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NSWebKDS.Controllers
{
    public class HomeController : Controller
    {
        private ActivateFactory _fac;
        public HomeController()
        {
            _fac = new ActivateFactory();
        }
        public ActionResult Index()
        {

            //NSLog.Logger.Info("abc....");
            return View();
        }

        [HttpPost]
        public JsonResult Active(ActivateModels model)
        {
            var data = new ActivateStoreModels();
            try
            {
                data = _fac.Activate(model);
                if(data != null && data.ListStore != null && data.ListStore.Any())
                {
                    var ParentStore = data.ListStore.GroupBy(x => new { x.CompanyId, x.CompanyName })
                        .Select(x => new  NSWebKDS.Shared.Models.StoreModels
                        {
                            CompanyId = x.Key.CompanyId,
                            CompanyName = x.Key.CompanyName
                        }).ToList();

                    ParentStore.ForEach(x =>
                    {
                        x.ChildStore = data.ListStore.Where(z => z.CompanyId.Equals(x.CompanyId)).ToList();
                        x.ChildStore.ForEach(o =>
                       {
                           var sExpiryDate = o.ExpiredDate == Commons.MaxDate ? "No expiry dates" : "Expiry Date: " + o.ExpiredDate.ToString("dd/MM/yyyy"); 
                           o.Name = "<div style=\"font-weight:bold;\">" + o.Name+ "</div><div class=\"divsmall\">&nbsp;&nbsp;&nbsp;<small> "+ sExpiryDate + "</small></div>";
                       });
                    });

                    data.ListStore = ParentStore;
                }
                else if(data != null && data.ListStore.Count == 0 && data.Success)
                {
                    // create cookie 
                    HttpCookie ActiveCookies = new HttpCookie("cookiesActive");
                    if(ActiveCookies != null)
                    {
                        ActiveCookies.Expires = DateTime.Now.AddMilliseconds(-1);
                    }
                    string myObjectJson = JsonConvert.SerializeObject(model);
                    ActiveCookies.Value = Server.UrlEncode(myObjectJson);
                    ActiveCookies.Expires = DateTime.MaxValue;
                    HttpContext.Response.Cookies.Add(ActiveCookies);
                }
            }
            catch(Exception ex)
            {
                NSLog.Logger.Error("Active controller", ex);
            }
            return Json(data);
        }

        [HttpGet]
        public void GetSettings()
        {
            if (Request.Cookies["cookiesActive"] != null)
            {
                var activate = Request.Cookies["cookiesActive"];
                var activateDecode = Server.UrlDecode(activate.Value);
                var activateValue = JsonConvert.DeserializeObject<ActivateModels>(activateDecode);

                CommonFunctions.ListenServer();
                CommonFunctions.GetListGeneralSetting(activateValue.StoreId);

                //CommonFunctions.RefreshOrder();
            }
        }
       

    }
}