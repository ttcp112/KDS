using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSWebKDS.Shared.Models.FilterModel
{

    public class PrinterModel
    {
        public string ID { get; set; }
        public string StoreID { get; set; }
        public string StoreName { get; set; }
        public string PrinterName { get; set; }
        public string IPAddress { get; set; }
        public string Port { get; set; }
        public int MaxChar { get; set; }
        public bool IsActive { get; set; }
        public string CreatedUser { get; set; }
        public int PrinterType { get; set; }
    }
}
