using Application.Data;
using Application.Support.Exceptions;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Application.MuscleGroups.Queries.GetMuscleGroup
{
    public class GetMuscleGroupQueryHandler : IRequestHandler<GetMuscleGroupQuery, MuscleGroupDto>
    {
        private readonly ITrainingDbContext _context;
        private readonly IMapper _mapper;

        public GetMuscleGroupQueryHandler(ITrainingDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<MuscleGroupDto> Handle(GetMuscleGroupQuery request, CancellationToken cancellationToken)
        {
            var muscleGroup = await _context.MuscleGroups
                    .Include(c => c.Exercises)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Id.Equals(request.Id));

            if (muscleGroup == null)
            {
                throw new MuscleGroupNotFoundException(nameof(MuscleGroup), request.Id);
            }

            return _mapper.Map<MuscleGroupDto>(muscleGroup);
        }
    }
}
