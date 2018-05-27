using System;

namespace Ticket.Repository
{
    public interface ITicketStatusRepository
    {
        ITicketStatus GetTicketStatus(int operatorId, string code, DateTime departureDate, string departureTime);
        ITicketStatus UpdateTicketStatus(int operatorId, string code, DateTime departureDate, string departureTime, int status);
    }
}
