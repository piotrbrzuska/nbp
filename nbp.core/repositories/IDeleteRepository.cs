using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace nbp.core.repositories
{
    public interface IDeleteRepository<T>
    {
        Task<bool> Delete(int id, CancellationToken ct);
        Task<bool> Delete(IEnumerable<int> ids, CancellationToken ct);
    }
}