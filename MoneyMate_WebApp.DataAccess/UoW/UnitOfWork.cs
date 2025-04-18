using MoneyMate_WebApp.DataAccess.Entities;
using MoneyMate_WebApp.DataAccess.Repositories;

namespace MoneyMate_WebApp.DataAccess.UoW
{
    public class UnitOfWork(
        MoneyMateContext context,
        IRepository<Account> accountRepository,
        IRepository<Transaction> transactionRepository,
        IRepository<Category> categoryRepository,
        IRepository<TypeEntity> typeRepository,
        IRepository<Currency> currencyRepository,
        IRepository<CategoryTranslation> categoryTranslationRepository,
        IRepository<TypeTranslation> typeTranslationRepository,
        IRepository<CurrencyTranslation> currencyTranslationRepository) : IUnitOfWork
    {
        private readonly MoneyMateContext _context = context;

        public IRepository<Account> Accounts { get; } = accountRepository;
        public IRepository<Transaction> Transactions { get; } = transactionRepository;
        public IRepository<Category> Categories { get; } = categoryRepository;
        public IRepository<TypeEntity> Types { get; } = typeRepository;
        public IRepository<Currency> Currencies { get; } = currencyRepository;

        public IRepository<CategoryTranslation> CategoryTranslations { get; } = categoryTranslationRepository;
        public IRepository<TypeTranslation> TypeTranslations { get; } = typeTranslationRepository;
        public IRepository<CurrencyTranslation> CurrencyTranslations { get; } = currencyTranslationRepository;

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
