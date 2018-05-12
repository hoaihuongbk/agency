namespace Sima.Common.Validation
{
    public interface IDateStringValidator
    {
        bool ValidDateString(string dateStr, bool required = false);
    }
}