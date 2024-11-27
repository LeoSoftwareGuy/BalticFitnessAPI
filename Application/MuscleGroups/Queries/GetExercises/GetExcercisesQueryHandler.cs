using Application.Support.Exceptions;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces;

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
            if (string.IsNullOrWhiteSpace(request.BodyPartUrl))
            {
                throw new ArgumentException("BodyPartUrl cannot be null or empty.", nameof(request.BodyPartUrl));
            }

            var muscleGroup = await _context.MuscleGroups
                            .Include(c => c.Exercises)
                            .AsNoTracking()
                            .FirstOrDefaultAsync(c => c.Name.ToLower() == request.BodyPartUrl.ToLower(), cancellationToken);
 

            if (muscleGroup == null)
            {
                throw new NotFoundException(nameof(MuscleGroup), request.BodyPartUrl);
            }

            var exerciseDtos = _mapper.Map<List<ExerciseDto>>(muscleGroup.Exercises);
            return exerciseDtos;
        }
    }
}
