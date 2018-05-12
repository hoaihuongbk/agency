using ServiceStack.DataAnnotations;
using System;

namespace PT.Common.Repository.OrmLite
{
    [Alias("tbl_area")]
    public class Area : IArea
    {
        [AutoIncrement]
        [Alias("id")] 
        public int Id { get; set; }
        [Alias("base_id")] 
        public int BaseId { get; set; }
        [Alias("type")] 
        public int Type { get; set; }
        [StringLength(128)]
        [Alias("code")] 
        public string Code { get; set; }
        [StringLength(256)]
        [Alias("name")] 
        public string Name { get; set; }
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
