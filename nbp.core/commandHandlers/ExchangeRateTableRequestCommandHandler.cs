using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using nbp.core.commands;
using nbp.core.models;
using nbp.core.repositories;

namespace nbp.core.commandHandlers
{
    public class ExchangeRateTableRequestCommandHandler : IRequestHandler<ExchangeRateTableRequestCommand, ExchangeRateTable[]>
    {
        private readonly IExchangeRatesTablesWithRatesRepository _repository;
        private readonly IMediator _mediator;

        public ExchangeRateTableRequestCommandHandler(IExchangeRatesTablesWithRatesRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }
        public async Task<ExchangeRateTable[]> Handle(ExchangeRateTableRequestCommand request, CancellationToken cancellationToken)
        {
            var results =  await TryFetchExchangeRateTables(request, cancellationToken);
            if (!results.Any() && request.Date.HasValue && request.Date.Value <= DateTime.Now)
            {
                await _mediator.Send(new ExchangeRatesTablesImportCommand()
                    { StartDate = request.Date.Value, EndDate = request.Date.Value }, cancellationToken);
                results =  await TryFetchExchangeRateTables(request, cancellationToken);
            }
            return results;
        }

        private async Task<ExchangeRateTable[]> TryFetchExchangeRateTables(ExchangeRateTableRequestCommand request,
            CancellationToken cancellationToken)
        {
            if (request.Last.HasValue && request.Last.Value)
            {
                return (await _repository.GetLast(cancellationToken)).ToArray();
            }

            if (!string.IsNullOrWhiteSpace(request.Currency))
            {
                if (request.Date.HasValue)
                {
                    return (await _repository.Get(request.Currency, request.Date.Value, request.Date.Value, cancellationToken))
                        .ToArray();
                }

                if (request.StartDate.HasValue && request.EndDate.HasValue)
                {
                    return (await _repository.Get(request.Currency, request.StartDate.Value, request.EndDate.Value,
                        cancellationToken)).ToArray();
                }
            }
            else
            {
                if (request.Date.HasValue)
                {
                    return (await _repository.Get(request.Date.Value, request.Date.Value, cancellationToken)).ToArray();
                }

                if (request.StartDate.HasValue && request.EndDate.HasValue)
                {
                    return (await _repository.Get(request.StartDate.Value, request.EndDate.Value, cancellationToken)).ToArray();
                }
            }

            return (await _repository.Get(cancellationToken)).ToArray();
        }
    }
}