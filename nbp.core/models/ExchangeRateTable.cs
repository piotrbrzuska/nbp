using System;
using System.Collections.Generic;

namespace nbp.core.models
{
    public class ExchangeRateTable
    {
        public int Id { get; set; }
        public string Table { get; set; }
        public string No { get; set; }
        public DateTime EffectiveDate { get; set; }
        
        public IEnumerable<ExchangeRate> Rates { get; set; }
    }
}