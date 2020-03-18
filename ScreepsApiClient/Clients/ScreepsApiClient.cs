namespace ScreepsApiClient
{
    public class ScreepsApiClient : IScreepsApiClient
    {
        private RestClientWrapper client;

        public IUserEndpoint User { get; }

        public ScreepsApiClient(string baseUrl, string token)
        {
            this.client = new RestClientWrapper(baseUrl, token);

            this.User = new UserEndpoint(this.client);
        }
    }
}