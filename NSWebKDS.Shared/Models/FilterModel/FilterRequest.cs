using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSWebKDS.Shared.Models.FilterModel
{
    public class FilterRequest
    {
        
    }
    public class TableRequest : RequestBaseModels
    {
        public bool IsOnlyShowItemActive { get; set; } //true
    }
    public class PrinterRequest : RequestBaseModels
    {
        public bool IsOnlyShowActiveItem { get; set; } //true
    }
    public class ZoneRequest : RequestBaseModels
    {
       
    }
}
