using ServiceStack;
using ServiceStack.Configuration;
using Sima.Common.Validation;

namespace Sima.Common.Plugin
{
    public class CommonValidations : IPlugin
    {
        public void Register(IAppHost appHost)
        {
            var appSettings = new AppSettings();

            appHost.Register<IAddressValidator>(new AddressValidator());
            appHost.Register<IDateStringValidator>(new DateStringValidator(appSettings));
            appHost.Register<IFullNameValidator>(new FullNameValidator());
            appHost.Register<IPasswordValidator>(new PasswordValidator());
            appHost.Register<IPhoneValidator>(new PhoneValidator(appSettings));
            appHost.Register<ITimeStringValidator>(new TimeStringValidator(appSettings));
            appHost.Register<IUserNameValidator>(new UserNameValidator());
            appHost.Register<ICustomEmailValidator>(new CustomEmailValidator());
        }
    }
}
