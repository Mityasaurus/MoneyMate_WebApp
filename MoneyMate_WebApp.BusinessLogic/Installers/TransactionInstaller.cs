using Microsoft.Extensions.DependencyInjection;
using MoneyMate_WebApp.BusinessLogic.Contracts;
using MoneyMate_WebApp.BusinessLogic.Services;

namespace MoneyMate_WebApp.BusinessLogic.Installers
{
    public static class TransactionInstaller
    {
        public static IServiceCollection AddTransactionService(this IServiceCollection services)
        {
            services.AddScoped<ITransactionService, TransactionService>();

            return services;
        }
    }
}
