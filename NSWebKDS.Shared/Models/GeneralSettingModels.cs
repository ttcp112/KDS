using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSWebKDS.Shared.Models
{
    public class GeneralSettingModels
    {
        public string ID { get; set; }
        public string StoreID { get; set; }
        public string SettingId { get; set; }
        public string DisplayName { get; set; }
        public string Value { get; set; }
        public string ValueType { get; set; }
        public int Status { get; set; }
        public int Code { get; set; }
    }

}
