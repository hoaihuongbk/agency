using System.Collections.Generic;

namespace Sima.Common.Constant
{
    public class CommonConstant
    {
        public static Dictionary<string, string> Pattern = new Dictionary<string, string>()
        {
            {"date",  @"^\d{2}-\d{2}-\d{4}$"},
            {"time",  @"^\d{2}:\d{2}$"},
            {"phone",  @"^\+?\d[\d\s.-]{7,11}$"}
        };
    }
}
