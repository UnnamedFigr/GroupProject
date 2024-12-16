using Microsoft.EntityFrameworkCore;
using TheDailyRoutine.Data;
using TheDailyRoutine.Entities;
using TheDailyRoutine.Services.Interfaces;

namespace TheDailyRoutine.Services.Implementation
{
    public class HabitProgressService : IHabitProgressService
    {
        private readonly ApplicationDbContext _context;

        public HabitProgressService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task RecordProgressAsync(int userHabitId, DateTime date, bool isCompleted)
        {
            var progress = new HabitProgress
            {
                UserHabitId = userHabitId,
                Date = date,
                IsCompleted = isCompleted
            };

            _context.HabitProgresses.Add(progress);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<HabitProgress>> GetProgressForUserAsync(string userId, DateTime startDate, DateTime endDate)
        {
            return await _context.HabitProgresses
                .Include(hp => hp.UserHabit)
                .Where(hp => hp.UserHabit.UserId == userId && hp.Date >= startDate && hp.Date <= endDate)
                .ToListAsync();
        }
    }
}
