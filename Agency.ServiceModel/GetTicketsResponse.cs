using System.Collections.Generic;
using Agency.ServiceModel.Types;
using Sima.Common.Model;

namespace Agency.ServiceModel
{
    public class GetTicketsResponse : PaginationResponse, IBaseData<List<TicketAgent>> 
    {
        public List<Types.TicketAgent> Data { get; set; }
    }
}