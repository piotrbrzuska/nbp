using System;

namespace nbp.api.client.queries
{
    public class ExchangeRatesSeriesDateRangeQuery : ExchangeRatesSeriesQuery
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}