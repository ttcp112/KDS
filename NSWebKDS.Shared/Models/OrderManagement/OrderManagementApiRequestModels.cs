using System.Collections.Generic;

namespace NSWebKDS.Shared.Models.OrderManagement
{
    public class OrderManagementApiRequestModels : RequestBaseModels
    {
        #region//GetOrderManagementRequest 
        //Get - Update
        public bool IsWeb { get; set; }
        public bool IsRecall { get; set; } //get list recall
        public bool IsSummary { get; set; } //get order summary
        public List<int> ListItemState { get; set; }
        public int DishStatus { get; set; } //enum EItemState // DishStatus == 1: list pending. DishStatus != 1: list served
        public int ItemFilter { get; set; } //enum EItemFilter
        public bool IsGroup { get; set; }   //gruopby orderno default: true
        public List<string> ListFloorID { get; set; } //filter by list zone
        public List<string> ListTableID { get; set; } //filter by list table
        public List<string> ListPrinterID { get; set; } //filter by list printer
        #endregion

        #region//UpdateOrderManagements 
        //public bool IsRecall { get; set; }
        public List<UpdateOrderManagementModels> Items { get; set; }
        #endregion

        public OrderManagementApiRequestModels()
        {
            Items = new List<UpdateOrderManagementModels>();
        }
    }
}
