namespace NSWebKDS.Shared.Models
{
    public class RequestBaseModels
    {
        // AppKey, AppSecret, Mode, StoreId
        public string AppKey { get; set; }
        public string AppSecret { get; set; }
        public int Mode { get; set; }
        public string StoreId { get; set; }
        public RequestBaseModels()
        {
            Mode = 1;
        }
    }
}
