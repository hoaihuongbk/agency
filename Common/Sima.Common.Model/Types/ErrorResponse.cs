using Sima.Common.Constant;

namespace Sima.Common.Model.Types
{
    public class SimaErrorResponse : BaseResponse
    {
        public SimaErrorResponse() : base()
        {

        }
        public SimaErrorResponse(CommonStatus st) : base()
        {
            Status = (int)st;
        }
    }
}
