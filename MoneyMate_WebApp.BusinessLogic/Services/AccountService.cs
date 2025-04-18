using MoneyMate_WebApp.BusinessLogic.Contracts;
using MoneyMate_WebApp.BusinessLogic.Dtos;
using MoneyMate_WebApp.DataAccess.UoW;

namespace MoneyMate_WebApp.BusinessLogic.Services
{
    public class AccountService(IUnitOfWork unitOfWork) : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task CreateAccountAsync(AccountDto accountDto)
        {
            ArgumentNullException.ThrowIfNull(accountDto);

            var account = new DataAccess.Entities.Account
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

        public async Task<IEnumerable<AccountDto>> GetAllAccountsAsync(string languageCode = "en")
        {
            var accounts = await _unitOfWork.Accounts.GetAllAsync();

            foreach (var account in accounts)
            {
                account.Type ??= await _unitOfWork.Types.GetAsync(account.TypeId);

                account.Currency ??= await _unitOfWork.Currencies.GetAsync(account.CurrencyId);
            }

            var typeTranslations = await _unitOfWork.TypeTranslations.GetAllAsync();
            var currencyTranslations = await _unitOfWork.CurrencyTranslations.GetAllAsync();

            var dtos = accounts.Select(a =>
            {
                var typeTranslation = typeTranslations.FirstOrDefault(t => t.TypeId == a.TypeId && t.LanguageCode == languageCode);
                var currencyTranslation = currencyTranslations.FirstOrDefault(c => c.CurrencyId == a.CurrencyId && c.LanguageCode == languageCode);

                return new AccountDto(
                    a.Id,
                    a.Name,
                    a.TypeId,
                    a.CurrencyId,
                    a.Balance,
                    new TypeDto(a.TypeId, typeTranslation?.Name ?? "[Translation Missing]"),
                    new CurrencyDto(
                        a.CurrencyId,
                        currencyTranslation?.CurrencyName ?? "[Translation Missing]",
                        a.Currency.CurrencyCode,  
                        a.Currency.Symbol        
                    )
                );
            });

            return dtos;
        }
    }
}
