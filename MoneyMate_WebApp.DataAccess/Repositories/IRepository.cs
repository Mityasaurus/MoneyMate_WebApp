using System.Collections.ObjectModel;

namespace MoneyMate_WebApp.DataAccess.Repositories
{
    public interface IRepository<T> where T : class
    {
        public Task<ReadOnlyCollection<T>> GetAllAsync();
        public Task<T?> GetAsync(Guid id);
        public Task<ReadOnlyCollection<T>> GetAsync(Func<T, bool> predicate);
        public Task CreateAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task DeleteAsync(Guid id);
    }
}
