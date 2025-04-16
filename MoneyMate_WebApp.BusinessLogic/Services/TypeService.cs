using MoneyMate_WebApp.BusinessLogic.Contracts;
using MoneyMate_WebApp.BusinessLogic.Dtos;
using MoneyMate_WebApp.DataAccess.UoW;

namespace MoneyMate_WebApp.BusinessLogic.Services
{
    public class TypeService(IUnitOfWork unitOfWork) : ITypeService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync(string languageCode = "en")
        {
            // Отримуємо всі типи
            var types = await _unitOfWork.Types.GetAllAsync();

            // Отримуємо всі переклади типів для заданої мови
            var typeTranslations = await _unitOfWork.TypeTranslations.GetAllAsync();

            return types.Select(t =>
            {
                // Знаходимо переклад для типу
                var typeTranslation = typeTranslations
                    .FirstOrDefault(tt => tt.TypeId == t.Id && tt.LanguageCode == languageCode);

                return new TypeDto(
                    t.Id,
                    typeTranslation?.Name ?? "[Translation Missing]"  // Якщо переклад відсутній, використовуємо значення за замовчуванням
                );
            });
        }
    }
}
