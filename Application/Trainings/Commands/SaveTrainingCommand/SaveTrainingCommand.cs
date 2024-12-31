using Application.Data;
using Application.Support.Interfaces;
using Application.Trainings.DTOs.Trainings;
using AutoMapper;
using BuildingBlocks.CQRS;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Application.Trainings.Commands.SaveTrainingCommand
{
    public record SaveTrainingCommand(List<ExerciseSetDto> ExerciseSets) : ICommand<SaveTrainingResult>;

    public record SaveTrainingResult(int Id);

    public class SaveTrainingCommandHandler : ICommandHandler<SaveTrainingCommand, SaveTrainingResult>
    {
        private readonly IMapper _mapper;
        private readonly ITrainingRepository _trainingRepository;
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;

        public SaveTrainingCommandHandler(ITrainingRepository trainingRepository, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _trainingRepository = trainingRepository;
            _mediator = mediator;
            _currentUserService = currentUserService;
        }

        //Dapper
        public async Task<SaveTrainingResult> Handle(SaveTrainingCommand request, CancellationToken cancellationToken)
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
                ExerciseSets = exerciseSets
            };

            var trainingId = await _trainingRepository.SaveTraining(entity);
            await _mediator.Publish(new TrainingAdded { UserId = entity.UserId }, cancellationToken);

            return new SaveTrainingResult(entity.Id);
        }



        ////EFCORE
        //public async Task<SaveTrainingResult> Handle(SaveTrainingCommand request, CancellationToken cancellationToken)
        //{
        //    var userId = _currentUserService.UserId;

        //    if (userId == null)
        //    {
        //        throw new UnauthorizedAccessException("User is not authenticated.");
        //    }

        //    var exerciseSets = _mapper.Map<List<ExerciseSet>>(request.ExerciseSets);

        //    // Fetch all required exercises in one go
        //    var exerciseIds = exerciseSets.Select(e => e.ExerciseId).Distinct();
        //    var existingExercises = await _context.Exercises
        //        .Where(e => exerciseIds.Contains(e.Id))
        //        .ToDictionaryAsync(e => e.Id, cancellationToken);

        //    // Attach exercises to ExerciseSets
        //    foreach (var set in exerciseSets)
        //    {
        //        if (existingExercises.TryGetValue(set.ExerciseId, out var exercise))
        //        {
        //            set.Exercise = exercise;
        //        }
        //        else
        //        {
        //            throw new KeyNotFoundException($"Exercise with Id {set.ExerciseId} not found.");
        //        }
        //    }

        //    // Create new Training entity
        //    var entity = new Training
        //    {
        //        UserId = userId,
        //        ExerciseSets = exerciseSets
        //    };

        //    await _context.Trainings.AddAsync(entity, cancellationToken);
        //    await _context.SaveChangesAsync(cancellationToken);
        //    // Notify other parts of the system
        //    await _mediator.Publish(new TrainingAdded { UserId = entity.UserId }, cancellationToken);

        //    return new SaveTrainingResult(entity.Id);
        //}
    }
}
