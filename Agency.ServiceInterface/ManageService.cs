using Agency.Repository;
using Agency.RepositoryInterface;
using Agency.ServiceInterface.Resources;
using Agency.ServiceModel;
using ServiceStack;
using Sima.Common.Constant;
using Sima.Common.Service;

namespace Agency.ServiceInterface
{
    [RequiredRole("Admin")]
    public class ManageService : AuthOnlyService
    {
        public IOperatorAgentRepository oaRepo { get; set; }

        public object Post(AssignOperatorAgent request)
        {
            var eoa = oaRepo.GetOperatorAgent(request.OperatorId, request.AgentId);
            IOperatorAgent oa;
            if (eoa == null)
            {
                oa = oaRepo.CreateOperatorAgent(ToCreateOA(request));
            } else
            {
                oa = oaRepo.UpdateOperatorAgent(eoa, ToUpdateStatusOA(eoa, (int)DataStatus.Enabled));
            }
            return new AssignOperatorAgentResponse()
            {
                Status = (int)CommonStatus.Success
            };
        }

        public object Post(RemoveOperatorAgent request)
        {
            var eoa = oaRepo.GetOperatorAgent(request.OperatorId, request.AgentId);
            if(eoa == null)
            {
                throw new System.Exception(ManageMessage.OADoesNotExist);
            }
            var oa = oaRepo.UpdateOperatorAgent(eoa, ToUpdateStatusOA(eoa, (int)DataStatus.Disabled));
            return new AssignOperatorAgentResponse()
            {
                Status = (int)CommonStatus.Success
            };
        }

        private IOperatorAgent ToCreateOA(AssignOperatorAgent request)
        {
            var oa = request.ConvertTo<OperatorAgent>();
            oa.Status = (int)DataStatus.Enabled;
            return oa;
        }
        private IOperatorAgent ToUpdateStatusOA(IOperatorAgent existingOA, int status)
        {
            var oa = ((OperatorAgent)existingOA).CreateCopy();
            oa.Status = status;
            return oa;
        }
    }
}
