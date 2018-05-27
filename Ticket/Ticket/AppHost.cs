using System;
using System.Collections.Generic;
using Funq;
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
using Ticket.ServiceInterface;

namespace Ticket
{
    //VS.NET Template Info: https://servicestack.net/vs-templates/EmptyAspNet
    public class AppHost : AppHostBase
    {
        /// <summary>
        /// Base constructor requires a Name and Assembly where web service implementation is located
        /// </summary>
        public AppHost()
            : base("Agency", typeof(TicketService).Assembly) { }

//        public override void OnBeforeInit()
//        {
//            base.OnBeforeInit();
//
//            var userAuthType = typeof(UserAuth);
//            userAuthType.AddAttributes(new AliasAttribute("tbl_user_auth"));
//            foreach (var prop in userAuthType.GetProperties())
//            {
//                prop.AddAttributes(new AliasAttribute(prop.Name.ToUnderscoreCase()));
//            }
//
//            var apiKeyType = typeof(ApiKey);
//            apiKeyType.AddAttributes(new AliasAttribute("tbl_api_key"));
//            foreach (var prop in apiKeyType.GetProperties())
//            {
//                prop.AddAttributes(new AliasAttribute(prop.Name.ToUnderscoreCase()));
//            }
//            
//            var userAuthRoleType = typeof(UserAuthRole);
//            userAuthRoleType.AddAttributes(new AliasAttribute("tbl_user_auth_role"));
//            foreach (var prop in userAuthRoleType.GetProperties())
//            {
//                prop.AddAttributes(new AliasAttribute(prop.Name.ToUnderscoreCase()));
//            }
//            
//            var userAuthDetails = typeof(UserAuthDetails);
//            userAuthDetails.AddAttributes(new AliasAttribute("tbl_user_auth_details"));
//            foreach (var prop in userAuthDetails.GetProperties())
//            {
//                prop.AddAttributes(new AliasAttribute(prop.Name.ToUnderscoreCase()));
//            }
//            
//
////            typeof(RegisterService).AddAttributes(new RestrictAttribute() { VisibilityTo = RequestAttributes.None });
////            typeof(GetApiKeysService).AddAttributes(new RestrictAttribute() { VisibilityTo = RequestAttributes.None });
////            typeof(RegenerateApiKeysService).AddAttributes(new RestrictAttribute() { VisibilityTo = RequestAttributes.None });
//        }

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
            ConfigureRedis(appSettings);
            ConfigureDb(container, appSettings);
            ConfigureRepository();
            ConfigureLogging(container, appSettings);
            ConfigureAuth();
            ConfigureFilter();
            ConfigureValidation();
            ConfigureExceptionFormat();
            ConfigureConverter();

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

        private void ConfigureRedis(IAppSettings appSettings)
        {
            Plugins.Add(new RedisFeature());
        }
        
        private void ConfigureDb(Container container, IAppSettings appSettings)
        {
            var connections = appSettings.Get<Dictionary<string, string>>("connectionStrings");
            
            var dbFactory = new OrmLiteConnectionFactory(connections.GetValueOrDefault("Agency"), MySqlDialect.Provider);
            container.Register<IDbConnectionFactory>(dbFactory);
        }
        
        private void ConfigureRepository()
        {
           Plugins.Add(new TicketRepositoryFeature());
        }

        private void ConfigureLogging(Container container, IAppSettings appSettings)
        {
            var logger = new SlackLogger(appSettings.Get<string>("slackIncomingWebHookUrl"));
            container.Register<ILog>(logger);
        }

        private void ConfigureAuth()
        {
            Plugins.Add(new UserFeature());
        }

        private void ConfigureFilter()
        {
            //Common
            Plugins.Add(new CommonFilters());
        }
        private void ConfigureValidation()
        {
            //Common
            Plugins.Add(new CommonValidations());
        }
        private void ConfigureExceptionFormat()
        {
            //Common
            Plugins.Add(new CommonExceptionFormats());
        }
        private void ConfigureConverter()
        {
            //Common
            Plugins.Add(new CommonConverters());
        }
    }
}