using NotificationApi.Interfaces;
using NotificationApi.Models;

namespace NotificationApi.Tests.Fakes
{
    public class FakeNotificationChannel : INotificationChannel
    {
        public string ChannelName { get; }
        public List<string> SentMessages = new();

        public FakeNotificationChannel(string channelName)
        {
            ChannelName = channelName;
        }

        public async Task<NotificationResponse> SendAsync(string recipient, string message)
        {
            SentMessages.Add(message);
            await Task.CompletedTask;
            return new NotificationResponse(true, "Fake enviado!", DateTime.Now);
        }
    }
}
