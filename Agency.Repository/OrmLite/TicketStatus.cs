using ServiceStack.DataAnnotations;

namespace Agency.Repository.OrmLite
{
    [Alias("tbl_ticket_status")]
    public class TicketStatus : ITicketStatus
    {
        [Alias("status")] 
        public int Status { get; set; }
    }
}
