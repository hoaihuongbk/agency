using Agency.RepositoryInterface;
using ServiceStack.DataAnnotations;
using System;
namespace Agency.Repository
{
	[Alias("tbl_Operator_Agent")]
	public class OperatorAgent : IOperatorAgent
	{
		[AutoIncrement]
				public int Id { get; set;} 
				public int OperatorId { get; set;} 
				public int AgentId { get; set;} 
				public int Type { get; set;} 
				[StringLength(128)]
			public string Code { get; set;} 
				[StringLength(256)]
			public string Name { get; set;} 
				public int Status { get; set;} 
				public DateTime IsPrgCreatedDate { get; set;} 
				public DateTime IsPrgUpdatedDate { get; set;} 
				public int IsPrgCreatedUserId { get; set;} 
				public int IsPrgUpdatedUserId { get; set;} 
	}
}
