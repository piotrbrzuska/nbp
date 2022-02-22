using System;

namespace nbp.api.client.models
{
    public class ExchangeRates
    {
        
        public string Table { get; set; }
        
        public string Currency { get; set; }
        public string Code { get; set; }

        public ExchangeRatesValues Rates { get; set; }
    }
}