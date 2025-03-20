using Microsoft.Extensions.DependencyInjection;
using MoneyMate_WebApp.BusinessLogic.Contracts;
using MoneyMate_WebApp.BusinessLogic.Services;

namespace MoneyMate_WebApp.BusinessLogic.Installers
{
    public static class CategoryInstaller
    {
        public static IServiceCollection AddCategoryService(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();

            return services;
        }
    }
}
