using Application.Support.Models;

namespace Application.Support.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync(Message message);
    }
}
