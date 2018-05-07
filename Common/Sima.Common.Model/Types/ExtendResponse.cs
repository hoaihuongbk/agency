namespace Sima.Common.Model.Types
{
    public class BasicResponse : BaseResponse
    {
    }

    public class ConvertResponse<T> : BaseResponse, IBaseData<T>
    {
        public T Data { get; set; }
    }

    public class PConvertResponse<T> : PaginationResponse, IBaseData<T>
    {
        public T Data { get; set; }
    }
}
