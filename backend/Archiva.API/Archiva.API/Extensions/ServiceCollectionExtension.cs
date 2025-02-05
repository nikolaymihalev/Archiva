using Archiva.Infrastructure.Common;
using Archiva.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection") ?? throw new ArgumentException("The connection string was not found!");

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IRepository, Repository>();

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services) 
        {
            return services;
        }
    }
}
