using ServiceStack;
using ServiceStack.Configuration;

namespace Sima.Common.Model
{
    public abstract class BaseRequest : IHasVersion
    {
        [ApiMember(Description = "Version. Ex: 1,2,3,...")]
        public int Version { get; set; }
        //public string Culture { get; set; }
        protected BaseRequest()
        {
            Version = ConfigUtils.GetAppSetting("AppVersion", 1);
            //Culture = System.Globalization.CultureInfo.CurrentCulture.Name; //Default culture
        }
    }

    //[ProtoContract]
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

    public interface IBaseData<T>
    {
        T Data { get; set; }
    }

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

    public abstract class PaginationResponse : BaseResponse
    {
        public int TotalRecord { get; set; }
    }
}