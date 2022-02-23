using System.Threading;
using System.Threading.Tasks;
using nbp.core.dto;

namespace nbp.core.repositories
{
    public interface ICurrenciesRepository : IReadRepository<CurrencyInfo>, IRegisterRepository<CurrencyInfo>, IClearRepository<CurrencyInfo>
    {
        Task<CurrencyInfo> GetByCode(string code, CancellationToken ct);
    }
}