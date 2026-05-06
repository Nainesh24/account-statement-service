using StatementService.Repositories;
using StatementService.Repositories.Interface;
using StatementService.Services;
using StatementService.Services.Interface;

namespace StatementService.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Repository
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            // Service
            services.AddScoped<IStatementServices, StatementServices>();
            return services;
        }
    }
}
