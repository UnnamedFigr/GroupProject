using System.Net.Mail;
using System.Net;
using TheDailyRoutine.Data;
using TheDailyRoutine.Entities;
using TheDailyRoutine.Entities.Enums;
using TheDailyRoutine.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TheDailyRoutine.Services.Implementation
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public NotificationService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Notification>> GetUserNotificationsAsync(string userId)
        {
            return await _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.SentDate)
                .ToListAsync();
        }

        public async Task SendNotificationAsync(Notification notification)
        {
            // Add the notification to the database
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            // Optionally send an email
            if (!string.IsNullOrEmpty(notification.Message))
            {
                await SendEmailAsync(notification.UserId, "Notification", notification.Message);
            }
        }

        public async Task SendProgressReminderAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) throw new Exception("User not found");

            var message = "Don't forget to complete your habits today!";
            var notification = new Notification
            {
                UserId = userId,
                Message = message,
                Type = NotificationType.Reminder,
                SentDate = DateTime.UtcNow
            };

            await SendNotificationAsync(notification);
        }

        public async Task SendAchievementNotificationAsync(string userId, string message)
        {
            var notification = new Notification
            {
                UserId = userId,
                Message = message,
                Type = NotificationType.Achievement,
                SentDate = DateTime.UtcNow
            };

            await SendNotificationAsync(notification);
        }

        private async Task SendEmailAsync(string userId, string subject, string body)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null || string.IsNullOrEmpty(user.Email))
                return;

            var smtpClient = new SmtpClient
            {
                Host = _configuration["Smtp:Host"],
                Port = int.Parse(_configuration["Smtp:Port"]),
                Credentials = new NetworkCredential(_configuration["Smtp:Username"], _configuration["Smtp:Password"]),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["Smtp:FromEmail"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(user.Email);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}