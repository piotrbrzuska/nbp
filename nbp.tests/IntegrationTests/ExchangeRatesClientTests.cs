using System;
using System.Threading.Tasks;
using nbp.api.client;
using nbp.api.client.queries;
using Xunit;

namespace nbp.tests.IntegrationTests
{
    public class ExchangeRatesClientTests
    {
        public const string NBP_API_URL = "http://api.nbp.pl";
        [Fact]
        public async Task ShouldFetchRatesSeries()
        {
            var apiClient = new ExchangeRatesClient(NBP_API_URL);
            var query = new ExchangeRatesSeriesQuery() { Table = "A" };
            var series = await apiClient.FetchRatesSeries(query);
            
            Assert.NotNull(series);
            Assert.NotEmpty(series.Rates);
        }
        
        
        [Fact]
        public async Task ShouldFetchTop10RatesSeries()
        {
            var apiClient = new ExchangeRatesClient(NBP_API_URL);
            var query = new ExchangeRatesSeriesLastQuery() { Table = "A", Count = 10};
            var series = await apiClient.FetchRatesSeriesList(query);
            
            Assert.NotNull(series);
            Assert.Equal(10, series.Count);
        }
        
        [Fact]
        public async Task ShouldFetchTodayRatesSeries()
        {
            var apiClient = new ExchangeRatesClient(NBP_API_URL);
            var query = new ExchangeRatesSeriesTodayQuery() { Table = "A"};
            var series = await apiClient.FetchRatesSeries(query);
            
            Assert.NotNull(series);
            Assert.NotEmpty(series.Rates);
            Assert.Equal(DateTime.Today, series.EffectiveDate);
        }
    }
}
