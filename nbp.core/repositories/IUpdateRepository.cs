using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace nbp.core.repositories
{
    public interface IUpdateRepository<T>
    {
        Task<bool> Update(T model, CancellationToken ct);
        Task<bool> Update(IEnumerable<T> models, CancellationToken ct);
    }
}