using System.Text.RegularExpressions;

namespace Sima.Common.Validation
{
    public interface IFullNameValidator
    {
        bool ValidFullName(string fullName, bool required = false);
    }

    public class FullNameValidator : IFullNameValidator
    {
        private const string ExcludeSymbol = @"[\@\%\/\\\&\?\,\;\:\!\-\>\<\=]+";
        private const int MinLen = 1;
        private const int MaxLen = 128;

        public bool ValidFullName(string fullName, bool required = false)
        {
            var validated = true;
            if (string.IsNullOrEmpty(fullName))
            {
                if (required)
                {
                    validated = false;
                }
            } else
            {
                if (fullName.Length < MinLen || fullName.Length > MaxLen)
                {
                    validated = false;
                }
                if(Regex.IsMatch(fullName, ExcludeSymbol))
                {
                    validated = false;
                }
            }
              
                //return !string.IsNullOrEmpty(fullName) && fullName.Length >= minLen && fullName.Length <= maxLen;
            return validated;
            //return string.IsNullOrEmpty(fullName) || (fullName.Length >= 1 && fullName.Length <= 128);
        }
    }
}
