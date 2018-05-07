using Agency.RepositoryInterface;

namespace Agency.Repository
{
    public class TicketStatus : ITicketStatus
    {
        public int Status { get; set; }
    }
}
