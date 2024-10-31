using Application.Support.Interfaces;
using Application.Trainings.DTOs.Trainings;
using AutoMapper;
using Domain;
using MediatR;
using Persistence.Interfaces;

namespace Application.Trainings.Commands.SaveTrainingCommand
{
    public class SaveTrainingCommand : IRequest<Unit>
    {
        public DateTime Trained { get; set; }
        public List<ExerciseSetDto> ExerciseSets { get; set; }

        public class SaveTrainingCommandHandler : IRequestHandler<SaveTrainingCommand, Unit>
        {
            private readonly IMapper _mapper;
            private readonly IMongoDbContext _context;
            private readonly IMediator _mediator;
            private readonly ICurrentUserService _currentUserService;

            public SaveTrainingCommandHandler(IMongoDbContext context, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService)
            {
                _mapper = mapper;
                _context = context;
                _mediator = mediator;
                _currentUserService = currentUserService;
            }

            public async Task<Unit> Handle(SaveTrainingCommand request, CancellationToken cancellationToken)
            {
                var userId = _currentUserService.UserId;

                if (userId == null)
                {
                    throw new UnauthorizedAccessException("User is not authenticated.");
                }

                var exerciseSets = _mapper.Map<List<ExerciseSet>>(request.ExerciseSets);

                var entity = new Training
                {
                    UserId = userId,
                    Trained = DateTime.UtcNow,
                    ExerciseSets = exerciseSets
                };

                await _context.Trainings.InsertOneAsync(entity, cancellationToken: cancellationToken);
                await _mediator.Publish(new TrainingAdded { UserId = entity.UserId }, cancellationToken);

                return Unit.Value;
            }
        }
    }
}
