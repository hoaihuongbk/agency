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
                logger.Error(
                    $"UncaughtException: OperationName: {operationName} | Request: {JsonSerializer.SerializeToString(req)} | Response: {JsonSerializer.SerializeToString(res)} | ErrorMessage: {ex.Message}");
                res.WriteAsync("Error: {0}".Fmt(ex.Message));
                res.EndRequest(true);
            });
        }
    }
}
