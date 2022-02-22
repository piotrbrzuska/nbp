using System.Text.Json;
using RestSharp;
using RestSharp.Serializers.Json;

namespace nbp.api.client
{
    public abstract class ExchangeClient
    {
        private readonly string _apiBaseUrl;

        public ExchangeClient(string apiBaseUrl)
        {
            _apiBaseUrl = apiBaseUrl;
        }
        protected RestClient GetClient()
        {
            var client = new RestClient(_apiBaseUrl);
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            client.UseSystemTextJson(serializeOptions);
            return client;
        }

    }
}