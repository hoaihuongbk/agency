using System;

namespace Ticket.Repository
{
    public interface ITicketAgent
    {
        int Id { get; set; }
        int OperatorId { get; set; }
        int AgentId { get; set; }
        int Type { get; set; }
        string Code { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        int XAsis { get; set; }
        int YAsis { get; set; }
        int FromAreaId { get; set; }
        int ToAreaId { get; set; }
        DateTime? FromDate { get; set; }
        DateTime? ToDate { get; set; }
        string DepartureTime { get; set; }
        int Price { get; set; }
        int Discount { get; set; }
        int Surcharge { get; set; }
        int Total { get; set; }
        int Deposit { get; set; }
        string Note { get; set; }
        int Status { get; set; }
        DateTime IsPrgCreatedDate { get; set; }
        DateTime IsPrgUpdatedDate { get; set; }
        int IsPrgCreatedUserId { get; set; }
        int IsPrgUpdatedUserId { get; set; }
    }
}
