using ServiceStack;
using ServiceStack.Configuration;
using Sima.Common.Validation;

namespace Sima.Common.Plugin
{
    public class CommonValidations : IPlugin
    {
        public void Register(IAppHost appHost)
        {
            appHost.Register<IAddressValidator>(new AddressValidator());
            appHost.Register<IDateStringValidator>(new DateStringValidator(appHost.AppSettings));
            appHost.Register<IFullNameValidator>(new FullNameValidator());
            appHost.Register<IPasswordValidator>(new PasswordValidator());
            appHost.Register<IPhoneValidator>(new PhoneValidator(appHost.AppSettings));
            appHost.Register<ITimeStringValidator>(new TimeStringValidator(appHost.AppSettings));
            appHost.Register<IUserNameValidator>(new UserNameValidator());
            appHost.Register<ICustomEmailValidator>(new CustomEmailValidator());
        }
    }
}
