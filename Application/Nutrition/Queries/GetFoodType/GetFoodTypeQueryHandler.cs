using Application.Data;
using Application.Support.Exceptions;
using AutoMapper;
using Domain.Nutrition;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Nutrition.Queries.GetFoodType
{
    public class GetFoodTypeQueryHandler : IRequestHandler<GetFoodTypeQuery, FoodTypeDto>
    {
        private IMapper _mapper;
        private ITrainingDbContext _dbContext;

        public GetFoodTypeQueryHandler(IMapper mapper, ITrainingDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<FoodTypeDto> Handle(GetFoodTypeQuery request, CancellationToken cancellationToken)
        {
            var foodType = await _dbContext.FoodTypes
                    .FirstOrDefaultAsync(c => c.Id.Equals(request.Id));

            if (foodType == null)
            {
                throw new FoodTypeNotFoundException(nameof(FoodType), request.Id);
            }

            return _mapper.Map<FoodTypeDto>(foodType);
        }
    }
}
