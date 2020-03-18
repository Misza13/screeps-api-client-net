namespace ScreepsApiClient
{
    using System.Threading.Tasks;
    using global::ScreepsApiClient.Model;

    public interface IUserEndpoint
    {
        Task<GetUserNameResponse> GetNameAsync();

        Task<GetUserMemoryResponse<TMem>> GetMemoryAsync<TMem>(string shard);
    }
}