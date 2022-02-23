using System;

namespace nbp.core.commands
{
    public class ExchangeRatesTablesImportCommand : Command<int>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}