using System.Text.RegularExpressions;

namespace Sima.Common.Validation
{
    public interface ICustomEmailValidator
    {
        bool ValidEmail(string email, bool required = false);
    }
    public class CustomEmailValidator : ICustomEmailValidator
    {
        public bool ValidEmail(string email, bool required = false)
        {
            var validated = true;
            if (string.IsNullOrEmpty(email))
            {
                if (required)
                {
                    validated = false;
                }
            }
            else
            {
                validated = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            }
            return validated;
        }
    }
}
