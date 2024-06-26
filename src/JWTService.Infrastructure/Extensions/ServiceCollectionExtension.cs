using JWTService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Oracle.ManagedDataAccess.Client;
namespace JWTService.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, string? connectionString)
        {
            return services.AddDbContext<ApplicationUserDbContext>(opt => opt.UseOracle(connectionString,
                b => b.MigrationsAssembly(typeof(ServiceCollectionExtension).Assembly.FullName)));
        }
    }
}
