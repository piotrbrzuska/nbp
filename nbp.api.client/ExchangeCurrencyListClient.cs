using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nbp.api.client.models;
using RestSharp;

namespace nbp.api.client
{
    public class ExchangeCurrencyListClient : ExchangeClient
    {
        public ExchangeCurrencyListClient(string apiBaseUrl) : base(apiBaseUrl)
        {
        }
        public async Task<IEnumerable<CurrencyInfo>> FetchCurrenciesInfo()
        {
            const string url = "api/exchangerates/tables/a";
            var client = GetClient();
            var request = new RestRequest(url);
            var response = await client.ExecuteGetAsync<List<ExchangeRatesTable>>(request);
            var results = response.Data?.First();
            var currencies = results?.Rates.Select(x => new CurrencyInfo() { Code = x.Code, Currency = x.Currency });
            return currencies;
        }
    }
}