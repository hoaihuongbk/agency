using System;
using ServiceStack.DataAnnotations;

namespace Agency.Repository.OrmLite
{
	[Alias("tbl_Receipt")]
	public class Receipt : IReceipt
	{
		[AutoIncrement]
				public int Id { get; set;} 
				public int AgentId { get; set;} 
				public int TicketId { get; set;} 
				public int Type { get; set;} 
				[StringLength(128)]
			public string Code { get; set;} 
				[StringLength(256)]
			public string Name { get; set;} 
				[StringLength(512)]
			public string Description { get; set;} 
				public int FromAreaId { get; set;} 
				public int ToAreaId { get; set;} 
				public DateTime DepartureDate { get; set;} 
				[StringLength(50)]
			public string DepartureTime { get; set;} 
				public int Price { get; set;} 
				public int Discount { get; set;} 
				public int Surcharge { get; set;} 
				public int Total { get; set;} 
				public int Deposit { get; set;} 
				[StringLength(1024)]
			public string Note { get; set;} 
				public int Status { get; set;} 
				public DateTime IsPrgCreatedDate { get; set;} 
				public DateTime IsPrgUpdatedDate { get; set;} 
				public int IsPrgCreatedUserId { get; set;} 
				public int IsPrgUpdatedUserId { get; set;} 
	}
}
