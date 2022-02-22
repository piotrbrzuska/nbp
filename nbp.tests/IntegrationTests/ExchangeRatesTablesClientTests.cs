using System;
using System.Linq;
using System.Threading.Tasks;
using nbp.api.client;
using nbp.api.client.queries;
using Xunit;

namespace nbp.tests.IntegrationTests
{
    public class ExchangeRatesTablesClientTests
    {
        public const string NBP_API_URL = "http://api.nbp.pl";


        [Fact]
        public async Task ShouldFetchRatesSeries()
        {
            var apiClient = new ExchangeRatesTablesClient(NBP_API_URL);
            var query = new ExchangeRatesSeriesQuery() { Table = "A" };
            var series = await apiClient.FetchRatesSeries(query);

            Assert.NotNull(series);
            Assert.NotEmpty(series.Rates);
        }


        [Fact]
        public async Task ShouldFetchTop10RatesSeries()
        {
            var apiClient = new ExchangeRatesTablesClient(NBP_API_URL);
            var query = new ExchangeRatesSeriesLastQuery() { Table = "A", Count = 10 };
            var series = await apiClient.FetchRatesSeriesList(query);

            Assert.NotNull(series);
            Assert.Equal(10, series.Count());
        }

        [Fact]
        public async Task ShouldFetchTodayRatesSeries()
        {
            var apiClient = new ExchangeRatesTablesClient(NBP_API_URL);
            var query = new ExchangeRatesSeriesTodayQuery() { Table = "A" };
            var series = await apiClient.FetchRatesSeries(query);

            Assert.NotNull(series);
            Assert.NotEmpty(series.Rates);
            Assert.Equal(DateTime.Today, series.EffectiveDate);
        }

        [Fact]
        public async Task ShouldFetchDateRatesSeries()
        {
            var apiClient = new ExchangeRatesTablesClient(NBP_API_URL);
            var query = new ExchangeRatesSeriesDateQuery() { Table = "A", Date = new DateTime(2022, 2, 18) };
            var series = await apiClient.FetchRatesSeries(query);

            Assert.NotNull(series);
            Assert.NotEmpty(series.Rates);
            Assert.Equal(query.Date, series.EffectiveDate);
        }

        [Fact]
        public async Task ShouldFetchDateRangeRatesSeries()
        {
            var apiClient = new ExchangeRatesTablesClient(NBP_API_URL);
            var query = new ExchangeRatesSeriesDateRangeQuery()
                { Table = "A", StartDate = new DateTime(2022, 2, 14), EndDate = new DateTime(2022, 2, 18) };
            var series = await apiClient.FetchRatesSeriesList(query);

            Assert.NotNull(series);
            Assert.Equal(5, series.Count());
            Assert.Equal(query.StartDate, series.First().EffectiveDate);
            Assert.Equal(query.EndDate, series.Last().EffectiveDate);
        }
    }
}