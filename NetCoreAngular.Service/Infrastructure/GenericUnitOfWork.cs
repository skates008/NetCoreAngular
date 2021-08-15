using NetCoreAngular.Data;
using System;
using System.Threading.Tasks;

namespace NetCoreAngular.Service
{
    public class GenericUnitOfWork : IGenericUnitOfWork
    {
        private NetCoreAngularDbContext _context;

        public GenericUnitOfWork(NetCoreAngularDbContext context)
        {
            _context = context;
        }

        public GenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class
        {
            return new GenericRepository<TEntity, TKey>(_context);
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                throw new Exception("Unable to Save Please try again later.");
            }
        }

        public void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
            }

            catch (Exception ex)
            {
                throw new Exception("Unable to Save Please try again later.");
            }
        }

        /// <summary>
        ///     Disposes the current object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Disposes all external resources.
        /// </summary>
        /// <param name="disposing">The dispose indicator.</param>
        private void Dispose(bool disposing)
        {
            if (disposing)
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
        }
    }
}