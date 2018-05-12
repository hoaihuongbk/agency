namespace Sima.Common.Validation
{
    public interface ITimeStringValidator
    {
        bool ValidTimeString(string timeStr, bool required = false);
    }
}