using Application.Support.Mappings;
using Domain.Nutrition;

namespace Application.Nutrition
{
    public class FoodTypeDto : IMapFrom<FoodType>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public List<ProductDto> Products { get; set; }
        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<FoodType, FoodTypeDto>();
        }
    }
}
