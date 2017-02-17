using ServiceStack.DataAnnotations;
using System;
namespace Sima.App.Repository.OrmLite
{
    [Alias("tbl_App")]
    public class App : IApp
    {
        [AutoIncrement]
        public int Id { get; set; }
        public int Type { get; set; }
        [StringLength(128)]
        public string Code { get; set; }
        [StringLength(128)]
        public string Name { get; set; }
        [StringLength(512)]
        public string Description { get; set; }
        public DateTime ValidStartDate { get; set; }
        public DateTime ValidEndDate { get; set; }
        public int ValidStartDateOffset { get; set; }
        public int MaxBookSeat { get; set; }
        public int ExpiredBookingMinute { get; set; }
        public int Status { get; set; }
        public DateTime IsPrgCreatedDate { get; set; }
        public DateTime IsPrgUpdatedDate { get; set; }
        public int IsPrgCreatedUserId { get; set; }
        public int IsPrgUpdatedUserId { get; set; }
    }
}
