using Microsoft.Extensions.DependencyInjection;
using MoneyMate_WebApp.DataAccess.Repositories;

namespace MoneyMate_WebApp.DataAccess.Installers
{
    public static class RepositoryInstaller
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }
    }
}
