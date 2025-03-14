using Microsoft.Extensions.DependencyInjection;
using MoneyMate_WebApp.BusinessLogic.Contracts;
using MoneyMate_WebApp.BusinessLogic.Services;

namespace MoneyMate_WebApp.BusinessLogic.Installers
{
    public static class AccountInstaller
    {
        public static IServiceCollection AddAccountService(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();

            return services;
        }
    }
}
