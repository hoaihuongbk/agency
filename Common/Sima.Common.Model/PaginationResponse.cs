namespace Sima.Common.Model
{
    public abstract class PaginationResponse : BaseResponse
    {
        public int TotalRecord { get; set; }
    }
}