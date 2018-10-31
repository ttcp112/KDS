using Newtonsoft.Json;
using NSWebKDS.Shared.Models;
using NSWebKDS.Shared.Models.FilterModel;
using NSWebKDS.Shared.Models.OrderManagement;
using NSWebKDS.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NSWebKDS.Shared.Factory
{
    public class OrderManagementFactory
    {
        public OrderManagementFactory()
        {
        }

        public OrderManagementApiResponseModels GetListData(OrderManagementClientModels model)
        {
            OrderManagementApiResponseModels data = new OrderManagementApiResponseModels();
            try
            {
                OrderManagementApiRequestModels paraBody = new OrderManagementApiRequestModels();

                paraBody.AppKey = model.AppKey;
                paraBody.AppSecret = model.AppSecret;
                paraBody.Mode = model.Mode;
                paraBody.StoreId = model.StoreId;

                ////
                paraBody.IsWeb = model.IsWeb;
                paraBody.IsRecall = model.IsRecall;             //get list recall
                paraBody.IsSummary = model.IsSummary;           //get order summary
                paraBody.ListItemState = model.ListItemState;
                paraBody.DishStatus = model.DishStatus;         //enum EItemState // DishStatus == 1: list pending. DishStatus != 1: list served
                paraBody.ItemFilter = model.ItemFilter;         //enum EItemFilter
                paraBody.IsGroup = model.IsGroup;               //gruopby orderno 
                paraBody.ListFloorID = model.ListFloorID;       //filter by list zone
                paraBody.ListTableID = model.ListTableID;       //filter by list table
                paraBody.ListPrinterID = model.ListPrinterID;   //filter by list printer

                NSLog.Logger.Info("GetListDataOM_Request: ", paraBody);
                var result = (ResponseBaseModels)ApiResponse.Post<ResponseBaseModels>(Commons.Order_OrderManagement_Get, null, paraBody);
                dynamic dataDynamic = result.Data;
                NSLog.Logger.Info("GetListDataOM_Response: ", result);

                data.IsSummary = dataDynamic["IsSummary"];

                //======
                var lstContent = JsonConvert.SerializeObject(dataDynamic["ListData"]);
                data.ListData = JsonConvert.DeserializeObject<List<OrderManagementModels>>(lstContent);
                if (data.ListData != null && data.ListData.Count > 0)
                {
                    data.ListData.ForEach(x =>
                    {
                        x.IdOrderNo = x.OrderNo;
                        //=============Pickup / Delivery
                        if (!string.IsNullOrEmpty(x.DeliveryNo))
                        {
                            if (x.DeliveryType != (int)Commons.EDeliveryType.Both)
                            {
                                x.OrderNo += " (" + x.DeliveryNo + ") " 
                                    + (x.DeliveryType == (int)Commons.EDeliveryType.Deliver ? Commons.DeliveryType_Deliver : Commons.DeliveryType_SelfCollect);
                            }
                        }
                        //====================
                        if (!string.IsNullOrEmpty(x.TableName))
                            x.OrderNo = "Table: " + x.TableName + " - " + x.OrderNo;
                        if (!string.IsNullOrEmpty(x.TagNumber))
                            x.OrderNo = x.OrderNo + " - " + x.TagNumber;
                        if (x.ListItem != null && x.ListItem.Count > 0)
                        {
                            var _datetime = x.ListItem.OrderByDescending(z => z.Time).First().Time;
                            x.ListItem.ForEach(z =>
                            {
                                //z.DataTime = z.Time;

                                z.DataTime = _datetime; //Dung change day 4/9/2018
                                //==============
                                z.Color = !string.IsNullOrEmpty(z.Color) ? (z.Color.Replace("#", "")) : z.Color;
                                z.Color = "#" + (string.IsNullOrEmpty(z.Color) ? "FFFFFF".ToString() : z.Color);
                                

                                z.GName = z.Name;
                                if (z.ListItem == null)
                                    z.ListItem = new List<string>();
                                //===================
                                if (z.ListItem != null && z.ListItem.Count > 0)
                                {
                                    z.Name = "<strong>" + z.Name + "</strong>: " + (string.Join(", ", z.ListItem));
                                }
                                //==========
                                if (!string.IsNullOrEmpty(z.Set))
                                {
                                    z.Name = "<strong>" + z.Set + "</strong>: " + z.Name;
                                }
                            });
                            x.OrderTime = x.ListItem.OrderBy(z => z.Time).First().Time;
                            
                        }
                    });
                }

                //Not use Variable ListSummary
                //lstContent = JsonConvert.SerializeObject(dataDynamic["ListSummary"]);
                //data.ListSummary = JsonConvert.DeserializeObject<List<SummaryItemModels>>(lstContent);
                for (int i = 0; i < data.ListData.Count(); i++) {
                    string _IdOrderNo = i.ToString() + data.ListData[i].IdOrderNo;
                    data.ListData[i].IdOrderNo = _IdOrderNo;
                }
                NSLog.Logger.Info("GetListDataOM_Return: ", data);
                return data;
            }
            catch (Exception e)
            {
                NSLog.Logger.Error("GetListDataOM_Fail: ", e);
                return data;
            }
        }
    }
}
