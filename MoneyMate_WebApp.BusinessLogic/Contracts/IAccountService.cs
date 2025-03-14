using MoneyMate_WebApp.BusinessLogic.Dtos;

namespace MoneyMate_WebApp.BusinessLogic.Contracts
{
    public interface IAccountService
    {
        Task CreateAccountAsync(AccountDto accountDto);
    }
}
