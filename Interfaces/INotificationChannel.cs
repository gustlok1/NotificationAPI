using NotificationApi.Models;

namespace NotificationApi.Interfaces
{
    public interface INotificationChannel
    {
        string ChannelName { get; }
        Task<NotificationResponse> SendAsync(string recipient, string message);

    }
}
