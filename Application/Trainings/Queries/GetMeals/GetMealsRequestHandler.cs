using Application.Data;
using Application.Support.Interfaces;
using Application.Trainings.DTOs.Nutrition;
using AutoMapper;
using Domain.Nutrition;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Trainings.Queries.GetMeals
{
    public class GetMealsRequestHandler : IRequestHandler<GetMealsRequest, List<SortedByDayNutrients>>
    {
        private readonly IMapper _mapper;
        private readonly ITrainingDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public GetMealsRequestHandler(IMapper mapper, ITrainingDbContext context, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<List<SortedByDayNutrients>> Handle(GetMealsRequest request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var result = new List<SortedByDayNutrients>();
            var meals = await _context.Meals
                .Where(c => c.UserId.Equals(userId))
                .ToListAsync(cancellationToken);


            if (meals.Any())
            {
                var sortedByDateMeals = new Dictionary<int, List<Meal>>();

                foreach (var meal in meals.OrderByDescending(d => d.MealTime))
                {
                    if (sortedByDateMeals.ContainsKey(meal.MealTime.DayOfYear))
                        sortedByDateMeals[meal.MealTime.DayOfYear].Add(meal);
                    else
                        sortedByDateMeals[meal.MealTime.DayOfYear] = new List<Meal> { meal };
                }

                foreach (var sortedByDateMeal in sortedByDateMeals)
                {
                    var calories = sortedByDateMeal.Value.Sum(m => m.GetMealsTotalCalories());
                    var carbs = sortedByDateMeal.Value.Sum(m => m.GetMealsTotalCarbs());
                    var proteins = sortedByDateMeal.Value.Sum(m => m.GetMealsTotalProtein());
                    var fats = sortedByDateMeal.Value.Sum(m => m.GetMealsTotalFats());
                    result.Add(new SortedByDayNutrients()
                    {
                        DayOfTheYear = sortedByDateMeal.Key,
                        DayCalories = RoundUpForDouble(calories, 2),
                        DayCarbs = RoundUpForDouble(carbs, 2),
                        DayFats = RoundUpForDouble(fats, 2),
                        DayProteins = RoundUpForDouble(proteins, 2)
                    });
                }
            }

            return result;
        }
        private double RoundUpForDouble(double input, int places)
        {
            double multiplier = Math.Pow(10, Convert.ToDouble(places));
            return Math.Ceiling(input * multiplier) / multiplier;
        }
    }
}


