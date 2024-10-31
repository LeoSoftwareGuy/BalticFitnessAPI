
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Persistence.Interfaces;
using Persistence.MongoDatabase;
using Persistence.SqlDataBase.AuthorizationDB;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            // Register MongoDB client
            services.AddSingleton<IMongoClient>(c =>
            {
                var mongoConfig = configuration.GetSection("Mongo");
                return new MongoClient(mongoConfig.GetSection("ConnectionString").Value);
            });

            // Register MongoDbContext
            services.AddScoped<IMongoDbContext>(c =>
            {
                var client = c.GetService<IMongoClient>();
                return new MongoDbContext(client, configuration);
            });


            services.AddDbContext<AuthorizationDbContext>(options =>
              options.UseSqlite(configuration.GetConnectionString("AuthorizationConnection")));

            services.AddScoped<IAuthorizationDbContext, AuthorizationDbContext>();
            return services;
        }
    }
}
