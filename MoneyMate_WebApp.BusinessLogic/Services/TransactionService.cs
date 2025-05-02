using MoneyMate_WebApp.BusinessLogic.Contracts;
using MoneyMate_WebApp.BusinessLogic.Dtos;
using MoneyMate_WebApp.DataAccess.Entities;
using MoneyMate_WebApp.DataAccess.UoW;
using System.Globalization;

namespace MoneyMate_WebApp.BusinessLogic.Services
{
    public class TransactionService(IUnitOfWork unitOfWork) : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task CreateTransactionAsync(TransactionDto transactionDto)
        {
            ArgumentNullException.ThrowIfNull(transactionDto);

            var account = await _unitOfWork.Accounts.GetAsync(transactionDto.AccountId) ?? throw new Exception("Рахунок не знайдено");

            var category = await _unitOfWork.Categories.GetAsync(transactionDto.CategoryId) ?? throw new Exception("Категорію не знайдено");
            
            if (category.Type == TransactionType.Expense)
            {
                if (account.Balance < transactionDto.Amount)
                    throw new Exception("Недостатньо коштів для проведення цієї операції");
                account.Balance -= transactionDto.Amount;
            }
            else if (category.Type == TransactionType.Income)
            {
                account.Balance += transactionDto.Amount;
            }
            else
            {
                throw new Exception("Invalid transaction type.");
            }

            var transaction = new Transaction
            {
                AccountId = transactionDto.AccountId,
                CategoryId = transactionDto.CategoryId,
                Date = transactionDto.Date,
                Amount = transactionDto.Amount,
                Comment = transactionDto.Comment,
                CreatedAt = DateTime.Now
            };

            await _unitOfWork.Accounts.UpdateAsync(account);
            await _unitOfWork.Transactions.CreateAsync(transaction);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<TransactionDto>> GetByAccountAsync(Guid accountId)
        {
            var transactions = await _unitOfWork.Transactions.GetAsync(t => t.AccountId == accountId);
            if (!transactions.Any()) return Array.Empty<TransactionDto>();

            var accountIds = transactions.Select(t => t.AccountId).Distinct().ToList();
            var categoryIds = transactions.Select(t => t.CategoryId).Distinct().ToList();

            var accounts = await _unitOfWork.Accounts.GetAsync(a => accountIds.Contains(a.Id));
            var categories = await _unitOfWork.Categories.GetAsync(c => categoryIds.Contains(c.Id));

            var translations = await _unitOfWork.CategoryTranslations
                .GetAsync(tr => categoryIds.Contains(tr.CategoryId));

            var cultureCode = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            var categoryDtos = categories.ToDictionary(
                c => c.Id,
                c =>
                {
                    var group = translations
                        .Where(tr => tr.CategoryId == c.Id)
                        .ToList();

                    var trForCulture = group.FirstOrDefault(tr =>
                        tr.LanguageCode.Equals(cultureCode, StringComparison.OrdinalIgnoreCase));

                    var chosen = trForCulture ?? group.FirstOrDefault();

                    var name = chosen?.Name ?? "—";

                    return new CategoryDto(c.Id, name, c.Type);
                });

            var accountDtos = accounts.ToDictionary(
                a => a.Id,
                a => new AccountDto(a.Id, a.Name, a.TypeId, a.CurrencyId, a.Balance)
            );

            var result = transactions.Select(t => new TransactionDto(
                t.Id,
                t.AccountId,
                t.CategoryId,
                t.Date,
                t.Amount,
                t.Comment,
                accountDtos[t.AccountId],
                categoryDtos[t.CategoryId]
            ));

            return result.OrderByDescending(r => r.Date);
        }


    }
}
