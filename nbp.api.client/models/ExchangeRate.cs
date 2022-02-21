using System;

namespace nbp.api.client.models
{
    public class ExchangeRate
    {
        public string Currency { get; set; }
        public string Code { get; set; }
        public string No { get; set; }
        public DateTime EffectiveDate { get; set; }
        public double Mid { get; set; }
        public double Bid { get; set; }
        public double Ask { get; set; }

    }
}