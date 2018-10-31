using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSWebKDS.Shared.Models
{
    public class FilterModels
    {
        public List<string> ListTableID { get; set; }
        public List<string> ListPrinter { get; set; }
        public List<string> ListFloorID { get; set; }
        public List<int> Type { get; set; }
        public FilterModels()
        {
            ListTableID = new List<string>();
            ListPrinter = new List<string>();
            ListFloorID = new List<string>();
            Type = new List<int>();
        }

    }
}
