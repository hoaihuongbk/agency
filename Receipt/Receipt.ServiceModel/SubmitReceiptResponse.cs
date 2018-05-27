using Sima.Common.Model;

namespace Receipt.ServiceModel
{
    public class SubmitReceiptResponse : BaseResponse, IBaseData<Types.Receipt>
    {
        public Types.Receipt Data { get; set; }
    }
}