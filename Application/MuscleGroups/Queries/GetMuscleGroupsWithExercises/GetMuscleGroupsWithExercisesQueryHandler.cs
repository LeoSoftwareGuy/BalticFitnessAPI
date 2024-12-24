using Application.Data;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Application.MuscleGroups.Queries.GetMuscleGroups
{
    public class GetMuscleGroupsWithExercisesQueryHandler : IRequestHandler<GetMuscleGroupsWithExercisesQuery, List<MuscleGroupDto>>
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

            return _mapper.Map<List<MuscleGroupDto>>(muscleGroups);
        }
    }
}
