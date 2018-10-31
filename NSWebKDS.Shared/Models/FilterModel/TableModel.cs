using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSWebKDS.Shared.Models.FilterModel
{
    public class TableModel
    {
        public string Index { get; set; } /* Index of table in excel when Importing table */
        public string ID { get; set; }
        public string Name { get; set; }
        public string StoreID { get; set; }
        public string StoreName { get; set; }
        public string StoreIDInPoins { get; set; }
        public string ZoneID { get; set; }
        public string ZoneName { get; set; }
        public int Cover { get; set; }
        public int NumberOfPerson { get; set; }
        public byte ViewMode { get; set; }
        public double XPoint { get; set; }
        public double YPoint { get; set; }
        public double ZPoint { get; set; }
        public bool IsActive { get; set; }
        public bool IsShowInReservation { get; set; }
        public bool IsTemp { get; set; }
        public string OrderID { get; set; }
        public int State { get; set; }
        public bool IsCall { get; set; }
        public bool IsGuestCheck { get; set; }
        public bool IsWalletOrder { get; set; }
    }
}
