using System;

namespace nbp.api.client.queries
{
    public class ExchangeRatesSeriesDateQuery : ExchangeRatesSeriesQuery
    {
        public DateTime Date { get; set; }
    }
}