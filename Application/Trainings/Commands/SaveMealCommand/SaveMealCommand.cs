
using Application.Data;
using Application.Support.Interfaces;
using Application.Trainings.DTOs.Nutrition;
using AutoMapper;
using BuildingBlocks.CQRS;
using Domain.Nutrition;
using MediatR;


namespace Application.Trainings.Commands.SaveMealCommand
{
    public record SaveMealCommand(List<ConsumedProductDto> ProductDtos) : ICommand<SaveMealResult>;
    public record SaveMealResult(int Id);

    public class SaveMealCommandHandler : ICommandHandler<SaveMealCommand, SaveMealResult>
    {
        private readonly IMapper _mapper;
        private readonly ITrainingDbContext _dbContext;
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;

        public SaveMealCommandHandler(IMapper mapper, ITrainingDbContext dbContext, IMediator mediator, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _mediator = mediator;
            _currentUserService = currentUserService;
        }
        public async Task<SaveMealResult> Handle(SaveMealCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }
            var consumedProducts = _mapper.Map<List<ConsumedProduct>>(request.ProductDtos);

            var entity = new Meal
            {
                UserId = userId,
                ConsumedProducts = consumedProducts
            };

            await _dbContext.Meals.AddAsync(entity, cancellationToken: cancellationToken);
            await _mediator.Publish(new MealAdded { UserId = entity.UserId }, cancellationToken);

            return new SaveMealResult(entity.Id);
        }
    }
}
