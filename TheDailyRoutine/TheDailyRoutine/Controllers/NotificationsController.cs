using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheDailyRoutine.Services.Interfaces;

namespace TheDailyRoutine.Controllers
{
    [Authorize]
    public class NotificationsController : Controller
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        // GET: /Notifications
        public async Task<IActionResult> Index()
        {
            var userId = User.Identity.Name;
            var notifications = await _notificationService.GetUserNotificationsAsync(userId);
            return View(notifications);
        }
    }
}
