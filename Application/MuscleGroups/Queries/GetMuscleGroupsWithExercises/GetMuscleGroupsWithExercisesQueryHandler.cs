using Application.Data;
using AutoMapper;
using BuildingBlocks.CQRS;
using Microsoft.EntityFrameworkCore;


namespace Application.MuscleGroups.Queries.GetMuscleGroups
{
    public record GetMuscleGroupsWithExercisesQuery : IQuery<List<MuscleGroupDto>>;

    public class GetMuscleGroupsWithExercisesQueryHandler : IQueryHandler<GetMuscleGroupsWithExercisesQuery, List<MuscleGroupDto>>
    {
        private readonly ITrainingDbContext _context;
        private readonly IMapper _mapper;

        public GetMuscleGroupsWithExercisesQueryHandler(ITrainingDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<MuscleGroupDto>> Handle(GetMuscleGroupsWithExercisesQuery request, CancellationToken cancellationToken)
        {
            var muscleGroups = await _context.MuscleGroups
              .AsNoTracking()
              .Include(c => c.Exercises)
              .ToListAsync(cancellationToken);

            var muscleGroupsDtos = _mapper.Map<List<MuscleGroupDto>>(muscleGroups);

            return muscleGroupsDtos;
        }
    }
}
