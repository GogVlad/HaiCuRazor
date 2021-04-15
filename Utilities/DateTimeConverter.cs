using System;

namespace RazorMvc.Utilities
{
    public static class DateTimeConverter
    {
        public static DateTime ConvertEpochToDateTime(long ticks)
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(ticks);
            DateTime dateTime = dateTimeOffset.UtcDateTime;
            return dateTime;
        }
    }
}
