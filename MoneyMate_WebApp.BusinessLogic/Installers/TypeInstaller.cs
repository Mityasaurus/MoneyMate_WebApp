using Microsoft.Extensions.DependencyInjection;
using MoneyMate_WebApp.BusinessLogic.Contracts;
using MoneyMate_WebApp.BusinessLogic.Services;

namespace MoneyMate_WebApp.BusinessLogic.Installers
{
    public static class TypeInstaller
    {
        public static IServiceCollection AddTypeService(this IServiceCollection services)
        {
            services.AddScoped<ITypeService, TypeService>();

            return services;
        }
    }
}
