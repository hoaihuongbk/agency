using Sima.Common.Model;

namespace Agency.ServiceModel
{
    public class PayReceiptResponse : BaseResponse, IBaseData<Types.Receipt>
    {
        public Types.Receipt Data { get; set; }
    }
}