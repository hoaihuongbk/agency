using ServiceStack;
using Sima.Common.Model;

namespace Agency.ServiceModel
{
    [Route("/setting", "GET,POST")]
    public class GetSetting : BaseRequest, IReturn<SettingResponse>
    {

    }
}