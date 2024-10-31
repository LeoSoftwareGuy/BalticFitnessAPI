using MediatR;

namespace Application.Nutrition.Queries.GetFoodProducts
{
    public class GetFoodProductsQuery : IRequest<List<ProductDto>>
    {
        public string FoodTypeUrl { get; set; } 
    }
}
