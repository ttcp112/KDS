using Quobject.SocketIoClientDotNet.Client;
using RestSharp;
using System;
using System.Configuration;

namespace NSWebKDS.Shared
{
    public static class Commons
    {
        #region Api_Links
        public const string Store_Active = "api/v1/Store/Activate";
        //GET ORDER MANA | //UPDATE ORDER MANA
        public const string Order_OrderManagement_Get = "api/v1/Order/OrderManagement/Get";
        public const string Order_OrderManagement_Update = "api/v1/Order/OrderManagement/Update";
        public const string Store_Setting = "api/v1/Setting/GetGeneralSettings";

        public const string Printer_Get = "api/v1/Printer/Get";
        public const string Zone_Get = "api/v1/Zone/Get";
        public const string Table_Get = "api/v1/Table/Get";

        #endregion End Api_Links
        public const string DeliveryType_Deliver = "Delivery";
        public const string DeliveryType_SelfCollect = "Self Collect";

        public static Socket _socket { get; set; }
        public static string SocketUrl
        {
            get
            {
                return string.IsNullOrEmpty(ConfigurationManager.AppSettings["SocketURL"]) ? "" : ConfigurationManager.AppSettings["SocketURL"];
            }
        }
        public static string HostApiUrl
        {
            get
            {
                var activate = CommonFunctions.GetActivateCookie();
                return activate.URL;
            }
        }

        public static string _StoreId
        {
            get
            {
                var activate = CommonFunctions.GetActivateCookie();
                return activate.StoreId;
            }
        }
        public static string ServerAction = "server_action";
        public static int _pendingTime
        {
            get
            {
                return CommonFunctions.GetPendingTimeSettingCookie();
            }
        }
        public static int _soundPrepareScreenIndex
        {
            get
            {
                return CommonFunctions.GetSoundPrepareScreenIndexCookie();
            }
        }
        public static int _numOfCellOrderMana
        {
            get
            {
                return CommonFunctions.GetNumOfCellOrderManaCookie();
            }
        }
        public static string _soundPrepareScreen
        {
            get
            {
                var soundIndex = CommonFunctions.GetSoundPrepareScreenIndexCookie();
                if (soundIndex == 0)
                    return "/Sounds/prepare-sound.mp3";
                else if (soundIndex == 1)
                    return "/Sounds/served-sound.m4a";
                else if (soundIndex == 2)
                    return "/Sounds/callForBill.m4a";
                else // 3
                    return "/Sounds/callForService.mp3";
            }
        }
        public static string UUID = "NSWebKDS";
        public static string DeviceName = "NSWebKDS";
        public static string CreatedUser = "admin";

        public static string TypeDineIn = "DineIn";
        public static string TypeTakeAway = "TakeAway";

        public static DateTime MaxDate = new DateTime(9999, 12, 31, 23, 59, 59, DateTimeKind.Utc);
        public static string AppKey
        {
            get
            {
                var activate = CommonFunctions.GetActivateCookie();
                return activate.AppKey;
            }
        }
        public static string AppSecret
        {
            get
            {
                var activate = CommonFunctions.GetActivateCookie();
                return activate.AppSecret;
            }
        }

        public enum EAction
        {
            ping,
            pong,
            Pos_ItemStateOrder
        }

        public enum EDeliveryType
        {
            Both = 0,
            Deliver = 1, /* delivery order */
            SelfCollect = 2 /* pick up order */
        }

        public enum EItemState
        {
            PendingStatus = 1,
            ReadyStatus = 2,
            ServedStatus = 3
        }

        public enum EItemFilter
        {
            All = 0,
            DineIn = 1,
            TakeAway = 2,
        }

        public enum ESettings
        {
            PendingTime = 117,
            SoundPrepareScreen = 54,
            NumOfCellOrderMana = 116
        }
    }
}
