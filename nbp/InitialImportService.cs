using System;
using System.Threading.Tasks;
using MediatR;
using nbp.core.commands;

namespace nbp
{
    public class InitialImportService
    {
        private readonly IMediator _mediator;

        public InitialImportService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Import()
        {
            var currencyImportCommand = new CurrencyImportCommand();
            await _mediator.Send(currencyImportCommand);
            var exchangeRatesImportCommand = new ExchangeRatesTablesImportCommand()
            {
                StartDate = DateTime.Now.AddDays(-30),
                EndDate = DateTime.Now
            };
            await _mediator.Send(exchangeRatesImportCommand);
        }
    }
}