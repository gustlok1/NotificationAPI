using NotificationApi.Interfaces;

namespace NotificationApi.Services
{
    public class SmsNotificationChannel : INotificationChannel
    {
        public string ChannelName => "sms";
        public Task SendMessage(string recipient, string message)
        {
            Console.WriteLine($"[SMS] Para: {recipient} | Mensagem: {message}");
            return Task.CompletedTask;
        }
    }
}
