using ServiceStack.Configuration;
using Sima.Common.Constant;
using System.Text.RegularExpressions;

namespace Sima.Common.Validation
{
    public class DateStringValidator : IDateStringValidator
    {
        private string Pattern { get; set; }

        public DateStringValidator()
        {
            Pattern = CommonConstant.Pattern.GetValueOrDefault("date");
        }
        public DateStringValidator(string pattern)
        {
            Pattern = pattern;
        }
        public DateStringValidator(IAppSettings appSettings)
        {
            Pattern = appSettings.Get("date.pattern", CommonConstant.Pattern.GetValueOrDefault("date"));
        }

        public bool ValidDateString(string dateStr, bool required = false)
        {
            if (required)
            {
                return !string.IsNullOrEmpty(dateStr) && Regex.IsMatch(dateStr, Pattern);
            }
            return string.IsNullOrEmpty(dateStr) || Regex.IsMatch(dateStr, Pattern);
        }
    }
}
