namespace ScreepsApiClient
{
    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Threading.Tasks;
    using global::ScreepsApiClient.Model;
    using Newtonsoft.Json;

    internal class UserEndpoint : IUserEndpoint
    {
        private readonly RestClientWrapper client;

        public UserEndpoint(RestClientWrapper client)
        {
            this.client = client;
        }
        
        public async Task<GetUserNameResponse> GetNameAsync()
        {
            return await this.client.GetAsync<GetUserNameResponse>("user/name");
        }

        public async Task<GetUserMemoryResponse<TMem>> GetMemoryAsync<TMem>(string shard)
        {
            var response = await this.client.GetAsync<GetUserRawMemoryResponse>("user/memory", shard);

            if (!response.Data.StartsWith("gz:"))
            {
                throw new Exception("Memory response not gzipped");
            }

            var memoryRaw = Decompress(response.Data.Substring(3).DecodeBase64());
            
            return new GetUserMemoryResponse<TMem>
            {
                Memory = JsonConvert.DeserializeObject<TMem>(memoryRaw)
            };
        }

        private static string Decompress(byte[] input)
        {
            using (var source = new MemoryStream(input))
            {
                using (var gzipped = new GZipStream(source, CompressionMode.Decompress))
                {
                    using (var reader = new StreamReader(gzipped))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }
        
        internal class GetUserRawMemoryResponse
        {
            public string Data { get; set; }
        }
    }
}