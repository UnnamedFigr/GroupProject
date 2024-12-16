using System.ComponentModel.DataAnnotations;
using TheDailyRoutine.Entities.Enums;

namespace TheDailyRoutine.Entities
{
    public class Notification
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public string Message { get; set; }

        public NotificationType Type { get; set; } 

        public DateTime SentDate { get; set; } = DateTime.UtcNow;

        public ApplicationUser User { get; set; }
    }
}
