namespace Sima.Common.Validation
{
    public class AddressValidator : IAddressValidator
    {
        private const int MinLength = 5;
        private const int MaxLength = 250;

        public bool ValidAddress(string address, bool required = false)
        {
            if (required)
            {
                return !string.IsNullOrEmpty(address) && address.Length >= MinLength && address.Length <= MaxLength;
            }
            return string.IsNullOrEmpty(address) || (address.Length >= MinLength && address.Length <= MaxLength);
        }
    }
}
