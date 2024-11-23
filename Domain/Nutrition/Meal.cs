namespace Domain.Nutrition
{
    public class Meal
    {
        public Meal()
        {
            ConsumedProducts = new HashSet<ConsumedProduct>();
            MealTime = DateTime.UtcNow;
        }
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime MealTime { get; set; }

        public ICollection<ConsumedProduct> ConsumedProducts { get; set; }
        public double GetMealsTotalCalories() => ConsumedProducts.Sum(m => m.CalculateCalories());
        public double GetMealsTotalProtein() => ConsumedProducts.Sum(m => m.CalculateProtein());
        public double GetMealsTotalCarbs() => ConsumedProducts.Sum(m => m.CalculateCarbs());
        public double GetMealsTotalFats() => ConsumedProducts.Sum(m => m.CalculateFats());
    }
}
