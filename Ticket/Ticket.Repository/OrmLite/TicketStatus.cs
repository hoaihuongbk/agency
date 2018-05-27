using ServiceStack.DataAnnotations;

namespace Ticket.Repository.OrmLite
{
    [Alias("tbl_ticket_status")]
    public class TicketStatus : ITicketStatus
    {
        [Alias("status")] 
        public int Status { get; set; }
    }
}
