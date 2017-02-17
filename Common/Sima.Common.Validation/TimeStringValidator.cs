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
        private string pattern { get; set; }

        public TimeStringValidator()
        {
            pattern = CommonConstant.Pattern["time"];
        }
        public TimeStringValidator(string _pattern)
        {
            pattern = _pattern;
        }
        public TimeStringValidator(IAppSettings appSettings)
        {
            pattern = appSettings.Get("time.pattern", CommonConstant.Pattern["time"]);
        }

        public bool ValidTimeString(string timeStr, bool required = false)
        {
            if (required)
            {
                return !string.IsNullOrEmpty(timeStr) && Regex.IsMatch(timeStr, pattern);
            }
            return string.IsNullOrEmpty(timeStr) || Regex.IsMatch(timeStr, pattern);
        }
    }
}
