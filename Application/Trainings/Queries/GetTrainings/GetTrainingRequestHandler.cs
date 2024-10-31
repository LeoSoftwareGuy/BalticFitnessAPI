using Amazon.Runtime.Internal.Transform;
using Application.Support.Interfaces;
using Application.Trainings.DTOs.Trainings;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Persistence.Interfaces;

namespace Application.Trainings.Queries.GetTrainings
{
    public class GetTrainingRequestHandler : IRequestHandler<GetTrainingsRequest, List<SortedByDayTraining>>
    {
        private readonly IMongoDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetTrainingRequestHandler(IMongoDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }


        /// <summary>
        /// Gets a list of SortedByDay objects, each representing a day of the year, 
        /// the exercises performed on that day, and the muscle groups worked. 
        /// This list is then returned as part of the ServiceResponse.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<SortedByDayTraining>> Handle(GetTrainingsRequest request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var result = new List<SortedByDayTraining>();

            var trainings = await _context.Trainings.Find(t => t.UserId == userId)
                .ToListAsync(cancellationToken);

            if (trainings.Any())
            {
                var muscleGroups = (await _context.MuscleGroups
                    .Find(FilterDefinition<MuscleGroup>.Empty)
                    .ToListAsync(cancellationToken));

                var sortedByDayTrainings = new Dictionary<DateTime, List<ExerciseSet>>();

                foreach (var training in trainings.OrderByDescending(d => d.Trained))
                {
                    if (sortedByDayTrainings.ContainsKey(training.Trained))
                    {
                        sortedByDayTrainings[training.Trained].AddRange(training.ExerciseSets.ToList());
                    }

                    else
                    {
                        sortedByDayTrainings[training.Trained] = training.ExerciseSets.ToList();
                    }
                }



                foreach (var sortedByDateTraining in sortedByDayTrainings)
                {
                    var exerciseSetsPerTraining = sortedByDateTraining.Value;
                    var muscleGroupIds = exerciseSetsPerTraining.Select(e => e.Exercise.MuscleGroupId).Distinct().ToList();

                    var exercisesPerMuscleGroup = new Dictionary<string, List<ExerciseGroupDto>>();
                    foreach (var muscleGroup in muscleGroupIds)
                    {
                        var allTrainingsPerMuscleGroup = exerciseSetsPerTraining.Where(m => m.Exercise.MuscleGroupId.Equals(muscleGroup)).ToList();
                        var uniqueExercisesPerMuscleGroup = allTrainingsPerMuscleGroup.Select(e => e.Exercise).Distinct().ToList();

                        var uniqueExerciesPerTrainingWithEachSet = new List<ExerciseGroupDto>();
                        foreach (var exerice in uniqueExercisesPerMuscleGroup)
                        {
                            var allSetsPerExercise = allTrainingsPerMuscleGroup.Where(e => e.Exercise.Id == exerice.Id).ToList();
                            var exerciseGroup = new ExerciseGroupDto
                            {
                                Id = exerice.Id,
                                Name = exerice.Name,
                                ExerciseSets = _mapper.Map<List<ExerciseSetDto>>(allSetsPerExercise)
                            };

                            uniqueExerciesPerTrainingWithEachSet.Add(exerciseGroup);
                        }

                        exercisesPerMuscleGroup.Add(muscleGroups.FirstOrDefault(c => c.Id.Equals(muscleGroup)).Name, uniqueExerciesPerTrainingWithEachSet);
                    }

                    result.Add(new SortedByDayTraining
                    {
                        TrainedAtDay = sortedByDateTraining.Key.Day,
                        TrainedAtMonth = sortedByDateTraining.Key.Month,
                        TrainedAtYear = sortedByDateTraining.Key.Year,
                        TrainedAtTime = sortedByDateTraining.Key.TimeOfDay.ToString(@"hh\:mm"),
                        ExercisesPerMuscleGroup = exercisesPerMuscleGroup
                    });
                }
            }

            return result;
        }
    }
}
