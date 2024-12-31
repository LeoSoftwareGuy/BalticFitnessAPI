using Application.Data;
using Application.Support.Exceptions;
using AutoMapper;
using BuildingBlocks.CQRS;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.MuscleGroups.Queries.GetExercises
{
    public record GetExcercisesForMuscleGroupQuery(int MuscleGroupId) : IQuery<GetExercisesResult>;
    public record GetExercisesResult(MuscleGroupDto MuscleGroupDto);

    public class GetExcercisesQueryHandler : IQueryHandler<GetExcercisesForMuscleGroupQuery, GetExercisesResult>
    {
        private IMuscleGroupRepository _context;
        private IMapper _mapper;

        public GetExcercisesQueryHandler(IMuscleGroupRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        //Dapper
        public async Task<GetExercisesResult> Handle(GetExcercisesForMuscleGroupQuery request, CancellationToken cancellationToken)
        {
            var muscleGroup =  await _context.GetMuscleGroup(request.MuscleGroupId);

            if (muscleGroup == null)
            {
                throw new MuscleGroupNotFoundException(nameof(MuscleGroup), new { request.MuscleGroupId });
            }

            var muscleGroupDto = _mapper.Map<MuscleGroupDto>(muscleGroup);
            return new GetExercisesResult(muscleGroupDto);
        }


        ////EFCORE

        //public async Task<GetExercisesResult> Handle(GetExcercisesForMuscleGroupQuery request, CancellationToken cancellationToken)
        //{
        //    var muscleGroup = await _context.MuscleGroups
        //                    .Include(c => c.Exercises)
        //                    .AsNoTracking()
        //                    .FirstOrDefaultAsync(c => c.Id.Equals(request.MuscleGroupId), cancellationToken);


        //    if (muscleGroup == null)
        //    {
        //        throw new MuscleGroupNotFoundException(nameof(MuscleGroup), new { request.MuscleGroupId });
        //    }

        //    var muscleGroupDto = _mapper.Map<MuscleGroupDto>(muscleGroup);
        //    return new GetExercisesResult(muscleGroupDto);
        //}
    }
}
