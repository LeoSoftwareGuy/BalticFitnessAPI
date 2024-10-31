using AutoMapper;
using Domain.Nutrition;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Persistence.Interfaces;

namespace Application.Nutrition.Queries.GetFoodTypes
{
    public class GetFoodTypesQueryHandler : IRequestHandler<GetFoodTypesQuery, List<FoodTypeDto>>
    {
        private readonly IMapper _mapper;
        private readonly IMongoDbContext _context;

        public GetFoodTypesQueryHandler(IMapper mapper, IMongoDbContext mongoDbContext)
        {
            _mapper = mapper;
            _context = mongoDbContext;
        }
        public async Task<List<FoodTypeDto>> Handle(GetFoodTypesQuery request, CancellationToken cancellationToken)
        {
            var foodTypes = await _context.FoodTypes
                   .Find(FilterDefinition<FoodType>.Empty)
                   .ToListAsync(cancellationToken);

            return _mapper.Map<List<FoodTypeDto>>(foodTypes);
        }
    }
}
