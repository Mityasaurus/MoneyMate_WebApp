using Microsoft.Extensions.DependencyInjection;
using MoneyMate_WebApp.DataAccess.UoW;

namespace MoneyMate_WebApp.DataAccess.Installers
{
    public static class UoWInstaller
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
