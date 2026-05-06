using Microsoft.EntityFrameworkCore;
using StatementService.Data;

namespace StatementService.Extensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(config.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
