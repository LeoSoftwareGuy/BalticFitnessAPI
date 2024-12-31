//using Application.Data;
//using MediatR;

//namespace Application.System.SeedSampleData
//{
//    public class SeedSampleDataCommand : IRequest
//    {
//    }

//    public class SeedSampleDataCommandHandler : IRequestHandler<SeedSampleDataCommand>
//    {
//        private readonly ITrainingDbContext _dbContext;
//        public SeedSampleDataCommandHandler(ITrainingDbContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        public async Task Handle(SeedSampleDataCommand request, CancellationToken cancellationToken)
//        {
//            var seeder = new SampleDataSeeder(_dbContext);
//            await seeder.SeedAllAsync(cancellationToken);

//        }
//    }
//}
