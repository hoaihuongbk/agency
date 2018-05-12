using ServiceStack;
using Sima.Common.Model;

namespace Agency.ServiceModel
{
    [Route("/operatoragent/assign")]
    public class AssignOperatorAgent : BaseRequest, IReturn<AssignOperatorAgentResponse>
    {
        public int OperatorId { get; set; }
        public int AgentId { get; set; }
    }
}
