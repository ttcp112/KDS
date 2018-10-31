using System;
using System.Collections.Generic;

namespace NSWebKDS.Shared.Models.OrderManagement
{
    public class DishItemModels
    {
        //for attribute html
        public DateTime DataTime { get; set; }
        public string ID { get; set; }
        public string Set { get; set; } //Set Name
        public string Name { get; set; } //Dish Name
        public List<string> ListItem { get; set; } //List Modifier Name
        public DateTime Time { get; set; }
        public double Qty { get; set; }
        public int DefaultState { get; set; } //Dish Default State
        public int State { get; set; } //Dish Current State
        public string Color { get; set; }
        public string PrinterName { get; set; }
        public bool IsTA { get; set; } //IsTakeAway
        public bool IsDel { get; set; } //IsDelete
        public int Sequence { get; set; }

        public string GName { get; set; } //Dish Name Gird
        public DishItemModels()
        {
            ListItem = new List<string>();
        }
    }
}
