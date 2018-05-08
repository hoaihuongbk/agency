using Agency.RepositoryInterface;
using ServiceStack.DataAnnotations;

namespace Agency.Repository
{
    [Alias("tbl_ticket_status")]
    public class TicketStatus : ITicketStatus
    {
        [Alias("status")] 
        public int Status { get; set; }
    }
}
