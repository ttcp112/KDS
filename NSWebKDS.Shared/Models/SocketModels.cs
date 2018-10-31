﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSWebKDS.Shared.Models
{
    public class SocketModels
    {
        public PushDataModels PushData { get; set; }
    }
    public class PushDataModels
    {
        public string Action { get; set; }
        public string Data { get; set; }
        public string DeviceName { get; set; }
        public string StoreID { get; set; }
    }

  
}
