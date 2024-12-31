//using Application.System.SeedSampleData;
//using MediatR;
//using Microsoft.EntityFrameworkCore;
//using Persistence.SqlDataBase.TrainingsDB;

//namespace BalticsFitness.API.Services
//{
//    public static class DatabaseExtensions
//    {
//        public static async Task InitialiseDatabaseAsync(this WebApplication app)
//        {
//            using var scope = app.Services.CreateScope();

//            var services = scope.ServiceProvider;
//            var context = services.GetRequiredService<TrainingsDbContext>();

//            if (!await context.MuscleGroups.AnyAsync())
//            {
//                try
//                {
//                    // Apply any pending migrations
//                    await context.Database.MigrateAsync();

//                    // Now use IMediator to trigger the seeding process
//                    var mediator = services.GetRequiredService<IMediator>();
//                    await mediator.Send(new SeedSampleDataCommand(), CancellationToken.None);
//                }
//                catch (Exception ex)
//                {
//                    // Log any exceptions that occur
//                    var logger = services.GetRequiredService<ILogger<Program>>();
//                    logger.LogError(ex, "An error occurred while migrating or initializing the database.");
//                }
//            }
//        }
//    }
//}