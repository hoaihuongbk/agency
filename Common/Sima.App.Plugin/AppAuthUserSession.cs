using ServiceStack;
using ServiceStack.Auth;
using Sima.App.Repository;
using System;
using System.Collections.Generic;

namespace Sima.App.Plugin
{
    public class AppAuthUserSession : AuthUserSession
    {
        public int AppId { get; set; }
        public string AppName { get; set; }
        public DateTime ValidStartDate { get; set; }
        public DateTime ValidEndDate { get; set; }
        public int ValidStartDateOffset { get; set; }
        public int MaxBookSeat { get; set; }
        public int ExpiredBookingMinute { get; set; }

        public override void OnAuthenticated(IServiceBase authService, IAuthSession session, IAuthTokens tokens, Dictionary<string, string> authInfo)
        {
            base.OnAuthenticated(authService, session, tokens, authInfo);

            var xapp = HostContext.AppHost.TryGetCurrentRequest().Headers["X-App"];
            if (!string.IsNullOrEmpty(xapp))
            {
                var appRepo = authService.ResolveService<IAppRepository>();
                var app = appRepo.GetAppByCode(xapp);
                if (app == null)
                {
                    throw new Exception("App does not existing");
                }

                //Populate
                this.AppId = app.Id;
                this.AppName = app.Name;
                this.ValidStartDate = app.ValidStartDate;
                this.ValidEndDate = app.ValidEndDate;
                this.ValidStartDateOffset = app.ValidStartDateOffset;
                this.MaxBookSeat = app.MaxBookSeat;
                this.ExpiredBookingMinute = app.ExpiredBookingMinute;

                authService.SaveSession(this, SessionFeature.DefaultSessionExpiry);
            }
        }
    }
}
