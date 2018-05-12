using System;
using ServiceStack.DataAnnotations;

namespace Agency.Repository.OrmLite
{
    [Alias("tbl_ticket")]
    public class Ticket : ITicket
    {
        [AutoIncrement]
        [Alias("id")] 
        public int Id { get; set; }
        [Alias("user_auth_id")] 
        public int UserAuthId { get; set; }
        [Alias("type")] 
        public int Type { get; set; }
        [StringLength(128)]
        [Alias("code")] 
        public string Code { get; set; }
        [StringLength(256)]
        [Alias("name")] 
        public string Name { get; set; }
        [StringLength(512)]
        [Alias("description")] 
        public string Description { get; set; }
        [Alias("x_asis")] 
        public int XAsis { get; set; }
        [Alias("y_axis")] 
        public int YAsis { get; set; }
        [Alias("from_area_id")] 
        public int FromAreaId { get; set; }
        [Alias("to_area_id")] 
        public int ToAreaId { get; set; }
        [Alias("from_date")] 
        public DateTime? FromDate { get; set; }
        [Alias("to_date")] 
        public DateTime? ToDate { get; set; }
        [StringLength(50)]
        [Alias("departure_time")] 
        public string DepartureTime { get; set; }
        [Alias("price")] 
        public int Price { get; set; }
        [Alias("discount")] 
        public int Discount { get; set; }
        [Alias("surcharge")] 
        public int Surcharge { get; set; }
        [Alias("total")] 
        public int Total { get; set; }
        [Alias("deposit")] 
        public int Deposit { get; set; }
        [StringLength(1024)]
        [Alias("note")] 
        public string Note { get; set; }
        [Alias("status")] 
        public int Status { get; set; }
        [Alias("is_prg_created_date")] 
        public DateTime IsPrgCreatedDate { get; set; }
        [Alias("is_prg_updated_date")] 
        public DateTime IsPrgUpdatedDate { get; set; }
        [Alias("is_prg_created_user_id")] 
        public int IsPrgCreatedUserId { get; set; }
        [Alias("is_prg_updated_user_id")] 
        public int IsPrgUpdatedUserId { get; set; }
    }
}
