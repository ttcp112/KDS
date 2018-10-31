using System;
using System.Collections.Generic;

namespace NSWebKDS.Shared.Models.OrderManagement
{
    public class SummaryItemModels
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string ColorCode { get; set; }
        public int NumberOfPending { get; set; }
        public int NumberOfReady { get; set; }
        public DateTime Time { get; set; }
        public List<OrderManagementModels> ListData { get; set; }
    }
}
