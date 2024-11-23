namespace Domain.Nutrition
{
    public class Product
    {
        public int Id { get; set; }
        public int FoodTypeId { get; set; }

        public string Title { get; set; }
        public string ImageUrl { get; set; }
  
        public float CaloriesPer100 { get; set; }
        public float FatsPer100 { get; set; }
        public float CarbsPer100 { get; set; }
        public float ProteinPer100 { get; set; }

        public FoodType FoodType { get; set; }
        public ICollection<ConsumedProduct> ConsumedProducts { get; set; }
    }
}
