using System.Linq;
using System.Threading.Tasks;
using nbp.api.client;
using Xunit;

namespace nbp.tests.IntegrationTests
{
    public class CurrencyListClientTests
    {
        public const string NBP_API_URL = "http://api.nbp.pl";
        [Fact]
        public async Task ShouldFetchCurrencyList()
        {
            var apiClient = new ExchangeCurrencyListClient(NBP_API_URL);
            var currencies = await apiClient.FetchCurrenciesInfo();

            Assert.NotNull(currencies);
            Assert.NotEmpty(currencies);
            Assert.Equal(35, currencies.Count());
        }
    }
}