using ServiceStack;
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
            appHost.Register<IValidator<CreateUser> >(new CreateUserValidator());
            appHost.RegisterService<UserService>();
        }
    }
}