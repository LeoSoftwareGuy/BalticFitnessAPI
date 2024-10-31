using AutoMapper;
using Domain;
using MediatR;
using MongoDB.Driver;
using Persistence.Interfaces;

namespace Application.MuscleGroups.Queries.GetMuscleGroups
{
    public class GetMuscleGroupsQueryHandler : IRequestHandler<GetMuscleGroupsQuery, List<MuscleGroupDto>>
    {
        private readonly IMongoDbContext _context;
        private readonly IMapper _mapper;

        public GetMuscleGroupsQueryHandler(IMongoDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<MuscleGroupDto>> Handle(GetMuscleGroupsQuery request, CancellationToken cancellationToken)
        {
            var muscleGroups = await _context.MuscleGroups
              .Find(FilterDefinition<MuscleGroup>.Empty) // Use an empty filter to get all documents
              .ToListAsync(cancellationToken); 

            // Map the results to your DTOs
            return _mapper.Map<List<MuscleGroupDto>>(muscleGroups);
        }
    }
}
