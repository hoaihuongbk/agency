using Receipt.Repository;
using Receipt.Repository.OrmLite;
using ServiceStack;
using ServiceStack.Data;

namespace Receipt.Plugin
{
    public class ReceiptRepositoryFeature : IPlugin
    {
        public void Register(IAppHost appHost)
        {
            var container = appHost.GetContainer();
            var dbFactory = container.Resolve<IDbConnectionFactory>();
            
            container.RegisterAutoWiredType(typeof(IReceipt), typeof(Repository.OrmLite.Receipt));
            
            var repo5 = new OrmLiteReceiptRepository(dbFactory);
            container.Register<IReceiptRepository>(c => repo5);
            repo5.InitSchema();
        }
    }
}