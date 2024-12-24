using BuildingBlocks.Exceptions.Handler;
using Infrastructure.Models;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IJwTokenService, JwtTokenService>(sp =>
            {
                var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
                return new JwtTokenService(jwtSettings);
            });

            services.AddTransient<IAutherizationService, AutherizationService>();

            services.AddExceptionHandler<CustomExceptionHandler>();
            services.AddHealthChecks()
             .AddNpgSql((configuration.GetConnectionString("AuthorizationConnection")!));

            return services;
        }
    }
}
