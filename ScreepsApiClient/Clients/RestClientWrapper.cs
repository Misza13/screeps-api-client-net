namespace ScreepsApiClient
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using global::ScreepsApiClient.Model;
    using RestSharp;

    internal class RestClientWrapper
    {
        private readonly string baseUrl;
        private readonly string token;
        
        private readonly RestClient client;

        public RestClientWrapper(string baseUrl, string token)
        {
            this.baseUrl = baseUrl;
            this.token = token;
            
            this.client = new RestClient();
        }

        public async Task<T> GetAsync<T>(string path) where T : new()
        {
            var request = new RestRequest(this.baseUrl + path);
            request.AddHeader("X-Token", this.token);

            var response = await this.client.ExecuteAsync(request);
            var content = this.client.Deserialize<T>(response).Data;

            if (content is RateLimitResponse rateLimitResponse)
            {
                rateLimitResponse.RateLimit = response.Headers.Get("X-RateLimit-Limit").ToInt();
                rateLimitResponse.RateLimitRemaining = response.Headers.Get("X-RateLimit-Remaining").ToInt();
                rateLimitResponse.RateLimitReset = response.Headers.Get("X-RateLimit-Reset").ToInt().ToDateTime();
            }

            return content;
        }

        private static string GetHeaderValue(IList<Parameter> headers, string headerName)
        {
            var header = headers.First(hdr => hdr.Name == headerName);
            return (string)header.Value;
        }
    }
}