using System.Threading;
using System.Threading.Tasks;
using MediatR;
using nbp.core.commands;

namespace nbp.core.currenciesCommandHandlers
{
    public class CurrencyImportCommandHandler : IRequestHandler<CurrencyImportCommand, int> 
    {
        private readonly ApiToDatabaseCurrenciesBridge _currenciesBridge;
        private readonly IUnitOfWork _unitOfWork;

        public CurrencyImportCommandHandler(ApiToDatabaseCurrenciesBridge currenciesBridge, IUnitOfWork unitOfWork)
        {
            _currenciesBridge = currenciesBridge;
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(CurrencyImportCommand request, CancellationToken cancellationToken)
        {
            var results = await _currenciesBridge.Import(cancellationToken);
            if (results > 0)
            {
                _unitOfWork.Commit();
            }
            return results;
        }
    }
}