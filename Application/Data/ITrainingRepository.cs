using Application.Trainings.DTOs.Trainings;
using Domain;

namespace Application.Data
{
    public interface ITrainingRepository
    {
        Task<List<SortedByDayTraining>> GetAllTrainings(string userId);
        Task<int> SaveTraining(Training training);
    }
}
