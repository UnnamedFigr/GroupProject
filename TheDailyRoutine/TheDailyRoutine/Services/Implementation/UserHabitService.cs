using TheDailyRoutine.Data;
using TheDailyRoutine.Entities.Enums;
using TheDailyRoutine.Entities;
using Microsoft.EntityFrameworkCore;
using TheDailyRoutine.Services.Interfaces;

namespace TheDailyRoutine.Services.Implementation
{
    public class UserHabitService : IUserHabitService
    {
        private readonly ApplicationDbContext _context;

        public UserHabitService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddHabitToUserAsync(int habitId, string userId, FrequencyType frequency)
        {
            var userHabit = new UserHabit
            {
                HabitId = habitId,
                UserId = userId,
                Frequency = frequency,
                StartDate = DateTime.UtcNow,
                IsActive = true
            };

            _context.UserHabits.Add(userHabit);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserHabit>> GetUserHabitsAsync(string userId)
        {
            return await _context.UserHabits
                .Include(uh => uh.Habit)
                .Where(uh => uh.UserId == userId && uh.IsActive)
                .ToListAsync();
        }

        public async Task RemoveUserHabitAsync(int userHabitId, string userId)
        {
            var userHabit = await _context.UserHabits
                .FirstOrDefaultAsync(uh => uh.Id == userHabitId && uh.UserId == userId);

            if (userHabit != null)
            {
                userHabit.IsActive = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}
