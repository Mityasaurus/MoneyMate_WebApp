using Microsoft.Extensions.DependencyInjection;
using MoneyMate_WebApp.BusinessLogic.Contracts;
using MoneyMate_WebApp.BusinessLogic.Services;

namespace MoneyMate_WebApp.BusinessLogic.Installers
{
    public static class CurrencyInstaller
    {
        public static IServiceCollection AddCurrencyService(this IServiceCollection services)
        {
            services.AddScoped<ICurrencyService, CurrencyService>();

            return services;
        }
    }
}
