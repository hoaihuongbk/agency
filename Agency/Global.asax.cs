using System;

namespace Agency
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_PreSendRequestHeaders()
        {
            Response.Headers.Remove("X-Powered-By");
        }
        protected void Application_Start(object sender, EventArgs e)
        {
            new AppHost().Init();
        }
    }
}