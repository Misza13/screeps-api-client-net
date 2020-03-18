namespace ScreepsApiClient
{
    using System;

    internal static class StringExtensions
    {
        private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        
        public static int ToInt(this string value)
        {
            return int.Parse(value);
        }

        public static DateTime ToDateTime(this int timeSeconds)
        {
            return epoch.AddSeconds(timeSeconds);
        }
    }
}