using MoneyMate_WebApp.DataAccess.Entities;
using MoneyMate_WebApp.DataAccess.Repositories;

namespace MoneyMate_WebApp.DataAccess.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Account> Accounts { get; }
        IRepository<Transaction> Transactions { get; }
        IRepository<Category> Categories { get; }
        IRepository<TypeEntity> Types { get; }
        IRepository<Currency> Currencies { get; }

        Task<int> SaveAsync();
    }
}
