using ServiceStack.Configuration;
using Sima.Common.Constant;
using System.Text.RegularExpressions;

namespace Sima.Common.Validation
{
    public interface IDateStringValidator
    {
        bool ValidDateString(string dateStr, bool required = false);
    }
    public class DateStringValidator : IDateStringValidator
    {
        private string pattern { get; set; }

        public DateStringValidator()
        {
            pattern = CommonConstant.Pattern.GetValueOrDefault("date");
        }
        public DateStringValidator(string _pattern)
        {
            pattern = _pattern;
        }
        public DateStringValidator(IAppSettings appSettings)
        {
            pattern = appSettings.Get("date.pattern", CommonConstant.Pattern.GetValueOrDefault("date"));
        }

        public bool ValidDateString(string dateStr, bool required = false)
        {
            if (required)
            {
                return !string.IsNullOrEmpty(dateStr) && Regex.IsMatch(dateStr, pattern);
            }
            return string.IsNullOrEmpty(dateStr) || Regex.IsMatch(dateStr, pattern);
        }
    }
}
