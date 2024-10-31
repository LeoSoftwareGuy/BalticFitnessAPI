using MediatR;

namespace Application.Nutrition.Queries.GetFoodTypes
{
    public class GetFoodTypesQuery : IRequest<List<FoodTypeDto>>
    {
    }
}
