using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using nbp.core.models;

namespace nbp.core.repositories
{
    public interface IExchangeRatesTablesRepository : IReadRepository<ExchangeRateTable>, IRegisterRepository<ExchangeRateTable>, IClearRepository<ExchangeRateTable>
    {
        Task<IEnumerable<ExchangeRateTable>> Get(DateTime startDate, DateTime endDate, CancellationToken ct);
    }

}