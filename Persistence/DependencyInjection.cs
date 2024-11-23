
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Interfaces;
using Persistence.SqlDataBase.AuthorizationDB;
using Persistence.SqlDataBase.TrainingsDB;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TrainingsDbContext>(options =>
                  options.UseNpgsql(configuration.GetConnectionString("TrainingsConnection")));

            services.AddDbContext<AuthorizationDbContext>(options =>
              options.UseNpgsql(configuration.GetConnectionString("AuthorizationConnection")));

            services.AddScoped<IAuthorizationDbContext, AuthorizationDbContext>();
            services.AddScoped<ITrainingDbContext, TrainingsDbContext>();
            return services;
        }
    }
}
