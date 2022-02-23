using System.Threading;
using System.Threading.Tasks;

namespace nbp.core.repositories
{
    public interface IClearRepository<T>
    {
        Task<bool> Clear(CancellationToken ct);
    }
}