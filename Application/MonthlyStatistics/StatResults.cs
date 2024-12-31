namespace Application.MonthlyStatistics
{
    public class StatResults
    {
        public StatResults()
        {
            BestWorkingWeightPerExercise = new();
            AverageAmountOfRepsPerTraining = 0;
            AverageAmountOfSetsPerTraining = 0;
        }
        public Dictionary<string, string> BestWorkingWeightPerExercise { get; set; }
        public double AverageAmountOfRepsPerTraining { get; set; }
        public double AverageAmountOfSetsPerTraining { get; set; }
        //public double AverageAmountOfCaloriesPerDay { get; set; } = 0.0;
        //public double AverageAmountOfProteinsPerDay { get; set; } = 0.0;
        //public double AverageAmountOfFatsPerDay { get; set; } = 0.0;
        //public double AverageAmountOfCarbsPerDay { get; set; } = 0.0;
    }
}
