using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSWebKDS.Shared.Models.Home
{
    public class ActivateStoreApiResponseModels
    {
        public ActivateStoreModels data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Error { get; set; }
    }
}
