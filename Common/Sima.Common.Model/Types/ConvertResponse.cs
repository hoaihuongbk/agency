namespace Sima.Common.Model.Types
{
    public class ConvertResponse<T> : BaseResponse, IBaseData<T>
    {
        public T Data { get; set; }
    }
}