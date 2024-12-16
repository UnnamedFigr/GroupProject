using Microsoft.AspNetCore.Identity;

namespace TheDailyRoutine.Entities
{
    public class ApplicationUser : IdentityUser
    {
      
        public DateTime RegisteredDate { get; set; } = DateTime.UtcNow;

        public ICollection<UserHabit> UserHabits { get; set; }
        public ICollection<Notification> Notifications { get; set; }
    }
}
