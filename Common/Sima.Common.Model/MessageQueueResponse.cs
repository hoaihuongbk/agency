namespace Sima.Common.Model
{
    public class SendMqEmailResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public string MqKey { get; set; }
    }

    public class SendMqSMSResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public string SMSIDRef { get; set; }
        public string MqKey { get; set; }
    }
}
