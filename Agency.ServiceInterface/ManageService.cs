﻿using Agency.Repository;
using Agency.RepositoryInterface;
using Agency.ServiceModel;
using ServiceStack;
using Sima.Common.Constant;
using Sima.Common.Service;

namespace Agency.ServiceInterface
{
    [RequiredRole("Admin")]
    public class ManageService : AuthOnlyService
    {
        private IOperatorAgentRepository OaRepo { get; set; }

        public object Post(AssignOperatorAgent request)
        {
            var eoa = OaRepo.GetOperatorAgent(request.OperatorId, request.AgentId);
            IOperatorAgent oa;
            oa = eoa == null ? OaRepo.CreateOperatorAgent(ToCreateOa(request)) : OaRepo.UpdateOperatorAgent(eoa, ToUpdateStatusOa(eoa, (int)DataStatus.Enabled));
            return new AssignOperatorAgentResponse()
            {
                Status = (int)CommonStatus.Success
            };
        }

        public object Post(RemoveOperatorAgent request)
        {
            var eoa = OaRepo.GetOperatorAgent(request.OperatorId, request.AgentId);
            if(eoa == null)
            {
//                throw new System.Exception(ManageMessage.OADoesNotExist);
            }
            var oa = OaRepo.UpdateOperatorAgent(eoa, ToUpdateStatusOa(eoa, (int)DataStatus.Disabled));
            return new AssignOperatorAgentResponse()
            {
                Status = (int)CommonStatus.Success
            };
        }

        private IOperatorAgent ToCreateOa(AssignOperatorAgent request)
        {
            var oa = request.ConvertTo<OperatorAgent>();
            oa.Status = (int)DataStatus.Enabled;
            return oa;
        }
        private IOperatorAgent ToUpdateStatusOa(IOperatorAgent existingOa, int status)
        {
            var oa = ((OperatorAgent)existingOa).CreateCopy();
            oa.Status = status;
            return oa;
        }
    }
}
