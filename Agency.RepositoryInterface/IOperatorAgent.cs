using System;
namespace Agency.RepositoryInterface
{
	public interface IOperatorAgent
	{
						int Id { get; set;} 
						int OperatorId { get; set;} 
						int AgentId { get; set;} 
						int Type { get; set;} 
						string Code { get; set;} 
						string Name { get; set;} 
						int Status { get; set;} 
						DateTime IsPrgCreatedDate { get; set;} 
						DateTime IsPrgUpdatedDate { get; set;} 
						int IsPrgCreatedUserId { get; set;} 
						int IsPrgUpdatedUserId { get; set;} 
			}
}
