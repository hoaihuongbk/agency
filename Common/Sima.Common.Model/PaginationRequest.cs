using ServiceStack;

namespace Sima.Common.Model
{
    public abstract class PaginationRequest : BaseRequest
    {
        [ApiMember(IsRequired = true)]
        public int Page { get; set; }

        [ApiMember(IsRequired = true)]
        public int NumRowPerPage { get; set; }

        protected PaginationRequest()
        {
            Page = 1;
            NumRowPerPage = 10;
        }
    }
}