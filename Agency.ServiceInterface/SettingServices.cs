using ServiceStack;
using Agency.ServiceModel;
using Sima.Common.Constant;

namespace Agency.ServiceInterface
{
    public class SettingServices : Service
    {
        public object Any(GetSetting request)
        {
            return new SettingResponse
            {
                Status = (int)CommonStatus.Success
            };
        }
    }
}