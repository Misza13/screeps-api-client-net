namespace ScreepsApiClient.Model
{
    public class GetUserMemoryResponse<T> : RateLimitResponse
    {
        public T Memory { get; set; }
    }
}