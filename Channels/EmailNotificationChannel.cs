using NotificationApi.Interfaces;
using NotificationApi.Models;

namespace NotificationApi.Services
{
    public class EmailNotificationChannel : INotificationChannel
    {
        public string ChannelName => "email";
        public async Task<NotificationResponse> SendAsync(string recipient, string message)
        {
            string messageResult = ($"[Email] Para: {recipient} | Mensagem: {message}");

            await Task.CompletedTask;
            return new NotificationResponse(true, messageResult, DateTime.Now);
        }
    }
}
