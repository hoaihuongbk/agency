using System;
using System.Collections.Generic;
using Funq;
using Receip.ServiceInterface;
using Receipt.Plugin;
using ServiceStack;
using ServiceStack.Configuration;
using ServiceStack.Data;
using ServiceStack.Logging;
using ServiceStack.OrmLite;
using ServiceStack.Text;
using ServiceStack.Validation;
using Sima.Common.Logging;
using Sima.Common.Plugin;
using Ticket.Plugin;

namespace Receipt
{
    //VS.NET Template Info: https://servicestack.net/vs-templates/EmptyAspNet
    public class AppHost : AppHostBase
    {
        /// <summary>
        /// Base constructor requires a Name and Assembly where web service implementation is located
        /// </summary>
        public AppHost()
            : base("Agency", typeof(ReceiptService).Assembly) { }

        /// <summary>
        /// Application specific configuration
        /// This method should initialize any IoC resources utilized by your web service classes.
        /// </summary>
        public override void Configure(Container container)
        {
            var appSettings = AppSettings;

            JsConfig.EmitCamelCaseNames = true;
            JsConfig<DateTime>.SerializeFn = dateObj => string.Format("{0:yyyy-MM-ddTHH:mm:ss.000}", dateObj);
            JsConfig<TimeSpan>.SerializeFn = timeSpan => string.Format("{0:00}:{1:00}", timeSpan.Hours, timeSpan.Minutes);
            JsConfig<DBNull>.SerializeFn = dbNull => string.Empty;
        
            //Configuring
            ConfigureRedis(container, appSettings);
            ConfigureDb(container, appSettings);
            ConfigureRepository(container, appSettings);
            ConfigureLogging(container, appSettings);
            ConfigureAuth(container, appSettings);
            ConfigureFilter(container, appSettings);
            ConfigureValidation(container, appSettings);
            ConfigureExceptionFormat(container, appSettings);
            ConfigureConverter(container, appSettings);

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
        }

        private void ConfigureRedis(Container container, IAppSettings appSettings)
        {
           Plugins.Add(new RedisFeature());
        }
        
        private void ConfigureDb(Container container, IAppSettings appSettings)
        {
            var connections = appSettings.Get<Dictionary<string, string>>("connectionStrings");
            
            var dbFactory = new OrmLiteConnectionFactory(connections.GetValueOrDefault("Agency"), MySqlDialect.Provider);
            container.Register<IDbConnectionFactory>(dbFactory);
        }
        
        private void ConfigureRepository(Container container, IAppSettings appSettings)
        {
            Plugins.Add(new ReceiptRepositoryFeature());
            Plugins.Add(new TicketRepositoryFeature());
        }

        private void ConfigureLogging(Container container, IAppSettings appSettings)
        {
            var logger = new SlackLogger(appSettings.Get<string>("slackIncomingWebHookUrl"));
            container.Register<ILog>(logger);
        }

        private void ConfigureAuth(Container container, IAppSettings appSettings)
        {
            Plugins.Add(new UserFeature());
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