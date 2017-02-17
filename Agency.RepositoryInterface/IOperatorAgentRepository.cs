namespace Agency.RepositoryInterface
{
	public interface IOperatorAgentRepository
    {
        IOperatorAgent CreateOperatorAgent(IOperatorAgent newOperatorAgent);
        void DeleteOperatorAgent(int id);
        IOperatorAgent GetOperatorAgent(int id);
        IOperatorAgent GetOperatorAgent(int operatorId, int agentId);
        IOperatorAgent UpdateOperatorAgent(IOperatorAgent existingOperatorAgent, IOperatorAgent newOperatorAgent);
    }
}
