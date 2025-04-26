using System;

namespace BookingManager.Domain.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime GetDateTimeNow()
        {
            return DateTime.UtcNow;
        }
    }
}

