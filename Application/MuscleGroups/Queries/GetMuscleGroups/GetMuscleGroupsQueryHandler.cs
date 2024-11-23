using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces;

namespace Application.MuscleGroups.Queries.GetMuscleGroups
{
    public class GetMuscleGroupsQueryHandler : IRequestHandler<GetMuscleGroupsQuery, List<MuscleGroupDto>>
    {
        private readonly ITrainingDbContext _context;
        private readonly IMapper _mapper;

        public GetMuscleGroupsQueryHandler(ITrainingDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<MuscleGroupDto>> Handle(GetMuscleGroupsQuery request, CancellationToken cancellationToken)
        {
            var muscleGroups = await _context.MuscleGroups
              .ToListAsync(cancellationToken); 

            return _mapper.Map<List<MuscleGroupDto>>(muscleGroups);
        }
    }
}
