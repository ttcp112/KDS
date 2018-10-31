using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSWebKDS.Shared.Models.FilterModel
{
    public class ZoneModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string StoreID { get; set; }
        public string StoreName { get; set; }
        public string Description { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
