
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
            return new GetTrainingsSortedByDayResult(result);
        }
    }
}
