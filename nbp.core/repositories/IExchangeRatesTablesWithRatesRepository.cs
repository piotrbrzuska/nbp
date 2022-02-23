using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using nbp.core.models;

namespace nbp.core.repositories
{
    public interface IExchangeRatesTablesWithRatesRepository : IReadRepository<ExchangeRateTable>, IRegisterRepository<ExchangeRateTable>, IClearRepository<ExchangeRateTable>
    {
        Task<IEnumerable<ExchangeRateTable>> Get(DateTime startDate, DateTime endDate, CancellationToken ct);
        Task<IEnumerable<ExchangeRateTable>> Get(string currencyCode, DateTime startDate, DateTime endDate, CancellationToken ct);
        Task<IEnumerable<ExchangeRateTable>> Get(string currencyCode, DateTime date, CancellationToken ct);
        Task<IEnumerable<ExchangeRateTable>> GetLast(CancellationToken ct);
        Task<IEnumerable<ExchangeRateTable>> GetLast(string currencyCode, CancellationToken ct);
    }
}