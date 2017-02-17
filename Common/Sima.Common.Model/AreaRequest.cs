using Sima.Common.Model.Types;
using ServiceStack;
using System.Collections.Generic;

namespace Sima.Common.Model
{
    [Route("/common/states", "GET,POST")]
    public class GetStates : BaseRequest, IReturn<ConvertResponse<List<State>>>
    {

    }

    [Route("/common/cities", "GET,POST")]
    [Route("/common/cities/{StateId}", "GET,POST")]
    public class GetCities : BaseRequest, IReturn<ConvertResponse<List<City>>>
    {
        public int StateId { get; set; }
    }
}
