using System;
using nbp.api.client;
using nbp.api.client.queries;
using Xunit;

namespace nbp.tests
{
    public class ExchangeRatesClientGetEndpointUrlTests
    {
        [Fact]
        public void ShouldReturnTableUrl()
        {
            var query = new ExchangeRatesSeriesQuery() { Table = "A" };
            var url = ExchangeRatesClient.GetEndpointUrl(query);
            Assert.Equal("api/exchangerates/tables/a/",url);
        }
        [Fact]
        public void ShouldReturnLastTodayUrl()
        {
            var query = new ExchangeRatesSeriesLastQuery() { Table = "A", Count = 5};
            var url = ExchangeRatesClient.GetEndpointUrl(query);
            Assert.Equal("api/exchangerates/tables/a/last/5/",url);
        }
        [Fact]
        public void ShouldReturnTodayUrl()
        {
            var query = new ExchangeRatesSeriesTodayQuery() { Table = "A"};
            var url = ExchangeRatesClient.GetEndpointUrl(query);
            Assert.Equal("api/exchangerates/tables/a/today/",url);
        }
        
        [Fact]
        public void ShouldReturnDateUrl()
        {
            var query = new ExchangeRatesSeriesDateQuery() { Table = "A", Date = new DateTime(2022,2,20)};
            var url = ExchangeRatesClient.GetEndpointUrl(query);
            Assert.Equal("api/exchangerates/tables/a/2022-02-20/",url);
        }
        
        [Fact]
        public void ShouldReturnDateRangeUrl()
        {
            var query = new ExchangeRatesSeriesDateRangeQuery() { Table = "A", StartDate = new DateTime(2022,2,14), EndDate = new DateTime(2022,2,18)};
            var url = ExchangeRatesClient.GetEndpointUrl(query);
            Assert.Equal("api/exchangerates/tables/a/2022-02-14/2022-02-18/",url);
        }
        
        
        [Fact]
        public void ShouldReturnCurrencyRatesUrl()
        {
            var query = new ExchangeRatesSeriesQuery() { Table = "A", Code = "USD"};
            var url = ExchangeRatesClient.GetEndpointUrl(query);
            Assert.Equal("api/exchangerates/rates/a/usd/",url);
        }
        [Fact]
        public void ShouldReturnLastTodayCurrencyRatesUrl()
        {
            var query = new ExchangeRatesSeriesLastQuery() { Table = "A", Code = "USD", Count = 5};
            var url = ExchangeRatesClient.GetEndpointUrl(query);
            Assert.Equal("api/exchangerates/rates/a/usd/last/5/",url);
        }
        [Fact]
        public void ShouldReturnTodayCurrencyRatesUrl()
        {
            var query = new ExchangeRatesSeriesTodayQuery() { Table = "A", Code = "USD"};
            var url = ExchangeRatesClient.GetEndpointUrl(query);
            Assert.Equal("api/exchangerates/rates/a/usd/today/",url);
        }
        
        [Fact]
        public void ShouldReturnDateCurrencyRatesUrl()
        {
            var query = new ExchangeRatesSeriesDateQuery() { Table = "A", Code = "USD", Date = new DateTime(2022,2,20)};
            var url = ExchangeRatesClient.GetEndpointUrl(query);
            Assert.Equal("api/exchangerates/rates/a/usd/2022-02-20/",url);
        }
        
        [Fact]
        public void ShouldReturnDateRangeCurrencyRatesUrl()
        {
            var query = new ExchangeRatesSeriesDateRangeQuery() { Table = "A", Code = "USD", StartDate = new DateTime(2022,2,14), EndDate = new DateTime(2022,2,18)};
            var url = ExchangeRatesClient.GetEndpointUrl(query);
            Assert.Equal("api/exchangerates/rates/a/usd/2022-02-14/2022-02-18/",url);
        }
    }
}
