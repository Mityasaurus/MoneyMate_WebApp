using MoneyMate_WebApp.BusinessLogic.Contracts;
using MoneyMate_WebApp.BusinessLogic.Dtos;
using MoneyMate_WebApp.DataAccess.Entities;
using MoneyMate_WebApp.DataAccess.UoW;

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
    }
}
