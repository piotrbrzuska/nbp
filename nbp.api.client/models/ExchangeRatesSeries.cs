using System;

namespace nbp.api.client.models
{
    public class ExchangeRatesSeries
    {
        
        public string Table { get; set; }
        
        public string Currency { get; set; }
        public string Code { get; set; }
        public string No { get; set; }
        public DateTime EffectiveDate { get; set; }

        public ExchangeRates Rates { get; set; }
    }
}