namespace Sima.Common.Validation
{
    public interface IPhoneValidator
    {
        bool ValidPhone(string phone, bool required = false);
    }
}