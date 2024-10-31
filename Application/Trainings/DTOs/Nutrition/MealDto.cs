using Application.Support.Mappings;
using AutoMapper;
using Domain.Nutrition;
using MongoDB.Bson;

namespace Application.Trainings.DTOs.Nutrition
{
    public class MealDto : IMapFrom<Meal>
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public DateTime MealTime { get; set; }

        public List<ConsumedProductDto> Products { get; set; } = new();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Meal, MealDto>().ReverseMap();
        }
    }
}
