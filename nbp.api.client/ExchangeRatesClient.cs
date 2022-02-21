using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            ExchangeRatesSeries results = null;
            if (GetResponseFormat(query) == ResponseFormat.Array)
            {
                var response = await client.ExecuteGetAsync<List<ExchangeRatesSeries>>(request);
                results = response.Data?.First();
            }
            else
            {
                var response = await client.ExecuteGetAsync<ExchangeRatesSeries>(request);
                results = response.Data;
            }

            return results;
        }
        
        public async Task<List<ExchangeRatesSeries>> FetchRatesSeriesList(ExchangeRatesSeriesQuery query)
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
        public static string GetEndpointUrl(ExchangeRatesSeriesQuery query)
        {
            var sb = new StringBuilder("api/exchangerates/");
            if (string.IsNullOrEmpty(query.Code))
            {
                sb.Append("tables/");
                sb.Append(query.Table);
                sb.Append('/');
            }
            else
            {
                sb.Append("rates/");
                sb.Append(query.Table);
                sb.Append('/');
                sb.Append(query.Code);
                sb.Append('/');
            }
            if (query is ExchangeRatesSeriesLastQuery lastQuery)
            {
                sb.Append("last/");
                sb.Append(lastQuery.Count);
                sb.Append('/');
            }
            else if (query is ExchangeRatesSeriesTodayQuery todayQuery)
            {
                sb.Append("today/");
            }
            else if (query is ExchangeRatesSeriesDateQuery dateQuery)
            {
                sb.Append(dateQuery.Date.ToString("yyyy-MM-dd"));
                sb.Append('/');
            }
            else if (query is ExchangeRatesSeriesDateRangeQuery dateRangeQuery)
            {
                sb.Append(dateRangeQuery.StartDate.ToString("yyyy-MM-dd"));
                sb.Append('/');
                sb.Append(dateRangeQuery.EndDate.ToString("yyyy-MM-dd"));
                sb.Append('/');
            }
            return sb.ToString().ToLower();
        }

        public static ResponseFormat GetResponseFormat(ExchangeRatesSeriesQuery query)
        {
            return !string.IsNullOrWhiteSpace(query.Code) ? ResponseFormat.Single : ResponseFormat.Array;
        }
    }

    public enum ResponseFormat
    {
        Array,
        Single
    }
}
