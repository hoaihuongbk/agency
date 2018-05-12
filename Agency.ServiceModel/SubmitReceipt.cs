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
}
