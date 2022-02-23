using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using nbp.api.client;
using nbp.core.models;
using nbp.core.repositories;

namespace nbp.core
{
    public class ApiToDatabaseCurrenciesBridge
    {
        private readonly ExchangeCurrencyListClient _apiClient;
        private readonly ICurrenciesRepository _repository;
        private readonly IMapper _mapper;

        public ApiToDatabaseCurrenciesBridge(ExchangeCurrencyListClient apiClient, ICurrenciesRepository repository, IMapper mapper)
        {
            _apiClient = apiClient;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Import(CancellationToken ct = new CancellationToken())
        {
            var apiModels = await _apiClient.FetchCurrenciesInfo();
            var currentModels = (await _repository.Get(ct)).ToArray();
            var models = _mapper.Map<CurrencyInfo[]>(apiModels);
            var modelsToImport = models.Except(currentModels, new CurrencyInfoEqualityComparerByCode()).ToArray();
            if (modelsToImport.Length > 0 && await _repository.Register(modelsToImport, ct))
            {
                return modelsToImport.Length;
            };
            return 0;
        }
    }
}