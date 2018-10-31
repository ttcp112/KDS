using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSWebKDS.Shared.Models.Home
{
    public class ActivateStoreRequest : RequestBaseModels
    {
        public string Url { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ApproveCode { get; set; }
        public byte DeviceType { get; set; }
        public string UUID { get; set; }
        public string DeviceName { get; set; }
        public string CreatedUser { get; set; }
    }
}
