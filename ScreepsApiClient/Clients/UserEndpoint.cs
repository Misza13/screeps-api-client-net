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
        
        public async Task<UserNameResponse> GetUsernameAsync()
        {
            var response = await this.client.GetAsync<UserNameResponse>("user/name");
            return response;
        }
    }
}