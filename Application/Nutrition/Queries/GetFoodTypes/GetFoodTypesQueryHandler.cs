using Application.Data;
using AutoMapper;
using BuildingBlocks.CQRS;
using Microsoft.EntityFrameworkCore;

namespace Application.Nutrition.Queries.GetFoodTypes
{
    public record GetFoodTypesQuery : IQuery<GetFoodTypesResult>;
    public record GetFoodTypesResult(List<FoodTypeDto> FoodTypeDtos);
    public class GetFoodTypesQueryHandler : IQueryHandler<GetFoodTypesQuery, GetFoodTypesResult>
    {
        private readonly IMapper _mapper;
        private readonly ITrainingDbContext _context;

        public GetFoodTypesQueryHandler(IMapper mapper, ITrainingDbContext mongoDbContext)
        {
            _mapper = mapper;
            _context = mongoDbContext;
        }
        public async Task<GetFoodTypesResult> Handle(GetFoodTypesQuery request, CancellationToken cancellationToken)
        {
            var foodTypes = await _context.FoodTypes
                   .ToListAsync(cancellationToken);

            var foodTypesDto = _mapper.Map<List<FoodTypeDto>>(foodTypes);
            return new GetFoodTypesResult(foodTypesDto);
        }
    }
}
