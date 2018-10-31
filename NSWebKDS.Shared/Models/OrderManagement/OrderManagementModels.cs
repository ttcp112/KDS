using System;
using System.Collections.Generic;

namespace NSWebKDS.Shared.Models.OrderManagement
{
    public class OrderManagementModels
    {
        //for Attribute html
        public string IdOrderNo { get; set; }
        //========
        public string OrderNo { get; set; }
        public string DeliveryNo { get; set; }
        public int DeliveryType { get; set; } //enum EDeliveryType
        public DateTime DeliveryTime { get; set; }
        public string TagNumber { get; set; }
        public string TableName { get; set; }
        public string ZoneName { get; set; }
        public DateTime OrderTime { get; set; }
        public List<DishItemModels> ListItem { get; set; }

        public OrderManagementModels()
        {
            ListItem = new List<DishItemModels>(); ;
        }
    }
}
