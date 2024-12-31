using Application.Data;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;
using Persistence.SqlDataBase;
using Persistence.SqlDataBase.AuthorizationDB;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(serviceProvider =>
            {
                var connectionString = configuration.GetConnectionString("TrainingsConnection") ??
                 throw new ApplicationException("connection string is null");

                return new TrainingsDbConnectionFactory(connectionString);
            });

            services.AddDbContext<AuthorizationDbContext>(options =>
             options.UseNpgsql(configuration.GetConnectionString("AuthorizationConnection")));

            services.AddScoped<IAuthorizationDbContext, AuthorizationDbContext>();

            services.AddTransient<IMuscleGroupRepository, MuscleGroupRepository>();
            services.AddTransient<IMonthlyStatisticsRepository, MonthlyStatisticsRepository>();
            services.AddTransient<ITrainingRepository, TrainingRepository>();
            return services;
        }
    }
}
