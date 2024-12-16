using System.ComponentModel.DataAnnotations;

namespace TheDailyRoutine.Entities
{
    public class HabitProgress
    {
        public int Id { get; set; }

        [Required]
        public int UserHabitId { get; set; }

        [Required]
        public DateTime Date { get; set; } 

        public bool IsCompleted { get; set; }

        public UserHabit UserHabit { get; set; }
    }
}
