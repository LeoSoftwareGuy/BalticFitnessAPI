namespace Domain.Nutrition
{
    public class ConsumedProduct
    {
        public double Quantity { get; set; }
        public double WeightGrams { get; set; }

        public DateTime ConsumedAt { get; set; }


        public Product Product { get; set; }

        public double CalculateCalories()
        {
            var caloriesPerGram = Product.CaloriesPer100 / 100;
            var calories = WeightGrams * caloriesPerGram * Quantity;
            return calories;
        }

        public double CalculateFats()
        {
            var fatsPerGram = Product.FatsPer100 / 100;
            var fats = WeightGrams * fatsPerGram * Quantity;
            return fats;
        }

        public double CalculateProtein()
        {
            var proteinPerGram = Product.ProteinPer100 / 100;
            var protein = WeightGrams * proteinPerGram * Quantity;
            return protein;
        }

        public double CalculateCarbs()
        {
            var carbsPerGram = Product.CarbsPer100 / 100;
            var carbs = WeightGrams * carbsPerGram * Quantity;
            return carbs;
        }
    }
}
