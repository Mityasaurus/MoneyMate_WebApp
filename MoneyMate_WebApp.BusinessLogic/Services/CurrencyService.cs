using MoneyMate_WebApp.BusinessLogic.Contracts;
using MoneyMate_WebApp.BusinessLogic.Dtos;
using MoneyMate_WebApp.DataAccess.UoW;

namespace MoneyMate_WebApp.BusinessLogic.Services
{
    public class CurrencyService(IUnitOfWork unitOfWork) : ICurrencyService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<CurrencyDto>> GetAllCurrenciesAsync()
        {
            var currencies = await _unitOfWork.Currencies.GetAllAsync();
            return currencies.Select(c => new CurrencyDto(c.Id, c.CurrencyName, c.CurrencyCode, c.Symbol));
        }
    }
}
