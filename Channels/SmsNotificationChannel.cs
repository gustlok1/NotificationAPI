using NotificationApi.Interfaces;
using NotificationApi.Models;

namespace NotificationApi.Services
{
    public class SmsNotificationChannel : INotificationChannel
    {
        public string ChannelName => "sms";
        public async Task<NotificationResponse> SendAsync(string recipient, string message)
        {
            string messageResult = ($"[SMS] Para: {recipient} | Mensagem: {message}");

            await Task.CompletedTask;
            return new NotificationResponse(true, messageResult, DateTime.Now);
        }
    }
}
