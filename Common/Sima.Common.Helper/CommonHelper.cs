using System.Linq;
using ServiceStack;
using ServiceStack.Configuration;
using Sima.Common.Constant;

namespace Sima.Common.Helper
{
    public static class CommonHelper
    {
        public static bool HasProperty(this object obj, string propName)
        {
            var type = obj.GetType();
            return type.GetProperty(propName) != null;
        }

        public static string GetPattern(string prefix)
        {
            var key = "{0}.pattern".Fmt(prefix.Trim());
            return ConfigUtils.GetAppSetting(key, CommonConstant.Pattern[prefix.Trim()]);
        }
        public static string ToUnderscoreCase(this string str) {
            return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
        }
    }
}
