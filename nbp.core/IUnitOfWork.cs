using System;

namespace nbp.core
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
    }
}