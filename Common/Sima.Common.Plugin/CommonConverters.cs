using Sima.Common.Constant;
using Sima.Common.Model.Types;
using ServiceStack;
using System.Collections.Generic;
using System.Linq;

namespace Sima.Common.Plugin
{
    public class CommonConverters : IPlugin
    {
        public void Register(IAppHost appHost)
        {
            //Common Converter
            appHost.ResponseConverters.Add((req, response) =>
            {
                var type = response.GetType().Name;
                object result = response;
                var sesion = req.GetSession();
                switch (type)
                {
                    case "AuthenticateResponse":
                        var res = (AuthenticateResponse)response;
                        result = new ConvertResponse<AuthenticateResponse>
                        {
                            Status = !string.IsNullOrEmpty(res.SessionId) ? 1 : 0,
                            Data = res
                        };
                        break;
                    case "RegenerateApiKeysResponse":
                        var res1 = (RegenerateApiKeysResponse)response;
                        result = new ConvertResponse<List<UserApiKey>>
                        {
                            Status = res1.Results.Any() ? (int)CommonStatus.Success : (int)CommonStatus.UndefinedError,
                            Data = res1.Results
                        };
                        break;
                    case "GetApiKeysResponse":
                        var res2 = (GetApiKeysResponse)response;
                        result = new ConvertResponse<List<UserApiKey>>
                        {
                            Status = res2.Results.Any() ? (int)CommonStatus.Success : (int)CommonStatus.UndefinedError,
                            Data = res2.Results
                        };
                        break;
                    default:
                        break;
                }

                return result;
            });
        }
    }
}
