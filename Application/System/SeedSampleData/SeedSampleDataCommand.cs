using MediatR;
using Persistence.Interfaces;

namespace Application.System.SeedSampleData
{
    public class SeedSampleDataCommand : IRequest
    {
    }

    public class SeedSampleDataCommandHandler : IRequestHandler<SeedSampleDataCommand>
    {
        private readonly IMongoDbContext _dbContext;
        public SeedSampleDataCommandHandler(IMongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(SeedSampleDataCommand request, CancellationToken cancellationToken)
        {
            var seeder = new SampleDataSeeder(_dbContext);
            await seeder.SeedAllAsync(cancellationToken);
        }
    }
}
