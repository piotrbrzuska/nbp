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

        public ExchangeRateTableRequestCommandHandler(IExchangeRatesTablesWithRatesRepository repository)
        {
            _repository = repository;
        }
        public async Task<ExchangeRateTable[]> Handle(ExchangeRateTableRequestCommand request, CancellationToken cancellationToken)
        {
            if (request.Last.HasValue && request.Last.Value)
            {
                return (await _repository.GetLast(cancellationToken)).ToArray();
            }
            if (!string.IsNullOrWhiteSpace(request.Currency))
            {
                if (request.Date.HasValue)
                {
                    return (await _repository.Get(request.Currency, request.Date.Value, request.Date.Value, cancellationToken)).ToArray();
                }
                if (request.StartDate.HasValue && request.EndDate.HasValue)
                {
                    return (await _repository.Get(request.Currency, request.StartDate.Value, request.EndDate.Value, cancellationToken)).ToArray();
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