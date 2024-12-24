using Application.Data;
using Application.Support.Interfaces;
using Application.Trainings.DTOs.Trainings;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Application.Trainings.Commands.SaveTrainingCommand
{
    public class SaveTrainingCommand : IRequest<Unit>
    {
        public List<ExerciseSetDto> ExerciseSets { get; set; }

        public class SaveTrainingCommandHandler : IRequestHandler<SaveTrainingCommand, Unit>
        {
            private readonly IMapper _mapper;
            private readonly ITrainingDbContext _context;
            private readonly IMediator _mediator;
            private readonly ICurrentUserService _currentUserService;

            public SaveTrainingCommandHandler(ITrainingDbContext context, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService)
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

                // Map incoming DTOs to ExerciseSet entities
                var exerciseSets = _mapper.Map<List<ExerciseSet>>(request.ExerciseSets);

                // Fetch all required exercises in one go
                var exerciseIds = exerciseSets.Select(e => e.ExerciseId).Distinct();
                var existingExercises = await _context.Exercises
                    .Where(e => exerciseIds.Contains(e.Id))
                    .ToDictionaryAsync(e => e.Id, cancellationToken);

                // Attach exercises to ExerciseSets
                foreach (var set in exerciseSets)
                {
                    if (existingExercises.TryGetValue(set.ExerciseId, out var exercise))
                    {
                        set.Exercise = exercise;
                    }
                    else
                    {
                        throw new KeyNotFoundException($"Exercise with Id {set.ExerciseId} not found.");
                    }
                }

                // Create new Training entity
                var entity = new Training
                {
                    UserId = userId,
                    ExerciseSets = exerciseSets
                };

                await _context.Trainings.AddAsync(entity, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                // Notify other parts of the system
                await _mediator.Publish(new TrainingAdded { UserId = entity.UserId }, cancellationToken);

                return Unit.Value;
            }
        }
    }
}
