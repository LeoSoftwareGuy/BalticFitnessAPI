using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces;

namespace Application.Nutrition.Queries.GetFoodTypes
{
    public class GetFoodTypesQueryHandler : IRequestHandler<GetFoodTypesQuery, List<FoodTypeDto>>
    {
        private readonly IMapper _mapper;
        private readonly ITrainingDbContext _context;

        public GetFoodTypesQueryHandler(IMapper mapper, ITrainingDbContext mongoDbContext)
        {
            _mapper = mapper;
            _context = mongoDbContext;
        }
        public async Task<List<FoodTypeDto>> Handle(GetFoodTypesQuery request, CancellationToken cancellationToken)
        {
            var foodTypes = await _context.FoodTypes
                   .ToListAsync(cancellationToken);

            return _mapper.Map<List<FoodTypeDto>>(foodTypes);
        }
    }
}
