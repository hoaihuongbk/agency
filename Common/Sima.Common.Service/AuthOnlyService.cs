using ServiceStack;

namespace Sima.Common.Service
{
    [Authenticate]
    public abstract class AuthOnlyService : BaseService
    {

    }
}
