using System;
namespace Sima.App.Repository
{
    public interface IApp
    {
        int Id { get; set; }
        int Type { get; set; }
        string Code { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        DateTime ValidStartDate { get; set; }
        DateTime ValidEndDate { get; set; }
        int ValidStartDateOffset { get; set; }
        int MaxBookSeat { get; set; }
        int ExpiredBookingMinute { get; set; }
        int Status { get; set; }
        DateTime IsPrgCreatedDate { get; set; }
        DateTime IsPrgUpdatedDate { get; set; }
        int IsPrgCreatedUserId { get; set; }
        int IsPrgUpdatedUserId { get; set; }
    }
}
