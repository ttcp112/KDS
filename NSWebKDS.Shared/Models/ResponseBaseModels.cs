﻿namespace NSWebKDS.Shared.Models
{
    public class ResponseBaseModels
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Error { get; set; }
        public object Data { get; set; }
    }
}
