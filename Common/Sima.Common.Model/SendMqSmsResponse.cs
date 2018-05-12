namespace Sima.Common.Model
{
    public class SendMqSmsResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public string SmsidRef { get; set; }
        public string MqKey { get; set; }
    }
}