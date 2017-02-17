using System.Text.RegularExpressions;

namespace Sima.Common.Validation
{
    public class VietNamPhoneValidator : IPhoneValidator
    {
        private const string Pattern = @"0\d{9,10}";
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
