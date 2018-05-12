using ServiceStack.Configuration;
using Sima.Common.Constant;
using System.Text.RegularExpressions;

namespace Sima.Common.Validation
{
    public class PhoneValidator : IPhoneValidator
    {
        private string Pattern { get; set; }
        public PhoneValidator()
        {
            Pattern = CommonConstant.Pattern.GetValueOrDefault("phone");
        }
        public PhoneValidator(string pattern)
        {
            Pattern = pattern;
        }
        public PhoneValidator(IAppSettings appSettings)
        {
            Pattern = appSettings.Get("phone.pattern", CommonConstant.Pattern.GetValueOrDefault("phone"));
        }

        public bool ValidPhone(string phone, bool required = false)
        {
            if (required)
            {
                return Regex.IsMatch(phone, Pattern);
            }
            return string.IsNullOrEmpty(phone) || Regex.IsMatch(phone, Pattern);
        }
    }
}
