using MoneyMate_WebApp.BusinessLogic.Dtos;

namespace MoneyMate_WebApp.BusinessLogic.Contracts
{
    public interface ITypeService
    {
        Task<IEnumerable<TypeDto>> GetAllTypesAsync(string languageCode = "en");
    }
}
