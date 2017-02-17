using Agency.ServiceModel.Types;
using ServiceStack;
using Sima.Common.Model;

namespace Agency.ServiceModel
{
    [Route("/setting", "GET,POST")]
    public class GetSetting : BaseRequest, IReturn<SettingResponse>
    {

    }

    public class SettingResponse : BaseResponse, IBaseData<Settings>
    {
        public Settings Data { get; set; }
    }
}