using Application.Nutrition;
using Application.Support.Mappings;
using AutoMapper;
using Domain.Nutrition;

namespace Application.Trainings.DTOs.Nutrition
{
    public class ConsumedProductDto : IMapFrom<ConsumedProduct>
    {
        public double Quantity { get; set; }
        public double WeightGrams { get; set; }
        public DateTime ConsumedAt { get; set; }

        public ProductDto Product { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ConsumedProduct, ConsumedProductDto>().ReverseMap();
        }
    }
}
