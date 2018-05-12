using System.Collections.Generic;
using ServiceStack;

namespace Sima.Common.Model.Types
{
    public class CreateUserReponse : BaseResponse, IBaseData<CreateUserDataReponse>
    {
        public CreateUserDataReponse Data { get; set; }
    }
}