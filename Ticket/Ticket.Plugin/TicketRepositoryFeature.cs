using ServiceStack;
using ServiceStack.Data;
using ServiceStack.Redis;
using Ticket.Repository;
using Ticket.Repository.OrmLite;
using Ticket.Repository.Redis;

namespace Ticket.Plugin
{
    public class TicketRepositoryFeature : IPlugin
    {
        public void Register(IAppHost appHost)
        {
            var container = appHost.GetContainer();
            var dbFactory = container.Resolve<IDbConnectionFactory>();
            var redisManager = container.Resolve<IRedisClientsManager>();
            
            //Repositories
            container.RegisterAutoWiredType(typeof(ITicket), typeof(Repository.OrmLite.Ticket));
            container.RegisterAutoWiredType(typeof(ITicketAgent), typeof(TicketAgent));
            container.RegisterAutoWiredType(typeof(ITicketStatus), typeof(TicketStatus));

            var repo = new OrmLiteTicketRepository(dbFactory);
            container.Register<ITicketRepository>(c => repo);
            repo.InitSchema();

            var repo2 = new OrmLiteTicketAgentRepository(dbFactory);
            container.Register<ITicketAgentRepository>(c => repo2);

            var repo4 = new RedisTicketStatusRepository(redisManager);
            container.Register<ITicketStatusRepository>(c => repo4);
        }
    }
}