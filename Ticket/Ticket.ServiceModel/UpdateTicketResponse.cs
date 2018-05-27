using Sima.Common.Model;

namespace Ticket.ServiceModel
{
    public class UpdateTicketResponse : BaseResponse, IBaseData<Types.Ticket>
    {
        public Types.Ticket Data { get; set; }
    }
}