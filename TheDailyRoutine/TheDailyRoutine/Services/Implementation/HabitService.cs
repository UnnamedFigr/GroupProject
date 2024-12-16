using Microsoft.EntityFrameworkCore;
using TheDailyRoutine.Data;
using TheDailyRoutine.Entities;
using TheDailyRoutine.Services.Interfaces;

namespace TheDailyRoutine.Services.Implementation
{
    public class HabitService : IHabitService
    {
        private readonly ApplicationDbContext _context;

        public HabitService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Habit>> GetAllPredefinedHabitsAsync()
        {
            return await _context.Habits
                .Where(h => h.IsPredefined)
                .ToListAsync();
        }

        public async Task<Habit> CreateCustomHabitAsync(Habit habit, string userId)
        {
            habit.IsPredefined = false;
            habit.CreatedBy = userId;
            habit.CreatedOn = DateTime.UtcNow;

            _context.Habits.Add(habit);
            await _context.SaveChangesAsync();

            return habit;
        }

        public async Task<Habit> GetHabitByIdAsync(int id)
        {
            return await _context.Habits.FindAsync(id);
        }
    }
}
