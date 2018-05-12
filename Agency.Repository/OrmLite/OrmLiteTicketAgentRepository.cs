using System;
using System.Collections.Generic;
using System.Linq;
using PT.Common.Repository.OrmLite;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Agency.Repository.OrmLite
{
    public class OrmLiteTicketAgentRepository : OrmLiteTicketAgentRepository<TicketAgent>
    {
        public OrmLiteTicketAgentRepository(IDbConnectionFactory dbFactory) : base(dbFactory) { }
    }

    public class OrmLiteTicketAgentRepository<TTicketAgent> : OrmLiteBaseRepository, ITicketAgentRepository
          where TTicketAgent : class, ITicketAgent
    {
        protected OrmLiteTicketAgentRepository(IDbConnectionFactory dbFactory, string namedConnnection = null)
       : base(dbFactory, namedConnnection)
        {

        }

        public IEnumerable<ITicketAgent> GetTickets(int agentId, DateTime departureDate, int numRowPerPage = 10, int page = 1)
        {
            return Exec(db =>
            {
                var query = db.Select<TTicketAgent>(c =>
                    c.AgentId == agentId &&
                    c.Status == (int)TicketStatusConstant.Available
                    //(c.FromDate == null || (c.FromDate.HasValue && c.FromDate.Value <= departureDate)) &&
                    //(c.ToDate == null || (c.ToDate.HasValue && c.ToDate.Value >= departureDate))
                ).OrderBy(c => c.Code)
                .ThenBy(c => c.Name);

                return query.Take(numRowPerPage).Skip((page - 1) * numRowPerPage).ToList();
            });
        }
    }
}
