using MoneyMate_WebApp.BusinessLogic.Contracts;
using MoneyMate_WebApp.BusinessLogic.Dtos;
using MoneyMate_WebApp.DataAccess.Entities;
using MoneyMate_WebApp.DataAccess.UoW;

namespace MoneyMate_WebApp.BusinessLogic.Services
{
    public class AccountService(IUnitOfWork unitOfWork) : IAccountService
    {
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

            await unitOfWork.Accounts.CreateAsync(account);
            await unitOfWork.SaveAsync();
        }
    }
}
