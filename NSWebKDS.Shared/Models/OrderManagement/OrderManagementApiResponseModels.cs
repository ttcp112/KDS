using NSWebKDS.Shared.Models.FilterModel;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NSWebKDS.Shared.Models.OrderManagement
{
    public class OrderManagementApiResponseModels : RequestBaseModels
    {
        #region//Get OrderManagement Response
        public bool IsSummary { get; set; }
        public List<OrderManagementModels> ListData { get; set; }
        public List<SummaryItemModels> ListSummary { get; set; }
        //public List<ZoneModel> ListZone { get; set; }
        //public List<TableModel> ListTable { get; set; }
        //public List<PrinterModel> ListPrinter { get; set; }
        //public List<SelectListItem> ListType { get; set; }

        #endregion

        #region//Update OrderManagement Response
        //# RESPONSE
        //true/false
        #endregion

        public OrderManagementApiResponseModels()
        {
            ListData = new List<OrderManagementModels>();
            ListSummary = new List<SummaryItemModels>();
            //ListZone = new List<ZoneModel>();
            //ListTable = new List<TableModel>();
            //ListPrinter = new List<PrinterModel>();
            //ListType = new List<SelectListItem>()
            //{
            //    new SelectListItem { Text = Commons.TypeDineIn, Value = Commons.EItemFilter.DineIn.ToString("d")},
            //    new SelectListItem { Text = Commons.TypeTakeAway, Value = Commons.EItemFilter.TakeAway.ToString("d")}
            //};
        }
    }
}
