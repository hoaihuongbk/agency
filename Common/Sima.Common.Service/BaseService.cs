using Microsoft.Extensions.Localization;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.Logging;

namespace Sima.Common.Service
{
    public abstract class BaseService : ServiceStack.Service
    {
        protected AuthUserSession UserSession => SessionAs<AuthUserSession>();
        public IDbConnectionFactory DbFactory { get; set; }
        protected ILog Log { get; set; }
        protected readonly IStringLocalizer<BaseService> Localizer;

//        protected string RenderRazorViewToString(string viewName, object model)
//        {
//            var cultureName = System.Globalization.CultureInfo.CurrentCulture.Name;
//            switch (cultureName)
//            {
//                case "en-US":
//                    viewName = string.Format("{0}.{1}", viewName, cultureName);
//                    break;
//                default:
//                    break;
//            }
//
//            var razor = HostContext.GetPlugin<RazorFormat>();
//            var template = razor.GetViewPage(viewName);
//            if (template != null)
//            {
//                return razor.RenderToHtml(template, model);
//            }
//            throw new System.Exception("View Template not found");
//        }

        //public virtual GetErrorResponse OnError(CommonStatus error)
        //{
        //    return new GetErrorResponse()
        //    {
        //        Status = (int)error,
        //        Message = CommonMessage.UndefiedError
        //    };
        //}
    }
}
