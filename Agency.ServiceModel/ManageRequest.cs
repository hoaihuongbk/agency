using ServiceStack;
using Sima.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.ServiceModel
{
    [Route("/operatoragent/assign")]
    public class AssignOperatorAgent : BaseRequest, IReturn<AssignOperatorAgentResponse>
    {
        public int OperatorId { get; set; }
        public int AgentId { get; set; }
    }

    public class AssignOperatorAgentResponse : BaseResponse
    {
    }

    [Route("/operatoragent/remove")]
    public class RemoveOperatorAgent : BaseRequest, IReturn<RemoveOperatorAgentResponse>
    {
        public int OperatorId { get; set; }
        public int AgentId { get; set; }
    }

    public class RemoveOperatorAgentResponse : BaseResponse
    {

    }
}
