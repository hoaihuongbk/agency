﻿using Sima.Common.Constant;
using Sima.Common.Model.Types;
using ServiceStack;
using System.Linq;
using System.Threading.Tasks;

namespace Sima.Common.Plugin
{
    public class CommonConverters : IPlugin
    {
        public void Register(IAppHost appHost)
        {
            appHost.ResponseConverters.Add((req, response) =>
            {
                return Task.Run(() =>
                {
                    var type = response.GetType().Name;
                    var result = response;
                    switch (type)
                    {
                        case "AuthenticateResponse":
                            var res = (AuthenticateResponse)response;
                            result = new CustomAuthenticateResponse
                            {
                                Status = !string.IsNullOrEmpty(res.SessionId) ? (int)CommonStatus.Success : (int)CommonStatus.UndefinedError,
                                Data = res
                            };
                            break;
                        case "RegenerateApiKeysResponse":
                            var res1 = (RegenerateApiKeysResponse)response;
                            result = new CustomGetApiKeysResponse
                            {
                                Status = res1.Results.Any() ? (int)CommonStatus.Success : (int)CommonStatus.UndefinedError,
                                Data = res1.Results
                            };
                            break;
                        case "GetApiKeysResponse":
                            var res2 = (GetApiKeysResponse)response;
                            result = new CustomGetApiKeysResponse
                            {
                                Status = res2.Results.Any() ? (int)CommonStatus.Success : (int)CommonStatus.UndefinedError,
                                Data = res2.Results
                            };
                            break;
                        case "RegisterResponse":
                            var res3 = (RegisterResponse)response;
                            result = new CustomRegisterResponse
                            {
                                Status = !string.IsNullOrEmpty(res3.UserId) ? (int)CommonStatus.Success : (int)CommonStatus.UndefinedError,
                                Data = res3
                            };
                            break;
                        default:
                            break;
                    }

                    return result;     
                });
               
            });
        }
    }
}
