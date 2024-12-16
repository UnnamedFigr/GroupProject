using TheDailyRoutine.Entities;

namespace TheDailyRoutine.Services.Interfaces
{
    public interface IHabitService
    {
        Task<IEnumerable<Habit>> GetAllPredefinedHabitsAsync();
        Task<Habit> CreateCustomHabitAsync(Habit habit, string userId);
        Task<Habit> GetHabitByIdAsync(int id);
    }
}
