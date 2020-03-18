namespace ScreepsApiClient
{
    using System.Threading.Tasks;
    using global::ScreepsApiClient.Model;

    public interface IUserEndpoint
    {
        Task<UserNameResponse> GetUsernameAsync();
    }
}