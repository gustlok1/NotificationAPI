namespace NotificationApi.Interfaces
{
    public interface INotificationChannel
    {
        string ChannelName { get; }
        Task SendMessage(string recipient, string message);

    }
}
