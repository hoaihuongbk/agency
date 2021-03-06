﻿using System;
using System.Collections.Generic;

namespace Ticket.Repository
{
    public interface ITicketAgentRepository
    {
        IEnumerable<ITicketAgent> GetTickets(int agentId, DateTime departureDate, int numRowPerPage = 10, int page = 1);
    }
}
