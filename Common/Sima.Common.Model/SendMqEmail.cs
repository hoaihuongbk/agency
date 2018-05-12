namespace Sima.Common.Model
{
    public class SendMqEmail
    {
        public string To { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public string MqKey { get; set; }
    }
}
