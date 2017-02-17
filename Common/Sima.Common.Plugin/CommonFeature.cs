using ServiceStack;
using ServiceStack.Data;
using Sima.Common.Service;
using PT.Common.Repository.OrmLite;
using PT.Common.Repository;

namespace Sima.Common.Plugin
{
    public class CommonFeature : IPlugin
    {
        public void Register(IAppHost appHost)
        {
            var container = appHost.GetContainer();
            var dbFactory = container.Resolve<IDbConnectionFactory>();
            container.RegisterAutoWiredType(typeof(IArea), typeof(Area));
            var repo = new OrmLiteAreaRepository(dbFactory);
            container.Register<IAreaRepository>(c => repo);
            repo.InitSchema();

            appHost.RegisterService<AreaService>();
        }
    }
}
