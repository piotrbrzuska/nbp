using System;
using nbp.core.models;

namespace nbp.core.commands
{
    public class ExchangeRateTableRequestCommand : Command<ExchangeRateTable[]>
    {
        public DateTime? Date { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Currency { get; set; }
    }
}