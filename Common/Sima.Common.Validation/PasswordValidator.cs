namespace Sima.Common.Validation
{
    public class PasswordValidator : IPasswordValidator
    {
        public bool ValidPassword(string password)
        {
            return !string.IsNullOrEmpty(password)
               && password.Length >= 6
               && password.Length <= 128;
        }
    }
}
