using Agency.ServiceModel.Types;
using Sima.Common.Model;

namespace Agency.ServiceModel
{
    public class SettingResponse : BaseResponse, IBaseData<Settings>
    {
        public Settings Data { get; set; }
    }
}