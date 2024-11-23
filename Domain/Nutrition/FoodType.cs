
namespace Domain.Nutrition
{
    public class FoodType
    {
        public FoodType()
        {
            Products = new HashSet<Product>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
