using TheDailyRoutine.Entities;

namespace TheDailyRoutine.Services.Interfaces
{
    public interface INotificationService
    {
        Task<IEnumerable<Notification>> GetUserNotificationsAsync(string userId);
        Task SendNotificationAsync(Notification notification);
        Task SendProgressReminderAsync(string userId);
        Task SendAchievementNotificationAsync(string userId, string message);

    }
}
