using MoneyMate_WebApp.BusinessLogic.Contracts;
using MoneyMate_WebApp.BusinessLogic.Dtos;
using MoneyMate_WebApp.DataAccess.UoW;

namespace MoneyMate_WebApp.BusinessLogic.Services
{
    public class TypeService(IUnitOfWork unitOfWork) : ITypeService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.Types.GetAllAsync();
            return types.Select(t => new TypeDto(t.Id, t.Name));
        }
    }
}
