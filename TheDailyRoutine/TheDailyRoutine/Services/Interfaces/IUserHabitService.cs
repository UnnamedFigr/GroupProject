using TheDailyRoutine.Entities.Enums;
using TheDailyRoutine.Entities;

namespace TheDailyRoutine.Services.Interfaces
{
    public interface IUserHabitService
    {
        Task AddHabitToUserAsync(int habitId, string userId, FrequencyType frequency);
        Task<IEnumerable<UserHabit>> GetUserHabitsAsync(string userId);
        Task RemoveUserHabitAsync(int userHabitId, string userId);
    }
}
