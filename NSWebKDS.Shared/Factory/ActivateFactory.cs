using NSWebKDS.Shared.Models.Home;
using NSWebKDS.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSWebKDS.Shared.Factory
{
    public class ActivateFactory
    {

        public ActivateStoreModels Activate(ActivateModels model)
        {
            ActivateStoreModels data = new ActivateStoreModels();
            try
            {
                ActivateStoreRequest paraBody = new ActivateStoreRequest();

                if(string.IsNullOrEmpty(model.AppSecret) && string.IsNullOrEmpty(model.AppKey))
                {
                    paraBody.Email = model.Email;
                    paraBody.Password = CommonFunctions.GetSHA512(model.Password);
                }
                paraBody.DeviceType = model.DeviceType;
                paraBody.AppKey = model.AppKey;
                paraBody.AppSecret = model.AppSecret;
                paraBody.ApproveCode = model.ApproveCode;
                paraBody.StoreId = model.StoreId;
                paraBody.UUID = Commons.UUID;
                paraBody.DeviceName = Commons.DeviceName;
                paraBody.Mode = 1;
                paraBody.CreatedUser = Commons.CreatedUser;
                paraBody.Url = model.URL;

                NSLog.Logger.Info("Activate_Request: ", paraBody);
                var result = (ActivateStoreApiResponseModels)ApiResponse.PostForActivate<ActivateStoreApiResponseModels>(paraBody.Url+ "/" +Commons.Store_Active, null, paraBody);
                NSLog.Logger.Info("Activate_Response: ", result);
                if (result != null && string.IsNullOrEmpty(model.AppKey) && string.IsNullOrEmpty(model.AppSecret))
                {
                    data = result.data;
                    data.Success = result.Success;
                    data.Message = result.Message;
                }
                else if(result != null)
                {
                    data.Success = result.Success;
                    data.Message = result.Message;
                    data.Error = result.Error;
                }
                NSLog.Logger.Info("Activate_Return: ", data);
                return data;
            }
            catch (Exception e)
            {
                data.Message = e.Message;
                NSLog.Logger.Error("Activate_Fail: ", e);
                return data;
            }
        }
    }
}
