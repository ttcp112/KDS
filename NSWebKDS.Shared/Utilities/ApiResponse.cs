using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSWebKDS.Shared.Utilities
{
    public class ApiResponse
    {
        public static object Post<T>(string url, Dictionary<string, string> paramInfos, object paramBodys)
        {
            var client = new RestClient(Commons.HostApiUrl + "/" + url);
            var req = new RestRequest("");
            req.Method = Method.POST;
            if (paramInfos != null)
            {
                foreach (var param in paramInfos)
                {
                    req.AddParameter(param.Key, param.Value);
                }
            }
            if (paramBodys != null)
            {
                req.AddJsonBody(paramBodys);
            }
            var response = client.Execute(req);
            if (response.ResponseStatus == ResponseStatus.Completed)
            {
                T responseObj = JsonConvert.DeserializeObject<T>(response.Content);
                return responseObj;
            }
            return null;
        }

        public static object PostForActivate<T>(string url, Dictionary<string, string> paramInfos, object paramBodys)
        {
            var client = new RestClient(url);
            var req = new RestRequest("");
            req.Method = Method.POST;
            if (paramInfos != null)
            {
                foreach (var param in paramInfos)
                {
                    req.AddParameter(param.Key, param.Value);
                }
            }
            if (paramBodys != null)
            {
                req.AddJsonBody(paramBodys);
            }
            var response = client.Execute(req);
            if (response.ResponseStatus == ResponseStatus.Completed)
            {
                T responseObj = JsonConvert.DeserializeObject<T>(response.Content);
                return responseObj;
            }
            return null;
        }

        public static object Get<T>(string url, Dictionary<string, string> paramInfos, object paramBodys)
        {
            var client = new RestClient(Commons.HostApiUrl + "/" + url);
            var req = new RestRequest("");
            req.Method = Method.GET;
            if (paramInfos != null)
            {
                foreach (var param in paramInfos)
                {
                    req.AddParameter(param.Key, param.Value);
                }
            }
            if (paramBodys != null)
            {
                req.AddJsonBody(paramBodys);
            }
            var response = client.Execute(req);
            if (response.ResponseStatus == ResponseStatus.Completed)
            {
                T responseObj = JsonConvert.DeserializeObject<T>(response.Content);
                return responseObj;
            }
            return null;
        }
    }
}
