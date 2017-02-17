using System.Text.RegularExpressions;

namespace Sima.Common.Validation
{
    public interface IUserNameValidator
    {
        bool ValidUserName(string username);
    }
    public class UserNameValidator : IUserNameValidator
    {
        public bool ValidUserName(string username)
        {
            return Regex.IsMatch(username, @"^[a-zA-Z]{1}[a-zA-Z0-9\._\-]{0,23}[^.-]$");
        }
    }
}
