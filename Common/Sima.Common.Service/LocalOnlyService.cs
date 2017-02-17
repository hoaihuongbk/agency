using ServiceStack;

namespace Sima.Common.Service
{
    [Restrict(LocalhostOnly = true)]
    public abstract class LocalOnlyService : BaseService
    {
    }
}
