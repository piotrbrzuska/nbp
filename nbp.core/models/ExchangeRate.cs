namespace nbp.core.models
{
    public class ExchangeRate
    {
        public int Id { get; set; }
        public CurrencyInfo Currency { get; set; }
        public double Mid { get; set; }
        public double Bid { get; set; }
        public double Ask { get; set; }
    }
}