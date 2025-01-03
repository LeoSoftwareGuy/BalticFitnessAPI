﻿using Application.Data;
using AutoMapper;
using BuildingBlocks.CQRS;
using Microsoft.EntityFrameworkCore;


namespace Application.MuscleGroups.Queries.GetMuscleGroups
{
    public record GetMuscleGroupsQuery : IQuery<GetMuscleGroupsResult>;
    public record GetMuscleGroupsResult(List<MuscleGroupDto> MuscleGroupDtos);
    public class GetMuscleGroupsQueryHandler : IQueryHandler<GetMuscleGroupsQuery, GetMuscleGroupsResult>
    {
        private readonly ITrainingDbContext _context;
        private readonly IMapper _mapper;

        public GetMuscleGroupsQueryHandler(ITrainingDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<GetMuscleGroupsResult> Handle(GetMuscleGroupsQuery request, CancellationToken cancellationToken)
        {
            var muscleGroups = await _context.MuscleGroups
              .AsNoTracking()
              .ToListAsync(cancellationToken);

            var muscleGroupsDtos = _mapper.Map<List<MuscleGroupDto>>(muscleGroups);
            return new GetMuscleGroupsResult(muscleGroupsDtos);
        }
    }
}
