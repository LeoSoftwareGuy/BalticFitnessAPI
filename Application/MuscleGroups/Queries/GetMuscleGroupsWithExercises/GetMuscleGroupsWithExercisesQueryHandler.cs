using Application.Data;
using AutoMapper;
using BuildingBlocks.CQRS;
using Microsoft.EntityFrameworkCore;


namespace Application.MuscleGroups.Queries.GetMuscleGroups
{
    public record GetMuscleGroupsWithExercisesQuery : IQuery<List<MuscleGroupDto>>;

    public class GetMuscleGroupsWithExercisesQueryHandler : IQueryHandler<GetMuscleGroupsWithExercisesQuery, List<MuscleGroupDto>>
    {
        private readonly IMuscleGroupRepository _context;
        private readonly IMapper _mapper;

        public GetMuscleGroupsWithExercisesQueryHandler(IMuscleGroupRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //Dapper
        public async Task<List<MuscleGroupDto>> Handle(GetMuscleGroupsWithExercisesQuery request, CancellationToken cancellationToken)
        {
            var muscleGroups = await _context.GetAllMuscleGroupsWithExercises();
            var muscleGroupsDtos = _mapper.Map<List<MuscleGroupDto>>(muscleGroups);
            return muscleGroupsDtos;
        }






        ////EFCore
        //public async Task<List<MuscleGroupDto>> Handle(GetMuscleGroupsWithExercisesQuery request, CancellationToken cancellationToken)
        //{
        //    var muscleGroups = await _context.MuscleGroups
        //      .AsNoTracking()
        //      .Include(c => c.Exercises)
        //      .ToListAsync(cancellationToken);

        //    var muscleGroupsDtos = _mapper.Map<List<MuscleGroupDto>>(muscleGroups);

        //    return muscleGroupsDtos;
        //}
    }
}
