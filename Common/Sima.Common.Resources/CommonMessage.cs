using DbLocalizationProvider;

namespace Sima.Common.Resources
{
    [LocalizedResource]
    public class CommonMessage
    {
        public string InvalidRequest => "Request is invalid";
        public string NoData => "No response data";
        public string SystemError => "Opp. Error when process your request, please try again";
        public string UndefiedError => "Error is undefined";
    }
}