using ServiceStack;
using Sima.Common.Model;

namespace Agency.ServiceModel
{
    [Route("/tickets", "GET,POST")]
    public class GetTickets : PaginationRequest, IReturn<GetTicketsResponse>
    {
        [ApiMember(Description = "dd-MM-yyyy")]
        public string DepartureDate { get; set; }
    }
}
