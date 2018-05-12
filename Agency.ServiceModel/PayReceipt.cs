using ServiceStack;
using Sima.Common.Model;

namespace Agency.ServiceModel
{
    [Route("/receipt/pay")]
    public class PayReceipt : BaseRequest, IReturn<PayReceiptResponse>
    {
        public string Code { get; set; }
        public string Note { get; set; }
    }
}