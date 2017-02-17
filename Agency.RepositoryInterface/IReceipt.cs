using System;
namespace Agency.RepositoryInterface
{
	public interface IReceipt
	{
						int Id { get; set;} 
						int AgentId { get; set;} 
						int TicketId { get; set;} 
						int Type { get; set;} 
						string Code { get; set;} 
						string Name { get; set;} 
						string Description { get; set;} 
						int FromAreaId { get; set;} 
						int ToAreaId { get; set;} 
						DateTime DepartureDate { get; set;} 
						string DepartureTime { get; set;} 
						int Price { get; set;} 
						int Discount { get; set;} 
						int Surcharge { get; set;} 
						int Total { get; set;} 
						int Deposit { get; set;} 
						string Note { get; set;} 
						int Status { get; set;} 
						DateTime IsPrgCreatedDate { get; set;} 
						DateTime IsPrgUpdatedDate { get; set;} 
						int IsPrgCreatedUserId { get; set;} 
						int IsPrgUpdatedUserId { get; set;} 
			}
}
