using ServiceStack.Configuration;
using Sima.Common.Constant;
using System.Text.RegularExpressions;

namespace Sima.Common.Validation
{
    public interface IPhoneValidator
    {
        bool ValidPhone(string phone, bool required = false);
    }

    public class PhoneValidator : IPhoneValidator
    {
        private string pattern { get; set; }
        public PhoneValidator()
        {
            pattern = CommonConstant.Pattern.GetValueOrDefault("phone");
        }
        public PhoneValidator(string _pattern)
        {
            pattern = _pattern;
        }
        public PhoneValidator(IAppSettings appSettings)
        {
            pattern = appSettings.Get("phone.pattern", CommonConstant.Pattern.GetValueOrDefault("phone"));
        }

        public bool ValidPhone(string phone, bool required = false)
        {
            if (required)
            {
                return Regex.IsMatch(phone, pattern);
            }
            return string.IsNullOrEmpty(phone) || Regex.IsMatch(phone, pattern);
        }
    }
}
