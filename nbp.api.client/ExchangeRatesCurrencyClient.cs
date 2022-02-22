using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nbp.api.client.models;
using nbp.api.client.queries;
using RestSharp;

namespace nbp.api.client
{
    public class ExchangeRatesCurrencyClient : ExchangeRatesClient<ExchangeRates>
    {
        public ExchangeRatesCurrencyClient(string apiBaseUrl) : base(apiBaseUrl)
        {
        }
        
        public override async Task<ExchangeRates> FetchRatesSeries(ExchangeRatesSeriesQuery query)
        {
            var client = GetClient();
            var request = new RestRequest(GetEndpointUrl(query));
            ExchangeRates results = null;
            var response = await client.ExecuteGetAsync<ExchangeRates>(request);
            results = response.Data;
            return results;
        }
        
        public override async Task<IEnumerable<ExchangeRates>> FetchRatesSeriesList(ExchangeRatesSeriesQuery query)
        {
            var client = GetClient();
            var request = new RestRequest(GetEndpointUrl(query));
            var response = await client.ExecuteGetAsync<IEnumerable<ExchangeRates>>(request);
            return response.Data;
        }
 
    }
}