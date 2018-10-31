using Newtonsoft.Json;
using NSWebKDS.Shared.Models;
using NSWebKDS.Shared.Models.FilterModel;
using NSWebKDS.Shared.Models.OrderManagement;
using NSWebKDS.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSWebKDS.Shared.Factory
{
    public class FilterFactory
    {
        public FilterFactory()
        {
        }
        public List<TableModel> GetListTable(string StoreId, string AppKey, string AppSecret) {
            List<TableModel> model = new List<TableModel>();
            try
            {
                TableRequest paraBody = new TableRequest();
                paraBody.IsOnlyShowItemActive = true;
                paraBody.StoreId = StoreId;
                paraBody.AppKey = AppKey;
                paraBody.AppSecret = AppSecret;
                NSLog.Logger.Info("GetListTable_Request: ", paraBody);
                var result = (ResponseBaseModels)ApiResponse.Post<ResponseBaseModels>(Commons.Table_Get, null, paraBody);
                dynamic dataDynamic = result.Data;
                NSLog.Logger.Info("GetListTable_Response: ", result);
                var lstContent = JsonConvert.SerializeObject(dataDynamic["ListTable"]);
                model = JsonConvert.DeserializeObject<List<TableModel>>(lstContent);
            }
            catch (Exception ex) {
                NSLog.Logger.Info("GetListTable_False: ", ex);
            }
            return model;
        }
        public List<PrinterModel> GetListPrinter(string StoreId, string AppKey, string AppSecret)
        {
            List<PrinterModel> model = new List<PrinterModel>();
            try
            {
                PrinterRequest paraBody = new PrinterRequest();
                paraBody.IsOnlyShowActiveItem = true;
                paraBody.StoreId = StoreId;
                paraBody.AppKey = AppKey;
                paraBody.AppSecret = AppSecret;
                NSLog.Logger.Info("GetListPrinter_Request: ", paraBody);
                var result = (ResponseBaseModels)ApiResponse.Post<ResponseBaseModels>(Commons.Printer_Get, null, paraBody);
                dynamic dataDynamic = result.Data;
                NSLog.Logger.Info("GetListPrinter_Response: ", result);
                var lstContent = JsonConvert.SerializeObject(dataDynamic["ListPrinter"]);
                model = JsonConvert.DeserializeObject<List<PrinterModel>>(lstContent);
            }
            catch (Exception ex)
            {
                NSLog.Logger.Info("GetListPrinter_False: ", ex);
            }
            return model;
        }
        public List<ZoneModel> GetListZone(string StoreId, string AppKey, string AppSecret)
        {
            List<ZoneModel> model = new List<ZoneModel>();
            try
            {
                ZoneRequest paraBody = new ZoneRequest();
                paraBody.StoreId = StoreId;
                paraBody.AppKey = AppKey;
                paraBody.AppSecret = AppSecret;
                NSLog.Logger.Info("GetListZone_Request: ", paraBody);
                var result = (ResponseBaseModels)ApiResponse.Post<ResponseBaseModels>(Commons.Zone_Get, null, paraBody);
                dynamic dataDynamic = result.Data;
                NSLog.Logger.Info("GetListZone_Response: ", result);
                var lstContent = JsonConvert.SerializeObject(dataDynamic["ListZone"]);
                model = JsonConvert.DeserializeObject<List<ZoneModel>>(lstContent);
            }
            catch (Exception ex)
            {
                NSLog.Logger.Info("GetListZone_False: ", ex);
            }
            return model;
        }
    }
}
