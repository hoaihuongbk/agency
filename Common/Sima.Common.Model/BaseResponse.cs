using ServiceStack;

namespace Sima.Common.Model
{
    public abstract class BaseResponse
    {
        [ApiMember(Description = "Response status. Ex: 1 - Success; 0 - Error")]
        //[ProtoMember(1)]
        public int Status { get; set; }

        [ApiMember(Description = "Response message")]
        //[ProtoMember(2)]
        public string Message { get; set; }

        protected BaseResponse()
        {
            Status = 1;
        }
    }
}