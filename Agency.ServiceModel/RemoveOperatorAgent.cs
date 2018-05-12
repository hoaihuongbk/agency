using ServiceStack;
using Sima.Common.Model;

namespace Agency.ServiceModel
{
    [Route("/operatoragent/remove")]
    public class RemoveOperatorAgent : BaseRequest, IReturn<RemoveOperatorAgentResponse>
    {
        public int OperatorId { get; set; }
        public int AgentId { get; set; }
    }
}