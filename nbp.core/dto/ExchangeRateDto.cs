namespace nbp.core.dto
{
    public class ExchangeRateDto
    {
        public int Id { get; set; }
        public CurrencyInfoDto Currency { get; set; }
        public int CurrencyId { get; set; }
        public ExchangeRateTableDto Table { get; set; }
        public int TableId { get; set; }
        public double Mid { get; set; }
        public double Bid { get; set; }
        public double Ask { get; set; }
    }
}