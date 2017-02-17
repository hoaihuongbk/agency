using ServiceStack.DataAnnotations;
using System;

namespace PT.Common.Repository.OrmLite
{
    [Alias("tbl_Area")]
    public class Area : IArea
    {
        [AutoIncrement]
        public int Id { get; set; }
        public int BaseId { get; set; }
        public int Type { get; set; }
        [StringLength(128)]
        public string Code { get; set; }
        [StringLength(256)]
        public string Name { get; set; }
        public int Status { get; set; }
        public DateTime IsPrgCreatedDate { get; set; }
        public DateTime IsPrgUpdatedDate { get; set; }
        public int IsPrgCreatedUserId { get; set; }
        public int IsPrgUpdatedUserId { get; set; }
    }
}
