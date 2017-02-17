using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.RepositoryInterface
{
    public interface ITicketStatusRepository
    {
        ITicketStatus GetTicketStatus(int operatorId, string code, DateTime departureDate, string departureTime);
        ITicketStatus UpdateTicketStatus(int operatorId, string code, DateTime departureDate, string departureTime, int status);
    }
}
