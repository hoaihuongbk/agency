using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Data;
using ServiceStack.FluentValidation;
using Sima.Common.Model;
using Sima.Common.Service;
using Sima.Common.Validation;

namespace Sima.Common.Plugin
{
    public class UserFeature : IPlugin
    {
        public void Register(IAppHost appHost)
        {
            //Pre init process
            
            // Dependency
            var container = appHost.GetContainer();
            var appSettings = appHost.AppSettings;
            
            //Register all Authentication methods you want to enable for this web app.
            appHost.Plugins.Add(new AuthFeature(() => new AuthUserSession(),
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
            
            //Store User Data into the referenced SQl server
            var repo = new OrmLiteAuthRepository(container.Resolve<IDbConnectionFactory>())
            {
                UseDistinctRoleTables = true
            };
            container.Register<IAuthRepository>(c => repo);
            container.Register<IUserAuthRepository>(c => repo);
            repo.InitSchema();
            
            //Custom validators
            appHost.Register<IValidator<CreateUser> >(new CreateUserValidator());
            
            //Custom services
            appHost.RegisterService<UserService>();
        }
    }
}