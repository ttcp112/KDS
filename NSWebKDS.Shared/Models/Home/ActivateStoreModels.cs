using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSWebKDS.Shared.Models.Home
{
    public class ActivateStoreModels
    {
        public string AppKey { get; set; }
        public string AppSecret { get; set; }
        public string StoreCode { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Error { get; set; }
        public List<StoreModels> ListStore { get; set; }

        public ActivateStoreModels()
        {
            ListStore = new List<StoreModels>();
        }

    }
}
