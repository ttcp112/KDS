using Newtonsoft.Json;
using NSWebKDS.Shared.Factory;
using NSWebKDS.Shared.Models;
using NSWebKDS.Shared.Models.OrderManagement;
using NSWebKDS.Shared.Utilities;
using Quobject.SocketIoClientDotNet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NSWebKDS.Shared
{
    public class CommonFunctions
    {
        private static OrderManagementFactory _orderManagementFactory;
        public CommonFunctions()
        {
            _orderManagementFactory = new OrderManagementFactory();
        }
        public static void ListenServer()
        {
            try
            {
                Commons._socket = IO.Socket(Commons.SocketUrl);
                Commons._socket.On(Commons.EAction.ping.ToString(), (data) =>
                {
                    Commons._socket.Emit(Commons.EAction.pong.ToString(), "WebKDS - " + System.Environment.MachineName);
                });
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("ListenServer Error", ex);
            }
        }
        public static string GetSHA512(string text)
        {
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            byte[] message = UE.GetBytes(text);

            SHA512Managed hashString = new SHA512Managed();
            string hex = "";

            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }

        public static void GetListGeneralSetting(string storeId)
        {
            List<GeneralSettingModels> listdata = new List<GeneralSettingModels>();
            try
            {
                RequestBaseModels paraBody = new RequestBaseModels();
                paraBody.AppKey = Commons.AppKey;
                paraBody.AppSecret = Commons.AppSecret;

                paraBody.StoreId = storeId;
                paraBody.Mode = 1;

                NSLog.Logger.Info("GetListGeneralSetting Request: ", paraBody);
                var result = (ResponseBaseModels)ApiResponse.Post<ResponseBaseModels>(Commons.Store_Setting, null, paraBody);
                NSLog.Logger.Info("GetListGeneralSetting Result: ", result);

                dynamic data = result.Data;
                var lstZ = data["ListSettings"];
                var lstContent = JsonConvert.SerializeObject(lstZ);
                listdata = JsonConvert.DeserializeObject<List<GeneralSettingModels>>(lstContent);
                //check config
                var pendingTime = listdata.Where(ww => ww.Code == (int)Commons.ESettings.PendingTime).FirstOrDefault();
                //Commons._pendingTime = pendingTime != null ? int.Parse(pendingTime.Value) : 5;
                HttpContext.Current.Response.Cookies["WebKDSPendingTime"].Value = (pendingTime != null ? int.Parse(pendingTime.Value) : 5).ToString();
                HttpContext.Current.Response.Cookies["WebKDSPendingTime"].Expires = DateTime.MaxValue;

                var soundPrepareScreen = listdata.Where(ww => ww.Code == (int)Commons.ESettings.SoundPrepareScreen).FirstOrDefault();
                //Commons._soundPrepareScreenIndex = soundPrepareScreen != null ? int.Parse(soundPrepareScreen.Value) : 0;

                HttpContext.Current.Response.Cookies["WebKDSSoundPrepareScreenIndex"].Value = (soundPrepareScreen != null ? int.Parse(soundPrepareScreen.Value) : 0).ToString();
                HttpContext.Current.Response.Cookies["WebKDSSoundPrepareScreenIndex"].Expires = DateTime.MaxValue;
                //NumOfCellOrderMana
                var numOfCellOrderMana = listdata.Where(ww => ww.Code == (int)Commons.ESettings.NumOfCellOrderMana).FirstOrDefault();
                HttpContext.Current.Response.Cookies["WebKDSNumOfCellOrderMana"].Value = (numOfCellOrderMana != null ? int.Parse(numOfCellOrderMana.Value) : 0).ToString();
                HttpContext.Current.Response.Cookies["WebKDSNumOfCellOrderMana"].Expires = DateTime.MaxValue;


                NSLog.Logger.Info("GetListGeneralSetting: WebKDSPendingTime", (pendingTime != null ? int.Parse(pendingTime.Value) : 5).ToString());
                NSLog.Logger.Info("GetListGeneralSetting: WebKDSSoundPrepareScreenIndex", (soundPrepareScreen != null ? int.Parse(soundPrepareScreen.Value) : 0).ToString());
                NSLog.Logger.Info("GetListGeneralSetting: WebKDSNumOfCellOrderMana", (numOfCellOrderMana != null ? int.Parse(numOfCellOrderMana.Value) : 0).ToString());
            }
            catch (Exception e)
            {
                NSLog.Logger.Error("GetListGeneralSetting Error: ", e);

            }
        }

        public static void RefreshOrder()
        {
            try
            {
                Commons._socket.On(Commons.ServerAction, (data) =>
                {
                    var objReturn = JsonConvert.SerializeObject(data);
                    var objTmp = JsonConvert.DeserializeObject<SocketModels>(objReturn);
                    if (objTmp.PushData.Action == Commons.EAction.Pos_ItemStateOrder.ToString())
                    {

                        new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new
                            {
                                controller = "OrderManagement",
                                action = "Index"
                            })
                        );

                        //var aa = GetActivateCookie();

                        //refesh
                        //check storeId & filter
                        if (objTmp.PushData.StoreID == Commons._StoreId)
                        {
                            //OrderManagementClientModels request = new OrderManagementClientModels();
                            //request.StoreId = Commons._StoreId;

                            //bool isExist = false;
                            //if (!string.IsNullOrEmpty(objTmp.PushData.Data))
                            //{
                            //    var lstData = objTmp.PushData.Data.Split('|');
                            //    if (lstData.Count() == 4)
                            //    {
                            //        isExist = true;
                            //        //get cookie filter
                            //        var filterObj = GetFilterCookie();

                            //        //printer
                            //        var itemState = JsonConvert.DeserializeObject<ItemStateSocketModels>(lstData[3]);
                            //        if (filterObj.ListPrinter != null && filterObj.ListPrinter.Any())
                            //        {
                            //            isExist = filterObj.ListPrinter.Intersect(itemState.PrinterIDs).Any();
                            //            if (isExist == false)
                            //                return;
                            //            request.ListPrinterID = filterObj.ListPrinter;
                            //        }

                            //        if (filterObj.ListFloorID != null && filterObj.ListFloorID.Any())
                            //        {
                            //            isExist = filterObj.ListFloorID.Contains(lstData[1]);
                            //            if (isExist == false)
                            //                return;
                            //            request.ListFloorID = filterObj.ListFloorID;
                            //        }
                            //        //table
                            //        if (filterObj.ListTableID != null && filterObj.ListTableID.Any())
                            //        {
                            //            isExist = filterObj.ListTableID.Contains(lstData[2]);
                            //            if (isExist == false)
                            //                return;
                            //            request.ListTableID = filterObj.ListTableID;
                            //        }
                            //        ////printer
                            //        //var itemState = JsonConvert.DeserializeObject<ItemStateSocketModels>(lstData[3]);
                            //        //if (filterObj.ListPrinter != null && filterObj.ListPrinter.Any())
                            //        //{
                            //        //    isExist = filterObj.ListPrinter.Intersect(itemState.PrinterIDs).Any();
                            //        //    if (isExist == false)
                            //        //        return;
                            //        //    request.ListPrinterID = filterObj.ListPrinter;
                            //        //}
                            //        //type
                            //        if (filterObj.Type != null && filterObj.Type.Any())
                            //        {
                            //            if (filterObj.Type.Count == 2)
                            //            {
                            //                request.ItemFilter = 0;
                            //            }
                            //            else
                            //                request.ItemFilter = filterObj.Type.FirstOrDefault();
                            //        }
                            //    }
                            //}
                          //  if (isExist)
                          //  {
                          //      //refresh order management
                          //      //_orderManagementFactory.GetListData(request);
                          //      new RedirectToRouteResult(
                          //new RouteValueDictionary(
                          //    new
                          //    {
                          //        controller = "OrderManagement",
                          //        action = "Index"
                          //    })
                          //);
                          //  }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("RefreshOrder Error", ex);
            }
        }

        public static ActivateModels GetActivateCookie()
        {
            var activateValue = new ActivateModels();
            try
            {
                var activate = HttpContext.Current.Request.Cookies["cookiesActive"];
                if (activate != null)
                {
                    var activateDecode = HttpContext.Current.Server.UrlDecode(activate.Value);
                    activateValue = JsonConvert.DeserializeObject<ActivateModels>(activateDecode);
                }
            }
            catch (Exception ex)
            {

                NSLog.Logger.Error("GetActivateCookie error", ex);
            }
            return activateValue;
        }
        public static FilterModels GetFilterCookie()
        {
            var filterModels = new FilterModels();
            try
            {
                var tableIds = HttpContext.Current.Request.Cookies["ListTableID"];
                if (tableIds != null)
                {
                    var tableIdsDecode = HttpContext.Current.Server.UrlDecode(tableIds.Value);
                    var tableIdsValue = JsonConvert.DeserializeObject<List<string>>(tableIdsDecode);

                    filterModels.ListTableID = tableIdsValue;
                }
                var printers = HttpContext.Current.Request.Cookies["ListPrinter"];
                if (printers != null)
                {
                    var printersDecode = HttpContext.Current.Server.UrlDecode(printers.Value);
                    var printersValue = JsonConvert.DeserializeObject<List<string>>(printersDecode);

                    filterModels.ListPrinter = printersValue;
                }
                var floors = HttpContext.Current.Request.Cookies["ListFloorID"];
                if (floors != null)
                {
                    var floorsDecode = HttpContext.Current.Server.UrlDecode(floors.Value);
                    var floorsValue = JsonConvert.DeserializeObject<List<string>>(floorsDecode);

                    filterModels.ListFloorID = floorsValue;
                }

                var types = HttpContext.Current.Request.Cookies["Type"];
                if (types != null)
                {
                    var typesDecode = HttpContext.Current.Server.UrlDecode(types.Value);
                    var typesValue = JsonConvert.DeserializeObject<List<int>>(typesDecode);

                    filterModels.Type = typesValue;
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("GetFilterCookie error", ex);
            }
            return filterModels;
        }

        public static int GetPendingTimeSettingCookie()
        {
            int pendingTime = 0;
            try
            {
                var time = HttpContext.Current.Request.Cookies["WebKDSPendingTime"];
                if (time != null)
                {
                    int.TryParse(time.Value, out pendingTime);
                }
            }
            catch (Exception ex)
            {

                NSLog.Logger.Error("GetPendingTimeSettingCookie error", ex);
            }
            return pendingTime;
        }

        public static int GetSoundPrepareScreenIndexCookie()
        {
            int sound = 0;
            try
            {
                var soundIndex = HttpContext.Current.Request.Cookies["WebKDSSoundPrepareScreenIndex"];
                if (soundIndex != null)
                {
                    int.TryParse(soundIndex.Value, out sound);
                }
            }
            catch (Exception ex)
            {

                NSLog.Logger.Error("GetSoundPrepareScreenIndexCookie error", ex);
            }
            return sound;
        }

        public static int GetNumOfCellOrderManaCookie()
        {
            int num = 3;
            try
            {
                var obj = HttpContext.Current.Request.Cookies["WebKDSNumOfCellOrderMana"];
                if (obj != null)
                {
                    int.TryParse(obj.Value, out num);
                }
            }
            catch (Exception ex)
            {

                NSLog.Logger.Error("GetNumOfCellOrderManaCookie error", ex);
            }
            return num;
        }

    }
}
