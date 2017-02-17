using ServiceStack;
using ServiceStack.Data;
using Sima.App.Repository;
using Sima.App.Repository.OrmLite;

namespace Sima.App.Plugin
{
    public class AppFeature : IPlugin
    {
        public void Register(IAppHost appHost)
        {
            var container = appHost.GetContainer();

            var dbFactory = container.Resolve<IDbConnectionFactory>();
            //App registration
            container.RegisterAutoWiredType(typeof(IApp), typeof(Sima.App.Repository.OrmLite.App));
            var appRepo = new OrmLiteAppRepository(dbFactory);
            container.Register<IAppRepository>(c => appRepo);
            appRepo.InitSchema();
        }
	}
}
