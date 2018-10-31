using NSWebKDS.Shared.Models.FilterModel;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NSWebKDS.Shared.Models.OrderManagement
{
    public class FilterResponseModel : RequestBaseModels
    {
        public List<ZoneModel> ListZone { get; set; }
        public List<TableModel> ListTable { get; set; }
        public List<PrinterModel> ListPrinter { get; set; }
        public List<SelectListItem> ListType { get; set; }

        public FilterResponseModel()
        {
            ListZone = new List<ZoneModel>();
            ListTable = new List<TableModel>();
            ListPrinter = new List<PrinterModel>();
            ListType = new List<SelectListItem>()
            {
                new SelectListItem { Text = Commons.TypeDineIn, Value = Commons.EItemFilter.DineIn.ToString("d")},
                new SelectListItem { Text = Commons.TypeTakeAway, Value = Commons.EItemFilter.TakeAway.ToString("d")}
            };
        }
    }
}
