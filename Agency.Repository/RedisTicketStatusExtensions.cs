using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Repository
{
    public class RedisTicketStatusExtensions
    {
        public static string GetTicketStatusKey(int operatorId, DateTime departureDate, string departureTime)
        {
            return "{0}:{1:yyyyMMdd}:{2}".Fmt(operatorId, departureDate, departureTime.Replace(":", "").Trim());
        }
    }
}
