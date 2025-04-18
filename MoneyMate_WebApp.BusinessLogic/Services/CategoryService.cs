using MoneyMate_WebApp.BusinessLogic.Contracts;
using MoneyMate_WebApp.BusinessLogic.Dtos;
using MoneyMate_WebApp.DataAccess.UoW;

namespace MoneyMate_WebApp.BusinessLogic.Services
{
    public class CategoryService(IUnitOfWork unitOfWork) : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync(string languageCode = "en")
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();
            var translations = await _unitOfWork.CategoryTranslations.GetAllAsync();

            var localizedDtos = categories.Select(category =>
            {
                var translation = translations.FirstOrDefault(t =>
                    t.CategoryId == category.Id && t.LanguageCode == languageCode);

                var localizedName = translation?.Name ?? "[Translation Missing]";
                return new CategoryDto(category.Id, localizedName, category.Type);
            });

            return localizedDtos;
        }
    }
}
