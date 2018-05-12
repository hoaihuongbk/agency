namespace Sima.Common.Validation
{
    public interface IAddressValidator
    {
        bool ValidAddress(string address, bool required = false);
    }
}