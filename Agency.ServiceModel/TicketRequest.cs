using ServiceStack;
using Sima.Common.Model;
using System.Collections.Generic;

namespace Agency.ServiceModel
{
    [Route("/tickets", "GET,POST")]
    public class GetTickets : PaginationRequest, IReturn<GetTicketsResponse>
    {
        [ApiMember(Description = "dd-MM-yyyy")]
        public string DepartureDate { get; set; }
    }

    public class GetTicketsResponse : PaginationResponse, IBaseData<List<Types.TicketAgent>> 
    {
        public List<Types.TicketAgent> Data { get; set; }
    }

    [Route("/ticket/add", "POST")]
    public class AddTicket : BaseRequest, IReturn<AddTicketResponse>
    {
        public int Type { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int XAsis { get; set; }
        public int YAsis { get; set; }
        public int FromAreaId { get; set; }
        public int ToAreaId { get; set; }
        [ApiMember(Description = "dd-MM-yyyy")]
        public string StartDate { get; set; }
        [ApiMember(Description = "dd-MM-yyyy")]
        public string EndDate { get; set; }
        public string DepartureTime { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
        public int Surcharge { get; set; }
        public int Total { get; set; }
        public int Deposit { get; set; }
        public string Note { get; set; }
    }
    public class AddTicketResponse : BaseResponse, IBaseData<Types.Ticket>
    {
        public Types.Ticket Data { get; set; }
    }

    [Route("/ticket/update", "POST")]
    public class UpdateTicket : BaseRequest, IReturn<UpdateTicketResponse>
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int XAsis { get; set; }
        public int YAsis { get; set; }
        public int FromAreaId { get; set; }
        public int ToAreaId { get; set; }
        [ApiMember(Description = "dd-MM-yyyy")]
        public string StartDate { get; set; }
        [ApiMember(Description = "dd-MM-yyyy")]
        public string EndDate { get; set; }
        public string DepartureTime { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
        public int Surcharge { get; set; }
        public int Total { get; set; }
        public int Deposit { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
    }

    public class UpdateTicketResponse : BaseResponse, IBaseData<Types.Ticket>
    {
        public Types.Ticket Data { get; set; }
    }
}
