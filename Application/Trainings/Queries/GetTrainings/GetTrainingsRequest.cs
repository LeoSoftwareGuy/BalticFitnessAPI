using Application.Trainings.DTOs.Trainings;
using MediatR;

namespace Application.Trainings.Queries.GetTrainings
{
    public class GetTrainingsRequest : IRequest<List<SortedByDayTraining>>
    {
    }
}
