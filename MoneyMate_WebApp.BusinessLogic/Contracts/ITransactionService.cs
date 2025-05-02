using MoneyMate_WebApp.BusinessLogic.Dtos;

namespace MoneyMate_WebApp.BusinessLogic.Contracts
{
    public interface ITransactionService
    {
        Task CreateTransactionAsync(TransactionDto transactionDto);
        Task<IEnumerable<TransactionDto>> GetByAccountAsync(Guid accountId);
    }
}
