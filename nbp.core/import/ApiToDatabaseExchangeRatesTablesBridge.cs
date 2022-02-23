using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using nbp.api.client;
using nbp.api.client.queries;
using nbp.core.models;
using nbp.core.repositories;

namespace nbp.core
{
    public class ApiToDatabaseExchangeRatesTablesBridge
    {
        private readonly ExchangeRatesTablesClient _apiClient;
        private readonly IExchangeRatesTablesRepository _repository;
        private readonly ICurrenciesRepository _currenciesRepository;
        private readonly IMapper _mapper;

        public ApiToDatabaseExchangeRatesTablesBridge(ExchangeRatesTablesClient apiClient, 
            IExchangeRatesTablesRepository repository, 
            ICurrenciesRepository currenciesRepository,
            IMapper mapper)
        {
            _apiClient = apiClient;
            _repository = repository;
            _currenciesRepository = currenciesRepository;
            _mapper = mapper;
        }

        public async Task<int> Import(DateTime startDate, DateTime endDate, CancellationToken ct = new CancellationToken())
        {
            var currentModels = await _repository.Get(startDate, endDate, ct);
            var currentModelsEffectiveDate = currentModels.Select(x => x.EffectiveDate).ToArray();
            var query = new ExchangeRatesSeriesDateRangeQuery() { Table = "A", StartDate = startDate, EndDate = endDate };
            var apiModels = (await _apiClient.FetchRatesSeriesList(query))
                .Where(x => !currentModelsEffectiveDate.Contains(x.EffectiveDate))
                .ToArray();
            if (apiModels.Length == 0)
            {
                return 0;
            }
            
            var currencies = (await _currenciesRepository.Get(ct)).ToDictionary( x=> x.Code, x => x);
            var models = _mapper.Map<ExchangeRateTable[]>(apiModels);
            foreach (var model in models)
            {
                var rates = model.Rates.ToArray();
                foreach (var rate in rates)
                {
                    if (currencies.TryGetValue(rate.Currency.Code, out var currency))
                    {
                        rate.Currency = currency;
                    }
                }

                model.Rates = rates;
            }
            var modelsToImport = models;
            if (modelsToImport.Length > 0 && await _repository.Register(modelsToImport, ct))
            {
                return modelsToImport.Length;
            };
            return 0;
        }
    }
}