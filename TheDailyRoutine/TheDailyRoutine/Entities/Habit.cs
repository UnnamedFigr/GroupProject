using System.ComponentModel.DataAnnotations;

namespace TheDailyRoutine.Entities
{
    public class Habit
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The name cannot exceed some characters.")]
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsPredefined { get; set; } = true;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public string CreatedBy { get; set; }

        public ICollection<UserHabit> UserHabits { get; set; }
    }
}
