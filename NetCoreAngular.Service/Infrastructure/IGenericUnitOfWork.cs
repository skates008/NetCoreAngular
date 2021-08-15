using System;
using System.Threading.Tasks;

namespace NetCoreAngular.Service
{
    public interface IGenericUnitOfWork : IDisposable
    {
        GenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class;
        Task SaveChangesAsync();
        void SaveChanges();
    }
}