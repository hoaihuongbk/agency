using ServiceStack;

namespace Sima.Common.Plugin
{
    public class CommonFilters : IPlugin
    {
        public void Register(IAppHost appHost)
        {
            appHost.PreRequestFilters.Add((req, res) =>
            {
                var cultureName = req.GetHeader("X-Culture");
                if (string.IsNullOrEmpty(cultureName))
                {
                    cultureName = System.Globalization.CultureInfo.CurrentCulture.Name;
                }
                var culture = System.Globalization.CultureInfo.GetCultureInfo(cultureName);
                System.Threading.Thread.CurrentThread.CurrentCulture = culture;
                System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            });
            appHost.GlobalRequestFilters.Add(SessionFeature.AddSessionIdToRequestFilter);
        }
    }
}
