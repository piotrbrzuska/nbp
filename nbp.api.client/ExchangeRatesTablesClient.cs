using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nbp.api.client.models;
using nbp.api.client.queries;
using RestSharp;

namespace nbp.api.client
{
    public class ExchangeRatesTablesClient : ExchangeRatesClient<ExchangeRatesTable>
    {
        public ExchangeRatesTablesClient(string apiBaseUrl) : base(apiBaseUrl)
        {
        }
        
        public override async Task<ExchangeRatesTable> FetchRatesSeries(ExchangeRatesSeriesQuery query)
        {
            var client = GetClient();
            var request = new RestRequest(GetEndpointUrl(query));
            ExchangeRatesTable results = null;
            var response = await client.ExecuteGetAsync<List<ExchangeRatesTable>>(request);
            results = response.Data?.First();

            return results;
        }
        
        public override async Task<IEnumerable<ExchangeRatesTable>> FetchRatesSeriesList(ExchangeRatesSeriesQuery query)
        {
            var client = GetClient();
            var request = new RestRequest(GetEndpointUrl(query));
            var response = await client.ExecuteGetAsync<IEnumerable<ExchangeRatesTable>>(request);
            return response.Data;
        }
 
    }
}