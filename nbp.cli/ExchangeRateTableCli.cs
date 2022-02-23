using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using nbp.core.commands;

namespace nbp.cli
{
    public class ExchangeRateTableCli
    {
        private readonly IMediator  _mediator;

        public ExchangeRateTableCli(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task DispatchAction(string[] args)
        {
            switch (args[0].ToLower())
            {
                case "import":
                    var importCommand = new ExchangeRatesTablesImportCommand()
                    {
                        StartDate = DateTime.Now.AddDays(-30),
                        EndDate = DateTime.Now
                    };
                    var response = await _mediator.Send(importCommand, new CancellationToken());
                    break;
            }
        }
    }
}