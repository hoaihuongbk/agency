using Sima.Common.Model;

namespace Agency.ServiceModel
{
    public class AddTicketResponse : BaseResponse, IBaseData<Types.Ticket>
    {
        public Types.Ticket Data { get; set; }
    }
}