namespace Sima.Common.Model
{
    public class SendMqEmail
    {
        public string To { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public string MqKey { get; set; }
    }

    public class SendMqSms
    {
        public string Phone { get; set; }
        public string Message { get; set; }
        public string MqKey { get; set; }
    }
}
