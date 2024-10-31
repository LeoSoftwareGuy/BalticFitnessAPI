namespace Domain.Nutrition
{
    public class Meal
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public DateTime MealTime { get; set; }

        public ICollection<ConsumedProduct> Products { get; set; }
        public double GetMealsTotalCalories() => Products.Sum(m => m.CalculateCalories());
        public double GetMealsTotalProtein() => Products.Sum(m => m.CalculateProtein());
        public double GetMealsTotalCarbs() => Products.Sum(m => m.CalculateCarbs());
        public double GetMealsTotalFats() => Products.Sum(m => m.CalculateFats());
    }
}
