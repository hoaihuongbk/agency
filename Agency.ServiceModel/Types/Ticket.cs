using System;

namespace Agency.ServiceModel.Types
{
    public class Ticket
    {
        public int Id { get; set; }
        public int UserAuthId { get; set; }
        public int Type { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int XAsis { get; set; }
        public int YAsis { get; set; }
        public int FromAreaId { get; set; }
        public int ToAreaId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string DepartureTime { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
        public int Surcharge { get; set; }
        public int Total { get; set; }
        public int Deposit { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
    }
}
