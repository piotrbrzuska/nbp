using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using nbp.api.client;
using nbp.core.dto;
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
            var models = _mapper.Map<CurrencyInfo[]>(apiModels);
            if (await _repository.Register(models, ct))
            {
                return models.Length;
            };
            return 0;
        }
    }
}