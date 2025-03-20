using MoneyMate_WebApp.BusinessLogic.Dtos;

namespace MoneyMate_WebApp.BusinessLogic.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
    }
}
