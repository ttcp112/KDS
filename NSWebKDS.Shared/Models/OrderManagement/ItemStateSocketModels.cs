using System.Collections.Generic;

namespace NSWebKDS.Shared.Models.OrderManagement
{
    /*
     # SOCKET
        Socket Name: Pos_ItemStateOrder
        Data: orderID + "|" + (order.ZoneID ?? "") + "|" + (order.TableID ?? "") + "|" + json
        //json: list ItemStateSocket
     */
    public class ItemStateSocketModels
    {
        public string ID { get; set; }
        public string OrderID { get; set; }
        public int State { get; set; }
        public bool IsTakeAway { get; set; }
        public List<string> PrinterIDs { get; set; }
    }
}
