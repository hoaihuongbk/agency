using System;
using ServiceStack;
using ServiceStack.Redis;
using Ticket.Repository.OrmLite;

namespace Ticket.Repository.Redis
{
    public class RedisTicketStatusRepository : ITicketStatusRepository
    {
        private readonly IRedisClientsManager _clientManager;

        public RedisTicketStatusRepository(IRedisClientsManager clientManager)
        {
            _clientManager = clientManager;
        }

        public ITicketStatus GetTicketStatus(int operatorId, string code, DateTime departureDate, string departureTime)
        {
            using(var client = _clientManager.GetClient())
            {
                var key = RedisTicketStatusExtensions.GetTicketStatusKey(operatorId, departureDate, departureTime);
                var value = client.GetValueFromHash(key, code.Trim());
                return new TicketStatus()
                {
                    Status = !string.IsNullOrEmpty(value)  ? Convert.ToInt32(value) : (int)TicketStatusConstant.Available 
                };
            }
        }

        public ITicketStatus UpdateTicketStatus(int operatorId, string code, DateTime departureDate, string departureTime, int status)
        {
            using (var client = _clientManager.GetClient())
            {
                var key = RedisTicketStatusExtensions.GetTicketStatusKey(operatorId, departureDate, departureTime);
                client.SetEntryInHash(key, code.Trim(), "{0}".Fmt(status));
                return new TicketStatus()
                {
                    Status = status
                };
            }
        }
    }
}
