using Sima.Common.Model;

namespace Receipt.ServiceModel
{
    public class PayReceiptResponse : BaseResponse, IBaseData<Types.Receipt>
    {
        public Types.Receipt Data { get; set; }
    }
}