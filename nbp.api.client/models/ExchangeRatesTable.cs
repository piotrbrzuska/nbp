using System;

namespace nbp.api.client.models
{
    public class ExchangeRatesTable
    {
        
        public string Table { get; set; }
        public string No { get; set; }
        public DateTime EffectiveDate { get; set; }

        public ExchangeRatesTableValues Rates { get; set; }
    }
}