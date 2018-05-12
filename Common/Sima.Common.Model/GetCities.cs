using System.Collections.Generic;
using ServiceStack;
using Sima.Common.Model.Types;

namespace Sima.Common.Model
{
    [Route("/common/cities", "GET,POST")]
    [Route("/common/cities/{StateId}", "GET,POST")]
    public class GetCities : BaseRequest, IReturn<ConvertResponse<List<City>>>
    {
        public int StateId { get; set; }
    }
}