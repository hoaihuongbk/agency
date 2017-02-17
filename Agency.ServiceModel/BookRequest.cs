using ServiceStack;
using Sima.Common.Model;

namespace Agency.ServiceModel
{
    [Route("/receipt/submit")]
    public class SubmitReceipt : BaseRequest, IReturn<SubmitReceiptResponse>
    {
        public int TicketId { get; set; }
        [ApiMember(Description = "dd-MM-yyyy")]
        public string DepartureDate { get; set; }
        public string Note { get; set; }
    }

    public class SubmitReceiptResponse : BaseResponse, IBaseData<Types.Receipt>
    {
        public Types.Receipt Data { get; set; }
    }

    [Route("/receipt/pay")]
    public class PayReceipt : BaseRequest, IReturn<PayReceiptResponse>
    {
        public string Code { get; set; }
        public string Note { get; set; }
    }

    public class PayReceiptResponse : BaseResponse, IBaseData<Types.Receipt>
    {
        public Types.Receipt Data { get; set; }
    }

    [Route("/receipt/cancel")]
    public class CancelReceipt : BaseRequest, IReturn<CancelReceiptResponse>
    {
        public string Code { get; set; }
        public string Note { get; set; }
    }

    public class CancelReceiptResponse : BaseResponse, IBaseData<Types.Receipt>
    {
        public Types.Receipt Data { get; set; }
    }
}
