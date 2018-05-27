using System.Collections.Generic;
using Sima.Common.Model;
using Ticket.ServiceModel.Types;

namespace Ticket.ServiceModel
{
    public class GetTicketsResponse : PaginationResponse, IBaseData<List<TicketAgent>> 
    {
        public List<Types.TicketAgent> Data { get; set; }
    }
}