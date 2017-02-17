using System;

namespace PT.Common.Repository
{
    public interface IArea
    {
        int Id { get; set; }
        int BaseId { get; set; }
        int Type { get; set; }
        string Code { get; set; }
        string Name { get; set; }
        int Status { get; set; }
        DateTime IsPrgCreatedDate { get; set; }
        DateTime IsPrgUpdatedDate { get; set; }
        int IsPrgCreatedUserId { get; set; }
        int IsPrgUpdatedUserId { get; set; }
    }
}
