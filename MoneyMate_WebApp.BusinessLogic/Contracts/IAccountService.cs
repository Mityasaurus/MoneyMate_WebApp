using MoneyMate_WebApp.BusinessLogic.Dtos;

namespace MoneyMate_WebApp.BusinessLogic.Contracts
{
    public interface IAccountService
    {
        Task CreateAccountAsync(AccountDto accountDto);
        Task<IEnumerable<AccountDto>> GetAllAccountsAsync(string languageCode = "en");
    }
}
