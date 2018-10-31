using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSWebKDS.Shared.Models.Home
{
    public class ActivateModels
    {
        public string URL { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ApproveCode { get; set; }
        public byte DeviceType { get; set; }
        public string AppKey { get; set; }
        public string AppSecret { get; set; }
        public int Mode { get; set; }
        public string StoreId { get; set; }

    }
}
