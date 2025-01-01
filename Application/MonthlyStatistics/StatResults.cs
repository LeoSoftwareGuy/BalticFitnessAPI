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
    }
}
