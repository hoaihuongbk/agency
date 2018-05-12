namespace Sima.Common.Model.Types
{
    public class PConvertResponse<T> : PaginationResponse, IBaseData<T>
    {
        public T Data { get; set; }
    }
}