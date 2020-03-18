namespace ScreepsApiClient
{
    public interface IScreepsApiClient
    {
        IUserEndpoint User { get; }
    }
}