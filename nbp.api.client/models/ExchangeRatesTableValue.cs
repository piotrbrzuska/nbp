namespace nbp.api.client.models
{
    public class ExchangeRatesTableValue
    {
        public string Currency { get; set; }
        public string Code { get; set; }
        public double Mid { get; set; }
        public double Bid { get; set; }
        public double Ask { get; set; }
    }
}