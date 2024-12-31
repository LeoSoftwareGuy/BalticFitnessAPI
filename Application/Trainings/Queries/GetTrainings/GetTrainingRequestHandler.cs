
using Application.Data;
using Application.Support.Interfaces;
using Application.Trainings.DTOs.Trainings;
using BuildingBlocks.CQRS;

namespace Application.Trainings.Queries.GetTrainings
{
    public record GetTrainingsSortedByDay() : IQuery<GetTrainingsSortedByDayResult>;
    public record GetTrainingsSortedByDayResult(List<SortedByDayTraining> SortedByDayTrainingDtos);
    public class GetTrainingRequestHandler : IQueryHandler<GetTrainingsSortedByDay, GetTrainingsSortedByDayResult>
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetTrainingRequestHandler(ITrainingRepository trainingRepository, ICurrentUserService currentUserService)
        {
            _trainingRepository = trainingRepository;
            _currentUserService = currentUserService;
        }

        /// <summary>
        /// Gets a list of SortedByDay objects, each representing a day of the year, 
        /// the exercises performed on that day, and the muscle groups worked. 
        /// This list is then returned as part of the ServiceResponse.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<GetTrainingsSortedByDayResult> Handle(GetTrainingsSortedByDay request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var result = await _trainingRepository.GetAllTrainings(userId);

            //var result = new List<SortedByDayTraining>();

            //var trainings = await _context.Trainings
            //     .Where(t => t.UserId == userId)
            //     .Include(t => t.ExerciseSets)
            //     .ThenInclude(e => e.Exercise)
            //     .ThenInclude(e => e.MuscleGroup)
            //     .AsNoTracking()
            //    .ToListAsync(cancellationToken);

            //if (trainings.Any())
            //{
            //    var muscleGroups = await _context.MuscleGroups
            //        .ToListAsync(cancellationToken);

            //    var sortedByDayTrainings = trainings
            //           .OrderByDescending(d => d.Trained)
            //           .GroupBy(t => t.Trained.Date)
            //           .ToList();

            //    foreach (var dayGroup in sortedByDayTrainings)
            //    {
            //        var allExerciseSetsDoneDuringThisDay = dayGroup.SelectMany(t => t.ExerciseSets).ToList();
            //        var unqiueMuscleGroupsTrainedDuringThisDay = allExerciseSetsDoneDuringThisDay
            //            .Select(e => e.Exercise.MuscleGroup.Name)
            //            .Distinct()
            //            .ToList();

            //        var exercisesPerMuscleGroupSimplified = new Dictionary<string, List<ExerciseGroupDto>>();

            //        foreach (var uniqueMuscleGroupTrainedThisDay in unqiueMuscleGroupsTrainedDuringThisDay)
            //        {
            //            var allExerciseSetsForThisMuscleGroup = allExerciseSetsDoneDuringThisDay.
            //                Where(t => t.Exercise.MuscleGroup.Name == uniqueMuscleGroupTrainedThisDay).ToList();

            //            var uniqueExercisesIdsPerMuscleGroup = allExerciseSetsDoneDuringThisDay.
            //               Where(t => t.Exercise.MuscleGroup.Name == uniqueMuscleGroupTrainedThisDay)
            //               .Select(c => c.ExerciseId)
            //               .Distinct()
            //               .ToList();

            //            var allExercisesAndTheirSetsForMuscleGroup = new List<ExerciseGroupDto>();
            //            foreach (var uniqueExerciseIdForParticluarMuscleGroup in uniqueExercisesIdsPerMuscleGroup)
            //            {
            //                var allExerciseSetsForAnExercise = allExerciseSetsForThisMuscleGroup
            //                    .Where(c => c.ExerciseId == uniqueExerciseIdForParticluarMuscleGroup)
            //                    .Select(c => new ExerciseSetDto()
            //                    {
            //                        ExerciseId = c.ExerciseId,
            //                        Reps = c.Reps,
            //                        Weight = c.Weight
            //                    })
            //                    .ToList();

            //                var exerciseGroup = new ExerciseGroupDto
            //                {
            //                    Name = allExerciseSetsForThisMuscleGroup.FirstOrDefault(e=>e.ExerciseId.Equals(uniqueExerciseIdForParticluarMuscleGroup)).Exercise.Name,
            //                    Id = uniqueExerciseIdForParticluarMuscleGroup,
            //                    ExerciseSets = allExerciseSetsForAnExercise
            //                };

            //                allExercisesAndTheirSetsForMuscleGroup.Add(exerciseGroup);
            //            }

            //            exercisesPerMuscleGroupSimplified.Add(uniqueMuscleGroupTrainedThisDay, allExercisesAndTheirSetsForMuscleGroup);
            //        }

            //        result.Add(new SortedByDayTraining
            //        {
            //            TrainedAtDay = dayGroup.Key.Day,
            //            TrainedAtMonth = dayGroup.Key.Month,
            //            TrainedAtYear = dayGroup.Key.Year,
            //            TrainedAtTime = dayGroup.Key.TimeOfDay.ToString(@"hh\:mm"),
            //            ExercisesPerMuscleGroup = exercisesPerMuscleGroupSimplified
            //        });
            //    }
            //}

            return new GetTrainingsSortedByDayResult(result);
        }
    }
}
