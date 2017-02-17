using Sima.Common.Helper;
using ServiceStack;
using ServiceStack.Logging;
using ServiceStack.Text;

namespace Sima.Common.Plugin
{
    public class CommonExceptionFormats : IPlugin
    {
        public void Register(IAppHost appHost)
        {
            appHost.ServiceExceptionHandlers.Add((httpReq, request, exception) => CommonUtils.CreateErrorResponse(request, exception));
            appHost.UncaughtExceptionHandlers.Add((req, res, operationName, ex) =>
            {
                var logger = appHost.GetContainer().Resolve<ILog>();
                logger.Error(string.Format("UncaughtException: OperationName: {0} | Request: {1} | Response: {2} | ErrorMessage: {3}", operationName, JsonSerializer.SerializeToString(req), JsonSerializer.SerializeToString(res), ex.Message));
                res.Write("Error: {0}".Fmt(ex.Message));
                res.EndRequest(true);
            });
        }
    }
}
