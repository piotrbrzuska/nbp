using System;
using System.Linq;
using System.Threading.Tasks;
using nbp.api.client;
using nbp.api.client.queries;
using Xunit;

namespace nbp.tests.IntegrationTests
{
    public class ExchangeRatesCurrencyClientTests
    {
        public const string NBP_API_URL = "http://api.nbp.pl";


        [Fact]
        public async Task ShouldFetchCurrencyRatesSeries()
        {
            var apiClient = new ExchangeRatesCurrencyClient(NBP_API_URL);
            var query = new ExchangeRatesSeriesQuery() { Table = "A", Code = "USD", };
            var series = await apiClient.FetchRatesSeries(query);

            Assert.NotNull(series);
            Assert.NotEmpty(series.Rates);
            Assert.Equal(query.Code, series.Code);
        }


        [Fact]
        public async Task ShouldFetchTop10CurrencyRatesSeries()
        {
            var apiClient = new ExchangeRatesCurrencyClient(NBP_API_URL);
            var query = new ExchangeRatesSeriesLastQuery() { Table = "A", Code = "USD", Count = 10 };
            var series = await apiClient.FetchRatesSeries(query);

            Assert.NotNull(series);
            Assert.Equal(10, series.Rates.Count);
        }

        [Fact]
        public async Task ShouldFetchTodayCurrencyRatesSeries()
        {
            var apiClient = new ExchangeRatesCurrencyClient(NBP_API_URL);
            var query = new ExchangeRatesSeriesTodayQuery() { Table = "A", Code = "USD" };
            var series = await apiClient.FetchRatesSeries(query);

            Assert.NotNull(series);
            Assert.NotEmpty(series.Rates);
            Assert.Equal(query.Code, series.Code);
            Assert.Equal(DateTime.Today, series.Rates.First().EffectiveDate);
        }

        [Fact]
        public async Task ShouldFetchDateCurrencyRatesSeries()
        {
            var apiClient = new ExchangeRatesCurrencyClient(NBP_API_URL);
            var query = new ExchangeRatesSeriesDateQuery()
                { Table = "A", Code = "USD", Date = new DateTime(2022, 2, 18) };
            var series = await apiClient.FetchRatesSeries(query);

            Assert.NotNull(series);
            Assert.NotEmpty(series.Rates);
            Assert.Equal(query.Code, series.Code);
            Assert.Equal(query.Date, series.Rates.First().EffectiveDate);
        }

        [Fact]
        public async Task ShouldFetchDateRangeCurrencyRatesSeries()
        {
            var apiClient = new ExchangeRatesCurrencyClient(NBP_API_URL);
            var query = new ExchangeRatesSeriesDateRangeQuery()
            {
                Table = "A", Code = "USD", StartDate = new DateTime(2022, 2, 14), EndDate = new DateTime(2022, 2, 18)
            };
            var series = await apiClient.FetchRatesSeries(query);

            Assert.NotNull(series);
            Assert.Equal(5, series.Rates.Count);
            Assert.Equal(query.Code, series.Code);
            Assert.Equal(query.StartDate, series.Rates.First().EffectiveDate);
            Assert.Equal(query.EndDate, series.Rates.Last().EffectiveDate);
        }
    }
}