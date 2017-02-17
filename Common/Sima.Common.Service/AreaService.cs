using Sima.Common.Constant;
using Sima.Common.Model;
using Sima.Common.Model.Types;
using Sima.Common.Service.Resources;
using ServiceStack;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using PT.Common.Repository;
using PT.Common.Repository.OrmLite;

namespace Sima.Common.Service
{
    public class AreaService : BaseService
    {
        public IAreaRepository AreaRepository { get; set; }

        public object Any(GetStates request)
        {
            try
            {
                List<State> states;
                using(var client = base.TryResolve<IRedisClientsManager>().GetClient())
                {
                    var hashId = "state";
                    states = client.GetAllEntriesFromHash(hashId).Select(c => new State() {
                        Id = Convert.ToInt32(c.Key),
                        Name = c.Value
                    }).ToList();
                    if (states.IsNullOrEmpty())
                    {
                        var areas = AreaRepository.GetAreaByType((int)AreaType.State);
                        if (areas.Any())
                        {
                            client.SetRangeInHash(hashId,
                                areas.Select(c => new KeyValuePair<string, string>(Convert.ToString(c.Id), c.Name))
                            );

                            states = areas.Select(c => new State() { Id = c.Id, Name = c.Name }).ToList();
                        }
                    }
                }

                if (states.IsNullOrEmpty())
                {
                    return new ConvertResponse<List<State>>()
                    {
                        Status = (int)CommonStatus.NotFound,
                        Message = CommonMessage.NoData
                    };
                }

                return new ConvertResponse<List<State>>()
                {
                    Status = (int)CommonStatus.Success,
                    Data = states.OrderBy(c => c.Name).ToList()
                };

            } catch(Exception ex)
            {
                Log.Error("GetStates: {0}".Fmt(ex.Message));
            }
            return new ConvertResponse<List<State>>()
            {
                Status = (int)CommonStatus.UndefinedError,
                Message = CommonMessage.SystemError
            };
        }
        public object Any(GetCities request)
        {
            try
            {
                List<City> cities;
                using (var client = base.TryResolve<IRedisClientsManager>().GetClient())
                {
                    var hashId = "cities:{0}".Fmt(request.StateId);
                    cities = client.GetAllEntriesFromHash(hashId).Select(c => new City()
                    {
                        Id = Convert.ToInt32(c.Key),
                        Name = c.Value
                    }).ToList();
                    if (cities.IsNullOrEmpty())
                    {
                        var areas = AreaRepository.GetAreaByBaseId(request.StateId);
                        if (areas.Any())
                        {
                            client.SetRangeInHash(hashId,
                                areas.Select(c => new KeyValuePair<string, string>(Convert.ToString(c.Id), c.Name))
                            );

                            cities = areas.Select(c => new City() { Id = c.Id, Name = c.Name }).ToList();
                        }
                    }
                }

                if (cities.IsNullOrEmpty())
                {
                    return new ConvertResponse<List<City>>()
                    {
                        Status = (int)CommonStatus.NotFound,
                        Message = CommonMessage.NoData
                    };
                }

                return new ConvertResponse<List<City>>()
                {
                    Status = (int)CommonStatus.Success,
                    Data = cities.OrderBy(c => c.Name).ToList()
                };
            }
            catch (Exception ex)
            {
                Log.Error("GetCities: StateId: {0} | ErrorMessage: {1}".Fmt(request.StateId, ex.Message));
            }
            return new ConvertResponse<List<City>>()
            {
                Status = (int)CommonStatus.UndefinedError,
                Message = CommonMessage.SystemError
            };
        }
    }
}
