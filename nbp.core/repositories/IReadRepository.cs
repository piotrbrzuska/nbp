using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace nbp.core.repositories
{
    public interface IReadRepository<T>
    {
        Task<T> Get(int id, CancellationToken ct);
        Task<IEnumerable<T>> Get(CancellationToken ct);
    }
}