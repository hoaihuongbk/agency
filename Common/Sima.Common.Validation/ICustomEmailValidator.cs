namespace Sima.Common.Validation
{
    public interface ICustomEmailValidator
    {
        bool ValidEmail(string email, bool required = false);
    }
}