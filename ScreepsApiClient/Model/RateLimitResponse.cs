namespace ScreepsApiClient.Model
{
    using System;

    public class RateLimitResponse
    {
        public int RateLimit { get; internal set; }

        public int RateLimitRemaining { get; internal set; }

        public DateTime RateLimitReset { get; internal set; }
    }
}