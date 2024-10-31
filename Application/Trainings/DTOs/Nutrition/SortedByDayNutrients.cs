namespace Application.Trainings.DTOs.Nutrition
{
    public class SortedByDayNutrients
    {
        public int DayOfTheYear { get; set; }
        public double DayCalories { get; set; }
        public double DayProteins { get; set; }
        public double DayCarbs { get; set; }
        public double DayFats { get; set; }
    }
}
