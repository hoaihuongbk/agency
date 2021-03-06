﻿namespace Ticket.Repository
{
    public interface ITicketRepository
    {
        ITicket CreateTicket(ITicket newTicket);
        void DeleteTicket(int id);
        ITicket GetTicket(int id);
        ITicket UpdateTicket(ITicket existingTicket, ITicket newTicket);
    }
}
