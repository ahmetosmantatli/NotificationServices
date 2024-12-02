using NotificationService.DTOs;

namespace NotificationService.Services
{
    public interface INotificationService
    {
        Task SendEmail(NotificationDto notificationDto);
    }
}
