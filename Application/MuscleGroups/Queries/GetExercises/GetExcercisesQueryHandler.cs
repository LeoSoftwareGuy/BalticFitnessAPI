using Application.Data;
using Application.Support.Exceptions;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.MuscleGroups.Queries.GetExercises
{
    public class GetExcercisesQueryHandler : IRequestHandler<GetExcercisesQuery, List<ExerciseDto>>
    {
        private ITrainingDbContext _context;
        private IMapper _mapper;

        public GetExcercisesQueryHandler(ITrainingDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ExerciseDto>> Handle(GetExcercisesQuery request, CancellationToken cancellationToken)
        {
            var muscleGroup = await _context.MuscleGroups
                            .Include(c => c.Exercises)
                            .AsNoTracking()
                            .FirstOrDefaultAsync(c => c.Id.Equals(request.MuscleGroupId), cancellationToken);
 

            if (muscleGroup == null)
            {
                throw new MuscleGroupNotFoundException(nameof(MuscleGroup), new { request.MuscleGroupId });
            }

            var exerciseDtos = _mapper.Map<List<ExerciseDto>>(muscleGroup.Exercises);
            return exerciseDtos;
        }
    }
}
