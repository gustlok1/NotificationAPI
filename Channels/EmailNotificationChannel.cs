using NotificationApi.Interfaces;

namespace NotificationApi.Services
{
    public class EmailNotificationChannel : INotificationChannel
    {
        public string ChannelName => "email";
        public Task SendMessage(string recipient, string message)
        {
            Console.WriteLine($"[EMAIL] Para: {recipient} | Mensagem: {message}");
            return Task.CompletedTask;
        }
    }
}
