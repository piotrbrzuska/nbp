using System;
using System.Collections.Generic;

namespace nbp.core.dto
{
    public class ExchangeRateTableDto
    {
        public int Id { get; set; }
        public string Table { get; set; }
        public string No { get; set; }
        public DateTime EffectiveDate { get; set; }
        
        public ICollection<ExchangeRateDto> Rates { get; set; }
    }
}