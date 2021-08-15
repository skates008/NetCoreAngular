using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NetCoreAngular.Data;
using Microsoft.EntityFrameworkCore;

namespace NetCoreAngular.Service
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(NetCoreAngularDbContext dataContext)
        {
            DataContext = dataContext;
            _dbSet = DataContext.Set<TEntity>();
        }

        public NetCoreAngularDbContext DataContext { get; }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression = null)
        {
            if (expression == null)
                return _dbSet;

            return _dbSet.Where(expression);
        }

        public void Add(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentException("entity");
            _dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentException("entity");
            if (DataContext.Entry(entity).State == EntityState.Detached) _dbSet.Attach(entity);
            DataContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            if (DataContext.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);
            _dbSet.Remove(entity);
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<TEntity> FirstOrDefaultAsync()
        {
            return await _dbSet.FirstOrDefaultAsync();
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            if (expression == null)
                 return await _dbSet.FirstOrDefaultAsync();
             return await _dbSet.FirstOrDefaultAsync(expression);
        }

        public TEntity FirstOrDefault()
        {
            return  _dbSet.FirstOrDefault();
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression = null)
        {
            if (expression == null)
                return  _dbSet.FirstOrDefault();
            return  _dbSet.FirstOrDefault(expression);
        }

        public async Task<bool> Exists(Expression<Func<TEntity, bool>> expression = null)
        {
            if (expression == null)
                return await _dbSet.AnyAsync();
            return await _dbSet.AnyAsync(expression);
        }

        public IQueryable<TEntity> GetAllIgnoreGlobalQueries(Expression<Func<TEntity, bool>> expression = null)
        {
            if (expression == null)
                return _dbSet;

            return _dbSet.IgnoreQueryFilters().Where(expression);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

       
    }
}