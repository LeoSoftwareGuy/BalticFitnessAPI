﻿using Application.Support.Exceptions;
using AutoMapper;
using Domain;
using MediatR;
using MongoDB.Driver;
using Persistence.Interfaces;

namespace Application.MuscleGroups.Queries.GetMuscleGroup
{
    public class GetMuscleGroupQueryHandler : IRequestHandler<GetMuscleGroupQuery, MuscleGroupDto>
    {
        private readonly IMongoDbContext _context;
        private readonly IMapper _mapper;

        public GetMuscleGroupQueryHandler(IMongoDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<MuscleGroupDto> Handle(GetMuscleGroupQuery request, CancellationToken cancellationToken)
        {
            var muscleGroup = await _context.MuscleGroups
                    .Find(c => c.Id.Equals(request.Id))
                    .FirstOrDefaultAsync();

         
            if (muscleGroup == null)
            {
                throw new NotFoundException(nameof(MuscleGroup), request.Id);
            }

            return _mapper.Map<MuscleGroupDto>(muscleGroup);
        }
    }
}