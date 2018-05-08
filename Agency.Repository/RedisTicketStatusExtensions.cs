﻿using ServiceStack;
using System;

namespace Agency.Repository
{
    public static class RedisTicketStatusExtensions
    {
        public static string GetTicketStatusKey(int operatorId, DateTime departureDate, string departureTime)
        {
            return "{0}:{1:yyyyMMdd}:{2}".Fmt(operatorId, departureDate, departureTime.Replace(":", "").Trim());
        }
    }
}
