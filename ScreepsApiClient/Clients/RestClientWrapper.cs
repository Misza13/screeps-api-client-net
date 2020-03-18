namespace ScreepsApiClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using global::ScreepsApiClient.Model;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
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

        public async Task<T> GetAsync<T>(string path, string shard = null) where T : new()
        {
            var request = new RestRequest(this.baseUrl + path)
                .AddHeader("X-Token", this.token);

            if (shard != null)
            {
                request = request.AddParameter("shard", shard);
            }

            var response = await this.client.ExecuteAsync(request);
            var rawContent = (JObject) JsonConvert.DeserializeObject(response.Content);
            
            if (rawContent.ContainsKey("error"))
            {
                throw new Exception(rawContent["error"].Value<string>());
            }
            
            var content = JsonConvert.DeserializeObject<T>(response.Content); //TODO: Can we avoid doing this twice?

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