using Application.Support.Mappings;
using Domain.Nutrition;

namespace Application.Nutrition
{
    public class ProductDto : IMapFrom<Product>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public float CaloriesPer100 { get; set; }
        public float FatsPer100 { get; set; }
        public float CarbsPer100 { get; set; }
        public float ProteinPer100 { get; set; }

        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
