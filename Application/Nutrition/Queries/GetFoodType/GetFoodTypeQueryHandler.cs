using Application.Support.Exceptions;
using AutoMapper;
using Domain.Nutrition;
using MediatR;
using MongoDB.Driver;
using Persistence.Interfaces;

namespace Application.Nutrition.Queries.GetFoodType
{
    public class GetFoodTypeQueryHandler : IRequestHandler<GetFoodTypeQuery, FoodTypeDto>
    {
        private IMapper _mapper;
        private IMongoDbContext _dbContext;

        public GetFoodTypeQueryHandler(IMapper mapper, IMongoDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<FoodTypeDto> Handle(GetFoodTypeQuery request, CancellationToken cancellationToken)
        {
            var foodType = await _dbContext.FoodTypes
                    .Find(c => c.Id.Equals(request.Id))
                    .FirstOrDefaultAsync();

            if (foodType == null)
            {
                throw new NotFoundException(nameof(FoodType), request.Id);
            }

            return _mapper.Map<FoodTypeDto>(foodType);
        }
    }
}
