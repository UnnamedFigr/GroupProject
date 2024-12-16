using TheDailyRoutine.Entities.Enums;
using TheDailyRoutine.Entities;

namespace TheDailyRoutine.Services.Interfaces
{
    public interface IHabitProgressService
    {
        Task RecordProgressAsync(int userHabitId, DateTime date, bool isCompleted);
        Task<IEnumerable<HabitProgress>> GetProgressForUserAsync(string userId, DateTime startDate, DateTime endDate);

    }
}
