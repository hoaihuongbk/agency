using Funq;
using ServiceStack;
using Agency.ServiceInterface;
using ServiceStack.Configuration;
using System;
using ServiceStack.Text;
using ServiceStack.Redis;
using ServiceStack.Validation;
using ServiceStack.OrmLite;
using ServiceStack.Data;
using ServiceStack.Logging;
using Sima.Common.Logging;
using ServiceStack.Auth;
using System.Collections.Generic;
using Sima.Common.Plugin;
using Agency.RepositoryInterface;
using Agency.Repository;

namespace Agency
{
    //VS.NET Template Info: https://servicestack.net/vs-templates/EmptyAspNet
    public class AppHost : AppHostBase
    {
        /// <summary>
        /// Base constructor requires a Name and Assembly where web service implementation is located
        /// </summary>
        public AppHost()
            : base("Agency", typeof(SettingServices).Assembly) { }

        public override void OnBeforeInit()
        {
            base.OnBeforeInit();
            //typeof(RegisterService).AddAttributes(new RestrictAttribute() { VisibilityTo = RequestAttributes.None });
            //typeof(GetApiKeysService).AddAttributes(new RestrictAttribute() { VisibilityTo = RequestAttributes.None });
            //typeof(RegenerateApiKeysService).AddAttributes(new RestrictAttribute() { VisibilityTo = RequestAttributes.None });
        }

        /// <summary>
        /// Application specific configuration
        /// This method should initialize any IoC resources utilized by your web service classes.
        /// </summary>
        public override void Configure(Container container)
        {
            var appSettings = base.AppSettings;

            JsConfig.EmitCamelCaseNames = true;
            JsConfig<DateTime>.SerializeFn = dateObj => string.Format("{0:yyyy-MM-ddTHH:mm:ss.000}", dateObj);
            JsConfig<TimeSpan>.SerializeFn = timeSpan => string.Format("{0:00}:{1:00}", timeSpan.Hours, timeSpan.Minutes);
            JsConfig<DBNull>.SerializeFn = dbNull => string.Empty;
        
            //Configuring
            ConfigureRedis(container, appSettings);
            ConfigureDb(container, appSettings);
            ConfigureLogging(container, appSettings);
            ConfigureAuth(container, appSettings);
            ConfigureFilter(container, appSettings);
            ConfigureValidation(container, appSettings);
            ConfigureExceptionFormat(container, appSettings);
            ConfigureConverter(container, appSettings);
            //ConfigureGateway(container, appSettings);

            SetConfig(new HostConfig
            {
                HandlerFactoryPath = "api",
                //EnableFeatures = Feature.All.Remove(disableFeatures), //all formats except of JSV and SOAP
                DebugMode = true, //Show StackTraces in service responses during development
                WriteErrorsToResponse = false, //Disable exception handling
                DefaultContentType = MimeTypes.Json, //Change default content type
                AllowJsonpRequests = true, //Enable JSONP requests
                RestrictAllCookiesToDomain = appSettings.GetString("rootDomain")
            });

            //Plugins
            Plugins.Add(new PostmanFeature());
            Plugins.Add(new CorsFeature());
            Plugins.Add(new ValidationFeature());

            Plugins.Add(new CommonFeature());
        }

        //public override RouteAttribute[] GetRouteAttributes(Type requestType)
        //{
        //    var routes = base.GetRouteAttributes(requestType);
        //    routes.Each(x => x.Path = "/v1" + x.Path);
        //    return routes;
        //}

        //public override IDbConnection GetDbConnection(IRequest req = null)
        //{
        //    //If an API Test Key was used return DB connection to TestDb instead: 
        //    return req.GetApiKey()?.Environment == "test"
        //        ? TryResolve<IDbConnectionFactory>().OpenDbConnection("Agency_Test")
        //        : base.GetDbConnection(req);
        //}

        private void ConfigureRedis(Container container, IAppSettings appSettings)
        {
            var connections = appSettings.Get<Dictionary<string, string>>("connectionStrings");
            
#if DEBUG
            var sentinelHosts = new[] { connections.GetValueOrDefault("Sentinel0"), connections.GetValueOrDefault("Sentinel1"), connections.GetValueOrDefault("Sentinel2") };
            var sentinel = new RedisSentinel(sentinelHosts, masterName: "mymaster");
            sentinel.RedisManagerFactory = (master, slaves) => new RedisManagerPool(master, new RedisPoolConfig()
            {
                MaxPoolSize = 20
            });
            sentinel.HostFilter = host => "{0}?db=0".Fmt(host);
            container.Register<IRedisClientsManager>(c => sentinel.Start());
#else
            var redisManager = new RedisManagerPool(connections.GetValueOrDefault("Sentinel0"), new RedisPoolConfig() {
                MaxPoolSize = 20,
            });
            container.Register<IRedisClientsManager>(c => redisManager);
#endif

        }
        
        private void ConfigureDb(Container container, IAppSettings appSettings)
        {
            var connections = appSettings.Get<Dictionary<string, string>>("connectionStrings");
            
            var dbFactory = new OrmLiteConnectionFactory(connections.GetValueOrDefault("Agency"), SqlServerDialect.Provider);
            container.Register<IDbConnectionFactory>(dbFactory);

            //Repositories
            container.RegisterAutoWiredType(typeof(ITicket), typeof(Ticket));
            container.RegisterAutoWiredType(typeof(ITicketAgent), typeof(TicketAgent));
            container.RegisterAutoWiredType(typeof(IOperatorAgent), typeof(OperatorAgent));
            container.RegisterAutoWiredType(typeof(ITicketStatus), typeof(TicketStatus));
            container.RegisterAutoWiredType(typeof(IReceipt), typeof(Receipt));

            var repo = new OrmLiteTicketRepository(dbFactory);
            container.Register<ITicketRepository>(c => repo);
            repo.InitSchema();

            var repo2 = new OrmLiteTicketAgentRepository(dbFactory);
            container.Register<ITicketAgentRepository>(c => repo2);

            var repo3 = new OrmLiteOperatorAgentRepository(dbFactory);
            container.Register<IOperatorAgentRepository>(c => repo3);
            repo3.InitSchema();

            var repo4 = new RedisTicketStatusRepository(container.Resolve<IRedisClientsManager>());
            container.Register<ITicketStatusRepository>(c => repo4);

            var repo5 = new OrmLiteReceiptRepository(dbFactory);
            container.Register<IReceiptRepository>(c => repo5);
            repo3.InitSchema();
        }

        private void ConfigureLogging(Container container, IAppSettings appSettings)
        {
            var logger = new SlackLogger(appSettings.Get<string>("slackIncomingWebHookUrl"));
            container.Register<ILog>(logger);
        }

        private void ConfigureAuth(Container container, IAppSettings appSettings)
        {
            //Register all Authentication methods you want to enable for this web app.
            Plugins.Add(new AuthFeature(() => new AuthUserSession(),
                new IAuthProvider[] {
                    new ApiKeyAuthProvider(appSettings),
                    new CredentialsAuthProvider() {
                         SessionExpiry = new TimeSpan(0, 30, 0)
                    },              //Sign-in with UserName/Password credentials
                    new BasicAuthProvider() {
                        SessionExpiry = new TimeSpan(0, 30, 0)
                    },                    //Sign-in with HTTP Basic Auth
                }
            )
            {
                IncludeAssignRoleServices = true,
                MaxLoginAttempts = 5,
                ServiceRoutes = new Dictionary<Type, string[]> {
                    { typeof(AuthenticateService), new[] {"/auth", "/auth/{provider}"} },
                },
                GenerateNewSessionCookiesOnAuthentication = true,
                DeleteSessionCookiesOnLogout = true,
                IncludeAuthMetadataProvider = false
            });

            Plugins.Add(new RegistrationFeature());

            //Store User Data into the referenced SQl server
            var repo = new OrmLiteAuthRepository(container.Resolve<IDbConnectionFactory>());
            container.Register<IAuthRepository>(c => repo);
            container.Register<IUserAuthRepository>(c => repo);
            repo.InitSchema();
        }

        private void ConfigureFilter(Container container, IAppSettings appSettings)
        {
            //Common
            Plugins.Add(new CommonFilters());
        }
        private void ConfigureValidation(Container container, IAppSettings appSettings)
        {
            //Common
            Plugins.Add(new CommonValidations());
        }
        private void ConfigureExceptionFormat(Container container, IAppSettings appSettings)
        {
            //Common
            Plugins.Add(new CommonExceptionFormats());
        }
        private void ConfigureConverter(Container container, IAppSettings appSettings)
        {
            //Common
            Plugins.Add(new CommonConverters());
        }


    }
}