using ServiceStack.Configuration;
using Sima.Common.Constant;
using System.Text.RegularExpressions;

namespace Sima.Common.Validation
{
    public interface ITimeStringValidator
    {
        bool ValidTimeString(string timeStr, bool required = false);
    }
    public class TimeStringValidator : ITimeStringValidator
    {
        private string Pattern { get; set; }

        public TimeStringValidator()
        {
            Pattern = CommonConstant.Pattern.GetValueOrDefault("time");
        }
        public TimeStringValidator(string pattern)
        {
            Pattern = pattern;
        }
        public TimeStringValidator(IAppSettings appSettings)
        {
            Pattern = appSettings.Get("time.pattern", CommonConstant.Pattern.GetValueOrDefault("time"));
        }

        public bool ValidTimeString(string timeStr, bool required = false)
        {
            if (required)
            {
                return !string.IsNullOrEmpty(timeStr) && Regex.IsMatch(timeStr, Pattern);
            }
            return string.IsNullOrEmpty(timeStr) || Regex.IsMatch(timeStr, Pattern);
        }
    }
}
