namespace ScreepsApiClient
{
    using System.Threading.Tasks;
    using global::ScreepsApiClient.Model;

    internal class UserEndpoint : IUserEndpoint
    {
        private readonly RestClientWrapper client;

        public UserEndpoint(RestClientWrapper client)
        {
            this.client = client;
        }
        
        public async Task<GetUserNameResponse> GetNameAsync()
        {
            var response = await this.client.GetAsync<GetUserNameResponse>("user/name");
            return response;
        }
    }
}