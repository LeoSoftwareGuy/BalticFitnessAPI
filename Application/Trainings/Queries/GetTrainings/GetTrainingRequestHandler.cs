
using Application.Support.Interfaces;
using Application.Trainings.DTOs.Trainings;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces;

namespace Application.Trainings.Queries.GetTrainings
{
    public class GetTrainingRequestHandler : IRequestHandler<GetTrainingsRequest, List<SortedByDayTraining>>
    {
        private readonly ITrainingDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetTrainingRequestHandler(ITrainingDbContext context, IMapper mapper, ICurrentUserService currentUserService)
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

            var trainings = await _context.Trainings
                 .Where(t => t.UserId == userId)
                 .Include(t => t.ExerciseSets)
                 .ThenInclude(e => e.Exercise)
                 .AsNoTracking()
                .ToListAsync(cancellationToken);

            if (trainings.Any())
            {
                var muscleGroups = (await _context.MuscleGroups
                    .ToListAsync(cancellationToken));

                var sortedByDayTrainings = trainings
                       .OrderByDescending(d => d.Trained)
                       .GroupBy(t => t.Trained.Date)
                       .ToList();

                foreach (var dayGroup in sortedByDayTrainings)
                {
                    var exerciseSetsPerDay = dayGroup.SelectMany(t => t.ExerciseSets).ToList();

                    // Group exercise sets by muscle group
                    var exercisesPerMuscleGroup = exerciseSetsPerDay
                        .GroupBy(es => es.Exercise.MuscleGroupId) // Group by MuscleGroupId
                        .ToDictionary(
                            g => muscleGroups.FirstOrDefault(m => m.Id == g.Key)?.Name!, // Get muscle group name
                            g => g.Select(es => new ExerciseGroupDto
                            {
                                Id = es.Exercise.Id,
                                Name = es.Exercise.Name,
                                ExerciseSets = _mapper.Map<List<ExerciseSetDto>>(g.ToList())
                            }).ToList()
                        );

                    result.Add(new SortedByDayTraining
                    {
                        TrainedAtDay = dayGroup.Key.Day,
                        TrainedAtMonth = dayGroup.Key.Month,
                        TrainedAtYear = dayGroup.Key.Year,
                        TrainedAtTime = dayGroup.Key.TimeOfDay.ToString(@"hh\:mm"),
                        ExercisesPerMuscleGroup = exercisesPerMuscleGroup
                    });
                }
            }

            return result;
        }
    }
}
