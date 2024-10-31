using Application.Support.Interfaces;
using Application.Support.Models;

namespace Application.Support
{
    public class NotificationService : INotificationService
    {
        public Task SendAsync(Message message)
        {
            // Implementation for sending notification
            return Task.CompletedTask;
        }
    }
}
