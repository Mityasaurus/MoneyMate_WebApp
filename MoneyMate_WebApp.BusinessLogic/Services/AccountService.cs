using MoneyMate_WebApp.BusinessLogic.Contracts;
using MoneyMate_WebApp.BusinessLogic.Dtos;
using MoneyMate_WebApp.DataAccess.Entities;
using MoneyMate_WebApp.DataAccess.UoW;

namespace MoneyMate_WebApp.BusinessLogic.Services
{
    public class AccountService(IUnitOfWork unitOfWork) : IAccountService
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task CreateAccountAsync(AccountDto accountDto)
        {
            ArgumentNullException.ThrowIfNull(accountDto);

            var account = new Account
            {
                Name = accountDto.Name,
                TypeId = accountDto.TypeId,
                CurrencyId = accountDto.CurrencyId,
                Balance = accountDto.Balance,
                CreatedAt = DateTime.Now
            };
            await _unitOfWork.Accounts.CreateAsync(account);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<AccountDto>> GetAllAccountsAsync()
        {
            var accounts = await _unitOfWork.Accounts.GetAllAsync();

            foreach (var account in accounts)
            {
                account.Type ??= await _unitOfWork.Types.GetAsync(account.TypeId);

                account.Currency ??= await _unitOfWork.Currencies.GetAsync(account.CurrencyId);
            }

            var dtos = accounts.Select(a => new AccountDto(
                a.Id,
                a.Name,
                a.TypeId,
                a.CurrencyId,
                a.Balance,
                new TypeDto(a.Id, a.Type?.Name ?? string.Empty),
                new CurrencyDto(a.Id,
                                a.Currency?.CurrencyName ?? string.Empty,
                                a.Currency?.CurrencyCode ?? string.Empty,
                                a.Currency?.Symbol ?? string.Empty)
            ));

            return dtos;
        }

    }
}
