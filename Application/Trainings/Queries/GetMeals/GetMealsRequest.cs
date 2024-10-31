using Application.Trainings.DTOs.Nutrition;
using MediatR;

namespace Application.Trainings.Queries.GetMeals
{
    public class GetMealsRequest : IRequest<List<SortedByDayNutrients>>
    {
    }
}
