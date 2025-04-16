using MoneyMate_WebApp.BusinessLogic.Contracts;
using MoneyMate_WebApp.BusinessLogic.Dtos;
using MoneyMate_WebApp.DataAccess.UoW;

namespace MoneyMate_WebApp.BusinessLogic.Services
{
    public class CurrencyService(IUnitOfWork unitOfWork) : ICurrencyService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<CurrencyDto>> GetAllCurrenciesAsync(string languageCode = "en")
        {
            var currencies = await _unitOfWork.Currencies.GetAllAsync();

            var currencyTranslations = await _unitOfWork.CurrencyTranslations.GetAllAsync();

            return currencies.Select(c =>
            {
                var currencyTranslation = currencyTranslations
                    .FirstOrDefault(t => t.CurrencyId == c.Id && t.LanguageCode == languageCode);

                return new CurrencyDto(
                    c.Id,
                    currencyTranslation?.CurrencyName ?? "[Translation Missing]", 
                    c.CurrencyCode,
                    c.Symbol
                );
            });
        }
    }
}
