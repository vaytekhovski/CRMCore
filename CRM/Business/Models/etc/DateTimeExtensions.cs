using System;

namespace Business
{
    public static class DateTimeExtensions
    {
        private static readonly long _unixEpochTicks = (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Ticks;

        public static long? ToJavascriptTicks(this DateTime? value)
        {
            return value == null ? (long?)null : (value.Value.ToUniversalTime().Ticks - _unixEpochTicks) / 10000;
        }
        public static long ToJavascriptTicks(this DateTime value)
        {
            return (value.ToUniversalTime().Ticks - _unixEpochTicks) / 10000;
        }
    }
}