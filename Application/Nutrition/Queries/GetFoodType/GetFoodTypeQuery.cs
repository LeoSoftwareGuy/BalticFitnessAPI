using MediatR;

namespace Application.Nutrition.Queries.GetFoodType
{
    public class GetFoodTypeQuery :IRequest<FoodTypeDto>
    {
        public int Id { get; set; }
    }
}
