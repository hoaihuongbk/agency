using ServiceStack;

namespace Sima.Common.Service
{
    [Restrict(InternalOnly = true)]
    public abstract class InternelOnlyService : BaseService
    {
    }
}
