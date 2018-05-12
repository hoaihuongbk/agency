using Sima.Common.Model.Types;
using ServiceStack;
using System.Collections.Generic;

namespace Sima.Common.Model
{
    [Route("/common/states", "GET,POST")]
    public class GetStates : BaseRequest, IReturn<ConvertResponse<List<State>>>
    {

    }
}
