using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace nbp.core.repositories
{
    public interface IRegisterRepository<T>
    {
        Task<bool> Register(T model, CancellationToken ct);
        Task<bool> Register(IEnumerable<T> models, CancellationToken ct);
    }
}