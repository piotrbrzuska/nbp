using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using nbp.api.client.models;
using nbp.api.client.queries;
using RestSharp;
using RestSharp.Serializers.Json;

namespace nbp.api.client
{
    public class ExchangeRatesClient
    {
        private readonly string _apiBaseUrl;

        public ExchangeRatesClient(string apiBaseUrl)
        {
            _apiBaseUrl = apiBaseUrl;
        }

        public async Task<ExchangeRatesSeries> FetchRatesSeries(ExchangeRatesSeriesQuery query)
        {
            var client = GetClient();
            var request = new RestRequest(GetEndpointUrl(query));
            var response = await client.ExecuteGetAsync<List<ExchangeRatesSeries>>(request);
            return response.Data?.First();
        }
        
        public async Task<List<ExchangeRatesSeries>> FetchRatesSeriesList(ExchangeRatesSeriesLastQuery query)
        {
            var client = GetClient();
            var request = new RestRequest(GetEndpointUrl(query));
            var response = await client.ExecuteGetAsync<List<ExchangeRatesSeries>>(request);
            return response.Data;
        }

        private RestClient GetClient()
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
        private static string GetEndpointUrl(ExchangeRatesSeriesQuery query)
        {
            string url = $"api/exchangerates/tables/{query.Table}/";
            if (query is ExchangeRatesSeriesLastQuery lastQuery)
            {
                url += $"last/{lastQuery.Count}/";
            }
            else if (query is ExchangeRatesSeriesTodayQuery todayQuery)
            {
                url += "today/";
            }
            else if (query is ExchangeRatesSeriesDateQuery dateQuery)
            {
                url += dateQuery.Date.ToString("yyyy-MM-dd");
            }
            else if (query is ExchangeRatesSeriesDateRangeQuery dateRangeQuery)
            {
                url += $"{dateRangeQuery.StartDate:yyyy-MM-dd}/{dateRangeQuery.EndDate:yyyy-MM-dd}";
            }

            return url;
        }
    }
}
