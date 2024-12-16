using System.ComponentModel.DataAnnotations;
using TheDailyRoutine.Entities.Enums;

namespace TheDailyRoutine.Entities
{
    public class UserHabit
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        public int HabitId { get; set; }

     
        public FrequencyType Frequency { get; set; } 

        public bool IsActive { get; set; } = true;

        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        public Habit Habit { get; set; }
        public ApplicationUser User { get; set; } 
        public ICollection<HabitProgress> Progress { get; set; }
    }
}
