using Application.Support.Interfaces;
using Application.Trainings.DTOs.Nutrition;
using AutoMapper;
using Domain.Nutrition;
using MediatR;
using Persistence.Interfaces;

namespace Application.Trainings.Commands.SaveMealCommand
{
    public class SaveMealCommand : IRequest<Unit>
    {
        public DateTime MealTime { get; set; }

        public List<ConsumedProductDto> Products { get; set; } 

        public class SaveMealCommandHandler : IRequestHandler<SaveMealCommand, Unit>
        {
            private readonly IMapper _mapper;
            private readonly IMongoDbContext _dbContext;
            private readonly IMediator _mediator;
            private readonly ICurrentUserService _currentUserService;

            public SaveMealCommandHandler(IMapper mapper, IMongoDbContext dbContext, IMediator mediator, ICurrentUserService currentUserService)
            {
                _mapper = mapper;
                _dbContext = dbContext;
                _mediator = mediator;
                _currentUserService = currentUserService;
            }
            public async Task<Unit> Handle(SaveMealCommand request, CancellationToken cancellationToken)
            {
                var userId = _currentUserService.UserId;

                if (userId == null)
                {
                    throw new UnauthorizedAccessException("User is not authenticated.");
                }
                var consumedProducts = _mapper.Map<List<ConsumedProduct>>(request.Products);

                var entity = new Meal
                {
                    UserId = userId,
                    MealTime = DateTime.UtcNow,
                    Products = consumedProducts
                };

                await _dbContext.Meals.InsertOneAsync(entity, cancellationToken: cancellationToken);
                await _mediator.Publish(new MealAdded { UserId = entity.UserId }, cancellationToken);

                return Unit.Value;
            }
        }
    }
}
