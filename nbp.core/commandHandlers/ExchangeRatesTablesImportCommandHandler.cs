using System.Threading;
using System.Threading.Tasks;
using MediatR;
using nbp.core.commands;

namespace nbp.core.commandHandlers
{
    public class ExchangeRatesTablesImportCommandHandler : IRequestHandler<ExchangeRatesTablesImportCommand, int> 
    {
        private readonly ApiToDatabaseExchangeRatesTablesBridge _bridge;
        private readonly IUnitOfWork _unitOfWork;

        public ExchangeRatesTablesImportCommandHandler(ApiToDatabaseExchangeRatesTablesBridge bridge, IUnitOfWork unitOfWork)
        {
            _bridge = bridge;
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(ExchangeRatesTablesImportCommand request, CancellationToken cancellationToken)
        {
            var results = await _bridge.Import(request.StartDate, request.EndDate, cancellationToken);
            if (results > 0)
            {
                _unitOfWork.Commit();
            }
            return results;
        }
    }
}