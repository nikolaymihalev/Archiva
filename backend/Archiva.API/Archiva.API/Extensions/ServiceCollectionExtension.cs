using Archiva.API.Extensions;
using Archiva.Core.Conracts;
using Archiva.Core.Contracts;
using Archiva.Core.Services;
using Archiva.Infrastructure.Common;
using Archiva.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDocumentService, DocumentService>();

            return services;
        }

        public static IServiceCollection AddApplicationAuthentication(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "http://localhost:4200",
                        ValidAudience = "http://localhost:4200",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!))
                    };
                });

            services.Configure<JwtSettings>(config.GetSection("Jwt"));

            return services;
        }
    }
}
