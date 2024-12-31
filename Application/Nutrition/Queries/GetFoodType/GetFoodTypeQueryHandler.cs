//using Application.Data;
//using Application.Support.Exceptions;
//using AutoMapper;
//using BuildingBlocks.CQRS;
//using Domain.Nutrition;
//using Microsoft.EntityFrameworkCore;

//namespace Application.Nutrition.Queries.GetFoodType
//{
//    public record GetFoodTypeQuery(int Id) : IQuery<GetFoodTypeResult>;
//    public record GetFoodTypeResult(FoodTypeDto FoodTypeDto);
//    public class GetFoodTypeQueryHandler : IQueryHandler<GetFoodTypeQuery, GetFoodTypeResult>
//    {
//        private IMapper _mapper;
//        private ITrainingDbContext _dbContext;

//        public GetFoodTypeQueryHandler(IMapper mapper, ITrainingDbContext dbContext)
//        {
//            _mapper = mapper;
//            _dbContext = dbContext;
//        }
//        public async Task<GetFoodTypeResult> Handle(GetFoodTypeQuery request, CancellationToken cancellationToken)
//        {
//            var foodType = await _dbContext.FoodTypes
//                    .FirstOrDefaultAsync(c => c.Id.Equals(request.Id));

//            if (foodType == null)
//            {
//                throw new FoodTypeNotFoundException(nameof(FoodType), request.Id);
//            }

//            var foodTypeDto = _mapper.Map<FoodTypeDto>(foodType);
//            return new GetFoodTypeResult(foodTypeDto);
//        }
//    }
//}
