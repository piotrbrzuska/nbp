using System.Collections.Generic;

namespace nbp.core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public bool Commit()
        {
            if (!_context.ChangeTracker.HasChanges())
            {
                return true;
            }
            var rowsAffected = _context.SaveChanges();
            return rowsAffected > 0;
        }
    }
}