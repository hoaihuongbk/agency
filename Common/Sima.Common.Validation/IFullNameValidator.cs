namespace Sima.Common.Validation
{
    public interface IFullNameValidator
    {
        bool ValidFullName(string fullName, bool required = false);
    }
}