using System.Threading;
using System.Threading.Tasks;
using MediatR;
using nbp.core.commands;

namespace nbp.cli
{
    public class CurrenciesCli
    {
        private readonly IMediator  _mediator;

        public CurrenciesCli(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task DispatchAction(string[] args)
        {
            switch (args[0].ToLower())
            {
                case "import":
                    var importCommand = new CurrencyImportCommand();
                    var response = await _mediator.Send(importCommand, new CancellationToken());
                    break;
            }
        }
    }
}