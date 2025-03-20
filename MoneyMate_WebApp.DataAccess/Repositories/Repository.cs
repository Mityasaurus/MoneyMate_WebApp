using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace MoneyMate_WebApp.DataAccess.Repositories
{
    public class Repository<T>(MoneyMateContext context) : IRepository<T> where T : class
    {
        private readonly MoneyMateContext _context = context;
        private readonly DbSet<T> _dbSet = context.Set<T>();

        public async Task<ReadOnlyCollection<T>> GetAllAsync()
        {
            var list = await _dbSet.ToListAsync();
            return list.AsReadOnly();
        }


        public async Task<T?> GetAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<ReadOnlyCollection<T>> GetAsync(Func<T, bool> predicate)
        {
            return await Task.FromResult(_dbSet.Where(predicate).ToList().AsReadOnly());
        }

        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }
    }
}
