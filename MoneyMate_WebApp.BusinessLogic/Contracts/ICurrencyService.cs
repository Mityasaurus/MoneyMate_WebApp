using MoneyMate_WebApp.BusinessLogic.Dtos;

namespace MoneyMate_WebApp.BusinessLogic.Contracts
{
    public interface ICurrencyService
    {
        Task<IEnumerable<CurrencyDto>> GetAllCurrenciesAsync(string languageCode = "en");
    }
}
